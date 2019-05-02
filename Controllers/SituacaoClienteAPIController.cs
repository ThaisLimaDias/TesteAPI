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
    public class SituacaoClienteAPIController : Controller
    { 
        private readonly ISituacaoClienteAPIService _iSituacaoClienteAPIService;
        public SituacaoClienteAPIController(ISituacaoClienteAPIService iSituacaoClienteAPIService)
        {            
            _iSituacaoClienteAPIService = iSituacaoClienteAPIService;
        }

        // GET TesteAPI
        [HttpGet()]
        public async Task<IActionResult> Get()
        {
            try
            {
                var list = await _iSituacaoClienteAPIService.GetSituacaoCliente();

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
