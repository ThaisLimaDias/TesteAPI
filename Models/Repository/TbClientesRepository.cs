using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using TesteAPI.Models;

namespace TesteAPI.Models.Repository
{
    public class TbClientesRepository
    {
        private string stringConnection;
        private readonly IConfiguration _configuration;

        public TbClientesRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            stringConnection = _configuration.GetConnectionString("DBConnection");
        }

        public async Task<IEnumerable<TbClientes>> SelectClientes(string CPF)
        {
           
            IEnumerable<TbClientes> retorno;

            using (IDbConnection db = new SqlConnection(stringConnection))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@CPF", CPF);
                retorno = await db.QueryAsync<TbClientes>("SP_TB_CLIENTES_READ",parameters, 
                                    commandType: CommandType.StoredProcedure);

            }        

            return retorno;
        }

        public async Task<bool> DeleteClientes(string cpf)
        {
           int retorno = 0;

            using (IDbConnection db = new SqlConnection(stringConnection))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@CPF", cpf);
                parameters.Add("@OUTSTATUS", retorno);
                retorno = await db.ExecuteAsync("SP_TB_CLIENTES_DELETE",parameters, 
                                    commandType: CommandType.StoredProcedure);

            }

            if(retorno >0)
                return true;
            else
                return false;
        }

        public async Task<bool> UpdateClientes(TbClientes cliente)
        {
           int retorno = 0;

            using (IDbConnection db = new SqlConnection(stringConnection))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@ID", cliente.IdCliente);
                parameters.Add("@NOME", cliente.Nome);
                parameters.Add("@CPF", cliente.CPF);
                parameters.Add("@SEXO", cliente.Sexo);
                parameters.Add("@TIPOCLIENTE", cliente.IdTipoCLiente);
                parameters.Add("@SITCLIENTE", cliente.IdSituacaoCliente);
                parameters.Add("@OUTSTATUS", retorno);
                retorno = await db.ExecuteAsync("SP_TB_CLIENTES_UPDATE",parameters, 
                                    commandType: CommandType.StoredProcedure);

            }

            if(retorno >0)
                return true;
            else
                return false;
        }

        public async Task<bool> CreateClientes(TbClientes cliente)
        {
            int retorno = 0;
            
            using (IDbConnection db = new SqlConnection(stringConnection))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@NOME", cliente.Nome);
                parameters.Add("@CPF", cliente.CPF);
                parameters.Add("@SEXO", cliente.Sexo);
                parameters.Add("@TIPOCLIENTE", cliente.IdTipoCLiente);
                parameters.Add("@SITCLIENTE", cliente.IdSituacaoCliente);
                parameters.Add("@OUTSTATUS", retorno);
                retorno = await db.ExecuteAsync("SP_TB_CLIENTES_CREATE",parameters, 
                                    commandType: CommandType.StoredProcedure);

            }

            if(retorno >0)
                return true;
            else
                return false;
        }
    }
}