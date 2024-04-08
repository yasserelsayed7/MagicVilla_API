using AutoMapper;
using Azure;
using Magic_Villa_VillaAPI.Data;
using Magic_Villa_VillaAPI.Models;
using Magic_Villa_VillaAPI.Models.DTO;
using Magic_Villa_VillaAPI.Repository.IRepositpory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System.Net;


namespace Magic_Villa_VillaAPI.Controllers
{
    [Route("api/VillaNumberAPI")]
    [ApiController]
    public class VillaNumberAPIController : ControllerBase
    {
        protected APIResponse _response;
        private readonly ILogger<VillaNumberAPIController> _logger;
        private readonly IVillaNumberRepository _dbvillaNO;
        private readonly IVillaRepository _dbvilla;

        private readonly IMapper _mapper;
        public VillaNumberAPIController(IVillaNumberRepository dbvillNO, IMapper mapper, ILogger<VillaNumberAPIController> logger, IVillaRepository dbvilla)
        {
            this._response = new();
            this._dbvillaNO = dbvillNO;
            this._mapper = mapper;
            _logger = logger;
            _dbvilla = dbvilla;


        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public async Task<ActionResult<APIResponse>> GetVillaNumbers()
        {
            try
            {
                _logger.LogInformation("Get All villas numbers");
                IEnumerable<VillaNumber> villaNumberlist = await _dbvillaNO.GetAllAsync(includeProperities:"Villa");
                _response.Result = _mapper.Map<List<VillaNumberDTO>>(villaNumberlist);
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

        [HttpGet("{villaNo:int}", Name = "GetVillaNumber")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<APIResponse>> GetVillaNumber(int villaNo)
        {
            try
            {
                if (villaNo == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                VillaNumber villno = await _dbvillaNO.GetAsync(x => x.VillaNo == villaNo);
                if (villno == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                _response.Result = _mapper.Map<VillaNumberDTO>(villno);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }

            return Ok(_response);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [Authorize(Roles = "admin")]

        public async Task<ActionResult<APIResponse>> CreateVillaNumber([FromBody] VillaNumberCreateDTO villanoCreateDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(ModelState);
                }
                if (await _dbvillaNO.GetAsync(x => x.VillaNo == villanoCreateDTO.VillaNo) != null)
                {
                    ModelState.AddModelError("ErrorMessages", "villa number already existed !");
                    return BadRequest(ModelState);
                }
                if (await _dbvilla.GetAsync(u => u.Id == villanoCreateDTO.villaId) == null)
                {
                    ModelState.AddModelError("ErrorMessages", "VILLA Id is invalid !");
                    return BadRequest(ModelState);
                }
                                   
                if (villanoCreateDTO == null)
                {
                    return BadRequest(villanoCreateDTO);
                }
                VillaNumber villaNO = _mapper.Map<VillaNumber>(villanoCreateDTO);
                await _dbvillaNO.CreateAsync(villaNO);

                _response.Result = _mapper.Map<VillaNumber>(villanoCreateDTO);
                _response.StatusCode = HttpStatusCode.Created;
                
                return CreatedAtRoute("GetVillaNumber", new { villaNO = villaNO.VillaNo }, _response);
            }
            catch (Exception ex)
            {

                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpDelete("{villaNO:int}", Name = "DeleteVillaNumber")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "admin")]

        public async Task<ActionResult<APIResponse>> DeleteVillaNumber(int villaNO)
        {
            try
            {
                if (villaNO == 0)
                {
                    return BadRequest();
                }
                var villNO = await _dbvillaNO.GetAsync(x => x.VillaNo == villaNO);
                if (villaNO == null)
                {
                    return NotFound();
                }
                await _dbvillaNO.RemoveAsync(villNO);
                await _dbvillaNO.SaveAsync();

                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;

                return Ok(_response);
            }
            catch (Exception ex)
            {

                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };

            }
            return _response;
        }

        [HttpPut("{villNo:int}", Name = "UpdateVillaNumber")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "admin")]

        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> UpdateVillaNumber(int villNo, [FromBody] VillaNumberUpdateDTO villaNoUPDTO)
        {
            try
            {
                if (villaNoUPDTO == null || villNo != villaNoUPDTO.VillaNo)
                {
                    return BadRequest();
                }

                if (await _dbvilla.GetAsync(u => u.Id == villaNoUPDTO.villaId) == null)
                {
                    ModelState.AddModelError("CustomError", "VILLA Id is invalid !");
                    return BadRequest(ModelState);
                }


                VillaNumber model = _mapper.Map<VillaNumber>(villaNoUPDTO);
                 await _dbvillaNO.UpdateAsync(model);
                await _dbvillaNO.SaveAsync();

                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;

                return Ok(_response);
            }
            catch (Exception ex)
            {

                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };

            }
            return _response;
        }


    }
}

