﻿using Flavory.Services.AuthAPI.Models.Dto;
using Flavory.Services.AuthAPI.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Flavory.Services.AuthAPI.Controllers
{
	[Route("api/auth")]
	[ApiController]
	public class AuthAPIController : ControllerBase
	{
		private readonly IAuthService _authService;
		protected ResponseDto _response;

		public AuthAPIController(IAuthService authService)
		{
			_authService = authService;
			_response = new();
		}


		[HttpPost("register")]
		public async Task<IActionResult> Register([FromBody] RegistrationRequestDto model)
		{
			var errorMessage = await _authService.Register(model);
			if (!string.IsNullOrEmpty(errorMessage))  //It means the error message encountered
			{
				_response.IsSuccess= false;
				_response.Message=errorMessage;
				return BadRequest(_response);
			}
			return Ok(_response);
		}


		[HttpPost("login")]
		public async Task<IActionResult> Login()
		{
			return Ok();
		}
	}
}
