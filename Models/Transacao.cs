namespace GlobalBankApi.Models
{
    public class Transacao
    {
        public int Id {get; set;}

        public required string ContaId {get; set;}

        public required string Tipo {get; set;}
        
        public decimal Valor { get; set; }
        public DateTime DataTransacao {get; set;}

    }
}