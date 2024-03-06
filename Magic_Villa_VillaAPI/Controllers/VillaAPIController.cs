using AutoMapper;
using Azure;
using Magic_Villa_VillaAPI.Data;
using Magic_Villa_VillaAPI.Models;
using Magic_Villa_VillaAPI.Models.DTO;
using Magic_Villa_VillaAPI.Repository.IRepositpory;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Magic_Villa_VillaAPI.Controllers
{
    [ApiController]
    [Route("/api/VillaAPI")]
    public class VillaAPIController : ControllerBase
    {
        protected APIResponse _response;
        private readonly ILogger<VillaAPIController> _logger;
        private readonly IVillaRepository _dbVilla;
        private readonly IMapper _mapper;
        public VillaAPIController(IVillaRepository dbVilla , ILogger<VillaAPIController> logger,IMapper mapper)
        {
            this._dbVilla = dbVilla;
            this._logger = logger;
            this._mapper = mapper;
            this._response = new();
        }

        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task< ActionResult<APIResponse>> GetVillas()
        {
            try
            {
                _logger.LogInformation("Get All villas");
                IEnumerable<Villa> villalist = await _dbVilla.GetAllAsync();
                _response.Result = _mapper.Map<List<VillaDTO>>(villalist);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                
                _response.IsSuccess= false;
                _response.ErrorMessages=new List<string>() { ex.ToString() };
                
            }
            return _response;
        }
        [HttpGet("{id:int}", Name = "GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesResponseType(200,Type=typeof(VillaDTO))]
        public async Task<ActionResult<APIResponse>> GetVilla(int id)
        {
            try
            {
                if (id == 0)
                {
                    _logger.LogError("Get Villa Error with Id " + id);
                    _response.StatusCode=HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var villa = await _dbVilla.GetAsync(x => x.Id == id);
                if (villa == null)
                {
                    _response.StatusCode=HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                _response.Result = _mapper.Map<VillaDTO>(villa);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {

                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task< ActionResult<APIResponse>> CreateVilla ([FromBody]VillaCreateDTO createDto)
        {
            try { 
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (await _dbVilla.GetAsync(u=>u.Name.ToLower()== createDto.Name.ToLower())!=null)
            {
                ModelState.AddModelError("", "villa name already existed");
                return BadRequest(ModelState);
            }
            if (createDto == null)
            {
                return BadRequest(createDto);
            }
            Villa villa = _mapper.Map<Villa>(createDto);
            

            //Villa model = new ()
            //{
            //    Amenity = createDto.Amenity,
            //    Details = createDto.Details,

            //    Name = createDto.Name,
            //    ImageUrl = createDto.ImageUrl,
            //    Occupancy = createDto.Occupancy,
            //    Sqft = createDto.Sqft,
            //    Rate = createDto.Rate,
            //};
            await _dbVilla.CreateAsync(villa);
            await _dbVilla.SaveAsync();

            _response.Result = _mapper.Map<VillaDTO>(villa);
            _response.StatusCode = HttpStatusCode.Created;

            return CreatedAtRoute("GetVilla",new {id= villa.Id } , _response);
            }
            catch (Exception ex)
            {

                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };

            }
            return _response;
        }

        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        [HttpDelete("{id:int}",Name ="DeleteVilla")]
        public async Task <ActionResult<APIResponse>> DeleteVilla(int id)
        {
            try { 
            if (id==0)
            {
                return BadRequest();
            }
            var delvill =await _dbVilla.GetAsync(u=>u.Id==id);
            if (delvill == null)
            {
                return NotFound();  
            }
            await _dbVilla.RemoveAsync(delvill);
            await _dbVilla.SaveAsync();

            _response.StatusCode=HttpStatusCode.NoContent;
            _response.IsSuccess=true;

            return Ok(_response);
            }
            catch (Exception ex)
            {

                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };

            }
            return _response;
        }


        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpPut("{id:int}",Name ="UpdateVilla")]
        public ActionResult<APIResponse> UpdateVilaa(int id , [FromBody] VillaUpdateDTO updateDTO) {
            try { 
            if (updateDTO == null || id!= updateDTO.Id)
            {
                return BadRequest();
            }
            //var villa = _db.Villas.FirstOrDefault(u=>u.Id == id);
            //villa.Name= villaToUP.Name;
            //villa.Occupancy= villaToUP.Occupancy;
            //villa.Sqft= villaToUP.Sqft;

            Villa model = _mapper.Map<Villa>(updateDTO);
            
            _dbVilla.UpdateAsync(model);
            _dbVilla.SaveAsync();
            
            _response.StatusCode = HttpStatusCode.NoContent;
            _response.IsSuccess=true;

            return Ok(_response);
            }
            catch (Exception ex)
            {

                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };

            }
            return _response;
        }



        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        [HttpPatch("{id:int}", Name = "UpdatePartialVilla")]
        public async Task< IActionResult> UpdatePartialVilla(int id , JsonPatchDocument<VillaUpdateDTO> patchDTO)
        {
            if (patchDTO==null || id==0)
            {
                return BadRequest();
            }
            var Villa =await _dbVilla.GetAsync(u=> u.Id == id,tracked:false);
            VillaUpdateDTO villaDTO = _mapper.Map<VillaUpdateDTO>(Villa);


            
            if (Villa ==null)
            {
                return NotFound();
            }
            patchDTO.ApplyTo(villaDTO, ModelState);

            Villa model = _mapper.Map<Villa>(villaDTO);

            _dbVilla.UpdateAsync(model);
            await _dbVilla.SaveAsync();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return NoContent() ;
        }

    }
}
