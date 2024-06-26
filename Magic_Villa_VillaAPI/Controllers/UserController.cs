﻿using Magic_Villa_VillaAPI.Models;
using Magic_Villa_VillaAPI.Models.DTO;
using Magic_Villa_VillaAPI.Repository.IRepositpory;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Magic_Villa_VillaAPI.Controllers
{
    [ApiController]
    [Route("/api/UserAuth")]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        protected APIResponse _response;
        public UserController(IUserRepository userRepository)
        {
            this._userRepository = userRepository;
            _response = new();

        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody]LoginRequestDTO model)
        {
            var loginResponse =await _userRepository.Login(model);
            if (loginResponse.User == null || string.IsNullOrEmpty(loginResponse.Token))
            {
                _response.StatusCode=HttpStatusCode.BadRequest;
                _response.IsSuccess=false;
                _response.ErrorMessages.Add("Username or password is incorrect");
                return BadRequest(_response);
            }
            _response.StatusCode = HttpStatusCode.OK;
            _response.IsSuccess = true;
            _response.Result= loginResponse;
            return Ok(_response);
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterationRequestDTO model)
        {
            bool ifUserNameUnique = _userRepository.IsUniqueUser(model.UserName);
            if (!ifUserNameUnique)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages.Add("Username already exists");
                return BadRequest(_response);
            }
            var user = await _userRepository.Register(model);
            if (user == null)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages.Add("Error while Registering");
                return BadRequest(_response);
            }

            _response.StatusCode = HttpStatusCode.OK;
            _response.IsSuccess = true;
            return Ok(_response);
        }
    }

}
