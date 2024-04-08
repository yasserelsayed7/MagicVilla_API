using AutoMapper;
using MagicVilla_Utility;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SIUVilla_Web.Models;
using SIUVilla_Web.Models.DTO;
using SIUVilla_Web.Services.IServices;
using System.Diagnostics;

namespace SIUVilla_Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IVillaService _villaService;
        private readonly IMapper _mapper;
        public HomeController(IVillaService villaService, IMapper mapper)
        {
            this._villaService = villaService;
            this._mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            List<VillaDTO> list = new();
            var response = await _villaService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<VillaDTO>>(Convert.ToString(response.Result));
            }
            return View(list);
        }

       
    }
}
