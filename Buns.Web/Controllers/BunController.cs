using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Buns.Data;
using Buns.Web.Modules.Buns.Dto;
using Buns.Web.Modules.Buns.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Buns.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BunController : ControllerBase
    {
        private readonly BunService _service;
        public BunController(BunService service)
        {
            _service = service;
        }

        [HttpGet]
        public IEnumerable<BunListDto> Get() => _service.GetList();
    }
}