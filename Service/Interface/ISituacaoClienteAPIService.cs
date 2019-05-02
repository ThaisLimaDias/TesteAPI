     
using System.Collections.Generic;
using System.Threading.Tasks;
using TesteAPI.Models;
using TesteAPI.Service.Interface;

namespace TesteAPI.Service.Interface
{   
 
    public interface ISituacaoClienteAPIService
    {
        Task<IEnumerable<TbSituacaoCliente>> GetSituacaoCliente();

    }
}