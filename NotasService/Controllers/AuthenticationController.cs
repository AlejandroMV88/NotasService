using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NotasService.Middleware;
using NotasService.Models;
using NotasService.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotasService.Controllers
{

    namespace BackEndPreguntas.Controllers
    {
        [Route("api/[controller]")]
        [ApiController]
        public class AuthenticationController : ControllerBase
        {
            private IAuthenticationService authenticationService;
            private ILoginAddService loginAddService;
            public AuthenticationController(IAuthenticationService authenticationService, ILoginAddService loginAddService)
            {
                this.authenticationService = authenticationService;
                this.loginAddService = loginAddService;
            }

            [Route("login")]
            [HttpPost]

            public IActionResult Login([FromBody] User user)
            {
                //UserAddSqlService service = new UserAddSqlService();
                AuthenticateResponse resp = this.authenticationService.Login(user);
                if (resp != null)
                {
                    return Ok(resp);
                }
                return BadRequest("Contraseña o Usuario incorrecto");
            }

            
            [Route("addUser")]
            [HttpPost]
            public ServiceResponse Post([FromBody] User user)
            {
                return this.loginAddService.Execute(user);
            }

            //[CustomAuthorize]
            //[Route("query")]
            //[HttpPost]
            //public IActionResult Query()
            //{
            //    //UserAddSqlService service = new UserAddSqlService();
            //    String resp = this.authenticationService.MessageGet();
            //    if (!String.IsNullOrEmpty(resp))
            //    {
            //        return Ok(resp);
            //    }
            //    return BadRequest("Contraseña o Usuario incorrecto");
            //}
        }
    }

}
