namespace GlobalBankApi.Models
{
    public class ContaBancaria
    {
        public int Id {get; set;}

        public required string Titular {get; set;}

        public required string NumeroConta {get; set;}
        
        public decimal Saldo { get; set; }
        public required string TipoConta {get; set;}

    }
}