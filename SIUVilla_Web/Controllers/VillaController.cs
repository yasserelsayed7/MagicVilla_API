﻿using AutoMapper;
using MagicVilla_Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SIUVilla_Web.Models;
using SIUVilla_Web.Models.DTO;
using SIUVilla_Web.Services.IServices;
using System.Reflection;

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
            var response =await _villaService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess) { 
                list = JsonConvert.DeserializeObject<List<VillaDTO>>(Convert.ToString(response.Result));
            }
            return View(list);
        }
        [Authorize(Roles = "admin")]

        public async Task<IActionResult> CreateVilla()
        {
            return View();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateVilla( VillaCreateDTO model)
        {
            List<VillaDTO> list = new();
            if (ModelState.IsValid)
            {
                var response = await _villaService.CreateAsync<APIResponse>(model, HttpContext.Session.GetString(SD.SessionToken));
                if (response != null && response.IsSuccess)
                {
                    TempData["success"] = "Villa Created Successfully";
                    return RedirectToAction("IndexVilla");
                }
            }
            TempData["error"] = "Error Encounterd";

            return View(model);
        }

        [Authorize(Roles = "admin")]

        public async Task<IActionResult> UpdateVilla(int villaId)
        {
           
                var response = await _villaService.GetAsync<APIResponse>(villaId, HttpContext.Session.GetString(SD.SessionToken));
                if (response != null && response.IsSuccess)
                {
                    VillaDTO model = JsonConvert.DeserializeObject<VillaDTO>(Convert.ToString(response.Result));
                    return View(_mapper.Map<VillaUpdateDTO>(model));
                }
            return NotFound();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]

        public async Task<IActionResult> UpdateVilla(VillaUpdateDTO model)
        {
            List<VillaDTO> list = new();
            if (ModelState.IsValid)
            {
                var response = await _villaService.UpdateAsync<APIResponse>(model, HttpContext.Session.GetString(SD.SessionToken));
                if (response != null && response.IsSuccess)
                {
                    TempData["success"] = "Villa Updated Successfully";
                    return RedirectToAction("IndexVilla");
                }
            }
            TempData["error"] = "Error Encounterd";

            return View(model);
        }

        [Authorize(Roles = "admin")]

        public async Task<IActionResult> DeleteVilla(int villaId)
        {

            var response = await _villaService.GetAsync<APIResponse>(villaId, HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                VillaDTO model = JsonConvert.DeserializeObject<VillaDTO>(Convert.ToString(response.Result));
                return View(model);
            }

            return NotFound();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]

        public async Task<IActionResult> DeleteVilla(VillaDTO model)
        {
            List<VillaDTO> list = new();
            
                var response = await _villaService.DeleteAsync<APIResponse>(model.Id    , HttpContext.Session.GetString(SD.SessionToken));
                if (response != null && response.IsSuccess)
                {
                TempData["success"] = "Villa Deleted Successfully";
                return RedirectToAction("IndexVilla");
                }
            TempData["error"] = "Error Encounterd";

            return View(model);
        }
    }
}
