namespace TesteAPI.Models
{
    public class TbClientes
    {
        public long? IdCliente {get;set;}
        public string Nome {get;set;}
        public string CPF{get;set;}
        public string Sexo {get;set;}
        public long IdTipoCLiente{get;set;}
        public long IdSituacaoCliente{get;set;}

    }
}