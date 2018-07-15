﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using iBookmark.API.Auth;
using iBookmark.API.Options;
using iBookmark.Helper.Security;
using iBookmark.Model.AggregatesModel.UserAggregate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace iBookmark.API.Controllers
{
    [AllowAnonymous]
    [Produces("application/json")]
    [Route("Auth")]
    public class AuthenticationController : Controller
    {
        private IUserRepository _userRepository;

        private IEncryptorDecryptor _encryptorDecryptor;
        private IJwtFactory _jwtFactory;
        private readonly JwtIssuerOptions _jwtOptions;

        public AuthenticationController(IUserRepository userRepository, IEncryptorDecryptor encryptorDecryptor, IJwtFactory jwtFactory, IOptions<JwtIssuerOptions> jwtOptions)
        {
            this._userRepository = userRepository;
            this._encryptorDecryptor = encryptorDecryptor;
            _jwtFactory = jwtFactory;
            _jwtOptions = jwtOptions.Value;
        }
        public async Task<int> SignupAsync(UserModel user)
        {
            user.Password = _encryptorDecryptor.Encrypt(user.Password);
            return await _userRepository.InsertUpdateUserAsync(user);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]LoginModel login)
        {
            var identity = await GetClaimsIdentity(login);
            if (identity == null)
            {
                return BadRequest("Invalid username or password.");
            }

            var jwt = await Tokens.GenerateJwt(identity, _jwtFactory, login.UserName, _jwtOptions, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }


        private async Task<ClaimsIdentity> GetClaimsIdentity(LoginModel login)
        {
            if (string.IsNullOrEmpty(login.UserName) || string.IsNullOrEmpty(login.Password))
                return await Task.FromResult<ClaimsIdentity>(null);

            // get the user to verifty
            var user = await _userRepository.ValidateUserAsync(login);

            // check the credentials
            if (user != null)
            {
                return await Task.FromResult(_jwtFactory.GenerateClaimsIdentity(user));
            }

            // Credentials are invalid, or account doesn't exist
            return await Task.FromResult<ClaimsIdentity>(null);
        }
    }
}