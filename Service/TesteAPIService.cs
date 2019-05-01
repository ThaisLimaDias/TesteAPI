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
    public class TesteAPIService : ITesteAPIService
    {   
        //Toda Regra de negocio aqui
        private IConfiguration _configuration;
        private readonly TbClientesRepository _tbClientesRepository;
       
        public TesteAPIService(TbClientesRepository tbClientesRepository)
        {
            _tbClientesRepository = tbClientesRepository;
           // _iTesteAPIService = iTesteAPIService;
        }

        public async Task<IEnumerable<TbClientes>> GetCliente(string cpf)
        {
            
            IEnumerable<TbClientes> clientes;

            clientes = await _tbClientesRepository.SelectClientes(cpf);

            return clientes;
        }

        public async Task<TbClientes> PostCliente(TbClientes cliente)
        {
            //VERIFICA SE CPF EXISTE NO BANCO ANTES DE INSERIR
            var cliExis = await _tbClientesRepository.SelectClientes(cliente.CPF);
            if (cliExis.Count()>0)
                throw new Exception("Cliente não pode ser inserido, já existe um CPF Cadastrado!");

            var resp = await _tbClientesRepository.CreateClientes(cliente);
            
            if (resp == false)
                throw new Exception("Erro ao inserir Cliente!");

            var cli = await _tbClientesRepository.SelectClientes(cliente.CPF);
            return cli.FirstOrDefault();
        }

        public async Task<TbClientes> PutCliente(TbClientes cliente)
        {            
            //VERIFICA SE CLIENTE EXISTE ANTES DE ATUALIZAR PARA NÃO PERMITIR ALTERAR O CPF PARA UM EXISTENTE
            var cliExis = await _tbClientesRepository.SelectClientes(cliente.CPF);
            if (cliExis.Count()==0)
                throw new Exception("Cliente não pode ser localizado!");

             if (cliExis.Count()>1)
                throw new Exception("Cliente não pode ser inserido com este CPF!");
            var resp = await _tbClientesRepository.UpdateClientes(cliente);
            if (resp == false)
                throw new Exception("Erro ao atualizar Cliente!");

            var cli = await _tbClientesRepository.SelectClientes(cliente.CPF);
            return cli.FirstOrDefault();
        }

        public async Task<bool> DeleteCliente(string cpf)
        {
            if (cpf==null || cpf=="")
                throw new Exception("O CPF deve ser fornecido para a exclusão!");

            var cliExis = await _tbClientesRepository.SelectClientes(cpf);
            if (cliExis == null || cliExis.Count()==0)
                throw new Exception("Cliente não localizado!");

            var resp = await _tbClientesRepository.DeleteClientes(cpf);        

            if (resp == false)
                throw new Exception("Erro ao excluir Cliente!");

            return resp;
        }

       
    }

}