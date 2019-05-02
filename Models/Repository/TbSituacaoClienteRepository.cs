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
    public class TbSituacaoClienteRepository
    {
        private string stringConnection;
        private readonly IConfiguration _configuration;

        public TbSituacaoClienteRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            stringConnection = _configuration.GetConnectionString("DBConnection");
        }

        public async Task<IEnumerable<TbSituacaoCliente>> SituacaoCliente()
        {
           
            IEnumerable<TbSituacaoCliente> retorno;

            using (IDbConnection db = new SqlConnection(stringConnection))
            {
                var parameters = new DynamicParameters();
                retorno = await db.QueryAsync<TbSituacaoCliente>("SP_TB_SITUACAO_CLIENTE_READ",parameters, 
                                    commandType: CommandType.StoredProcedure);

            }        

            return retorno;
        }
    }
}