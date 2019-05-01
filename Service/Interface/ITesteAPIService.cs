     
using System.Collections.Generic;
using System.Threading.Tasks;
using TesteAPI.Models;
using TesteAPI.Service.Interface;

namespace TesteAPI.Service.Interface
{   
 
    public interface ITesteAPIService
    {
        Task<IEnumerable<TbClientes>> GetCliente(string cpf);
        Task<TbClientes> PostCliente(TbClientes cliente);
        Task<TbClientes> PutCliente(TbClientes cliente);
        Task<bool> DeleteCliente(string cpf);

    }
}