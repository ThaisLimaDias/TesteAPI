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
    public class SituacaoClienteAPIService : ISituacaoClienteAPIService
    {   
        //Toda Regra de negocio aqui
        private IConfiguration _configuration;
        private readonly TbSituacaoClienteRepository _tbSituacaoClienteRepository;
       
        public SituacaoClienteAPIService(TbSituacaoClienteRepository tbSituacaoClienteRepository)
        {
            _tbSituacaoClienteRepository = tbSituacaoClienteRepository;
        }

        public async Task<IEnumerable<TbSituacaoCliente>> GetSituacaoCliente()
        {
            
            IEnumerable<TbSituacaoCliente> sitclientes;

            sitclientes = await _tbSituacaoClienteRepository.SituacaoCliente();

            return sitclientes;
        }
       
    }

}