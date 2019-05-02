using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TesteAPI.Models.Repository;
using TesteAPI.Models;
using TesteAPI.Service.Interface;
using Microsoft.Extensions.Configuration;
using System.Linq;

namespace TesteAPI.Service
{
    public class TipoClienteAPIService : ITipoClienteAPIService
    {   
        //Toda Regra de negocio aqui
        private IConfiguration _configuration;
        private readonly TbTipoClienteRepository _tbTipoClienteRepository;
       
        public TipoClienteAPIService(TbTipoClienteRepository tbTipoClienteRepository)
        {
            _tbTipoClienteRepository = tbTipoClienteRepository;
        }

        public async Task<IEnumerable<TbTipoCliente>> GetTipoCliente()
        {
            
            IEnumerable<TbTipoCliente> tpclientes;

            tpclientes = await _tbTipoClienteRepository.TipoCliente();

            return tpclientes;
        }
       
    }

}