﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SIUVilla_Web.Models;
using SIUVilla_Web.Models.DTO;
using SIUVilla_Web.Services.IServices;

namespace SIUVilla_Web.Controllers
{
    public class VillaController : Controller
    {
        private readonly IVillaService _villaService;
        private readonly IMapper _mapper;
        public VillaController(IVillaService villaService , IMapper mapper)
        {
            this._villaService = villaService;
            this._mapper = mapper;
        }
        public async Task< IActionResult> IndexVilla()
        {
            List<VillaDTO> list = new();
            var response =await _villaService.GetAllAsync<APIResponse>();
            if (response != null && response.IsSuccess) { 
                list = JsonConvert.DeserializeObject<List<VillaDTO>>(Convert.ToString(response.Result));
            }
            return View(list);
        }

        public async Task<IActionResult> CreateVilla()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateVilla( VillaCreateDTO model)
        {
            List<VillaDTO> list = new();
            if (!ModelState.IsValid)
            {
                var response = await _villaService.CreateAsync<APIResponse>(model);
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction("IndexVilla");
                }
            }
            
            return View(ModelState);
        }
    }
}
