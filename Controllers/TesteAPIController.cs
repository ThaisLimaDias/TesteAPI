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
    public class TesteAPIController : Controller
    { 
        private readonly ITesteAPIService _itesteAPIService;
        public TesteAPIController(ITesteAPIService itesteAPIService)
        {            
            _itesteAPIService = itesteAPIService;
        }

        // GET TesteAPI
        [HttpGet()]
        public async Task<IActionResult> Get([FromQuery]string cpf)
        {
            try
            {
                var listCli = await _itesteAPIService.GetCliente(cpf);

                if (listCli != null && listCli.Count() > 0)
                    return Ok(listCli);
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

      

        // POST TesteAPI
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TbClientes cliente)
        {
            try
            {
                 if (ModelState.IsValid)
                {
                    var postCli = await _itesteAPIService.PostCliente(cliente);

                    if (postCli != null)
                        return Ok(postCli);
                    else
                        return BadRequest();
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // PUT TesteAPI/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(long id,[FromBody] TbClientes cliente)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var mod = await _itesteAPIService.PutCliente(cliente);
                    if (mod == null)
                        return BadRequest();

                    return Ok(mod);
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // DELETE TesteAPI/5
        [HttpDelete()]
        public async Task<IActionResult> Delete([FromBody] TbClientes cliente)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var delCli = await _itesteAPIService.DeleteCliente(cliente.CPF);

                    if (delCli)
                        return Ok();
                    else
                        return NotFound();
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
