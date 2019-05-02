using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TesteAPI.Service;
using TesteAPI.Models;
using TesteAPI.Service.Interface;

namespace TesteAPI.Controllers
{
    [Route("api/[controller]")]
    public class TipoClienteAPIController : Controller
    { 
        private readonly ITipoClienteAPIService _iTipoClienteAPIService;
        public TipoClienteAPIController(ITipoClienteAPIService iTipoClienteAPIService)
        {            
            _iTipoClienteAPIService = iTipoClienteAPIService;
        }

        // GET TesteAPI
        [HttpGet()]
        public async Task<IActionResult> Get()
        {
            try
            {
                var list= await _iTipoClienteAPIService.GetTipoCliente();

                if (list != null && list.Count() > 0)
                    return Ok(list);
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
