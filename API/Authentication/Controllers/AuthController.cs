using System;
using System.Threading.Tasks;
using Authentication.Data.Repository;
using Authentication.Events;
using Authentication.Models;
using Domain_Core.Bus;
using Microsoft.AspNetCore.Mvc;

namespace Authentication.Controllers {
    [Route ("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase {
        private readonly IUserService<User> _userService;
        private readonly IMediatorHandler _mediator;

        public AuthController (IUserService<User> userService, IMediatorHandler mediator) {
            _mediator = mediator;
            _userService = userService;
        }

        [HttpPost ("login")]
        public async Task<IActionResult> Login (Login model) {
            if (ModelState.IsValid) {
                var user = await _userService.FindByEmail (model.Email);

                if (await _userService.ValidateCredentials (user, model.Password)) {
                    await _userService.SignIn (user);

                    var token = await _userService.GetToken (user, this.Request.Path);
                    return Ok (new {
                        responseType = "UserLoggedIn",
                            response = "O utilizador fez login",
                            accessToken = token
                    });
                }
                return Unauthorized (new {
                    responseType = "Unauthorized",
                        response = "Credenciais não corretas."
                });
            }
            return BadRequest ();
        }

        [HttpPost ("register")]
        public async Task<IActionResult> Register (Register model) {
            if (model != null) {
                if (model.Password == model.ConfirmPassword) {
                var user = new User () {
                UserName = model.Username,
                Email = model.Email,
                Name = model.Name,
                IdentificationNumber = model.IdentificationNumber,
                PostalCode = model.PostalCode,
                Place = model.Place,
                Address = model.Address,
                PhoneNumber = model.PhoneNumber,
                IsCollaborator = model.IsCollaborator
                    };

                    if (model.IdentificationNumber == null || _userService.VerifyIdentificationNumber (model.IdentificationNumber)) {
                        await _userService.RegisterUser (user, model.Password);

                        await _mediator.RaiseEvent(new ClientRegisteredEvent(user.Id,user.UserName, user.Email, user.Name, user.IdentificationNumber, user.PostalCode, user.Place, user.Address, user.IsCollaborator));
                        return Ok (new {
                            responseType = "UserRegistered",
                                reponse = "User successfully registered",
                        });
                    }
                }

                return BadRequest (new {
                    responseType = "InvalidPassword",
                        response = "Password and ConfirmPassword must be the same"
                });
            }
            return BadRequest ();
        }

        [HttpGet ("isCollaborator/{id}")]
        public async Task<IActionResult> IsCollaborator (Guid id) {
            var user = await _userService.FindById (id);
            if (user != null)
                return Ok (user.IsCollaborator);

            return BadRequest ();
        }
    }
}