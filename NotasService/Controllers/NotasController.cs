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
    [Route("api/[controller]")]
    [ApiController]
    public class NotasController : ControllerBase
    {

        INotasAddService notasAddService;
        INotasGetService notasGetService;
        INotasUpdateService notasUpdateService;
        INotasDeleteService notasDeleteService;
        INotasSearchByDateService notasSearchByDateService;

        public NotasController(INotasAddService notasAddService,
                                INotasGetService notasGetService, 
                                INotasUpdateService notasUpdateService, 
                                INotasDeleteService notasDeleteService,
                                INotasSearchByDateService notasSearchByDateService)
        {
            this.notasGetService = notasGetService;
            this.notasAddService = notasAddService;
            this.notasUpdateService = notasUpdateService;
            this.notasDeleteService = notasDeleteService;
            this.notasSearchByDateService = notasSearchByDateService;
        }

        [CustomAuthorize]
        [Route("getNotas")]
        [HttpGet]
        public ServiceResponse Get()
        {
            return this.notasGetService.Execute();
        }

        [CustomAuthorize]
        [Route("addNotas")]
        [HttpPost]
        public ServiceResponse Post([FromBody] Notas notas)
        {
            return this.notasAddService.Execute(notas);
        }

        [CustomAuthorize]
        [Route("updateNotas")]
        [HttpPost]
        public ServiceResponse Update([FromBody] Notas notas)
        {
            return this.notasUpdateService.Execute(notas);
        }

        [CustomAuthorize]
        [Route("deleteNotas")]
        [HttpPost]
        public ServiceResponse Delete([FromBody] Notas notas)
        {
            return this.notasDeleteService.Execute(notas);
        }

        [CustomAuthorize]
        [Route("searchNotas")]
        [HttpPost]
        public ServiceResponse Search([FromBody] Notas notas)
        {
            return this.notasSearchByDateService.Execute(notas);
        }
    }
}
