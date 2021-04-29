using System;
using System.Runtime.Serialization;

namespace DIO.Bank
{
    [DataContract]
    public class Conta
    {
        [DataMember]
        private TipoConta TipoConta { get; set; }

        [DataMember]
        private double Saldo { get; set; }

        [DataMember]
        private double Credito { get; set; }

        [DataMember]
        private double CreditoAtual { get; set; }
        
        [DataMember]
        private string Nome { get; set; }

        public Conta(TipoConta TipoConta, double Saldo, double Credito, string Nome)
        {
            this.TipoConta = TipoConta;
            this.Saldo = Saldo;
            this.Credito = Credito;
            this.CreditoAtual = Credito;
            this.Nome = Nome;
        }

        public bool Sacar(double valorSaque)
        {
            if(valorSaque > (this.Saldo + this.CreditoAtual))
            {
                Console.WriteLine("Não há saldo suficiente para esta operação!");
                return false;
            }

            this.Saldo -= valorSaque;

            if(this.Saldo < 0)
            {
                this.CreditoAtual += this.Saldo;
                this.Saldo = 0;
            }

            Console.WriteLine("O saldo atual da conta de {0} é de R$ {1:0.00} e o crédito é de R$ {2:0.00}", this.Nome, this.Saldo, this.CreditoAtual);

            return true;
        }

        public void Depositar(double valorDeposito)
        {
            this.Saldo += valorDeposito;

            if(this.CreditoAtual < this.Credito)
            {
                var diferenca = this.Credito - this.CreditoAtual;

                this.Saldo -= diferenca;
                this.CreditoAtual += diferenca;
            }

            Console.WriteLine("O saldo atual da conta de {0} é de R$ {1:0.00} e o crédito é de R$ {2:0.00}", this.Nome, this.Saldo, this.CreditoAtual);
        }

        public void Transferir(double valorTransferencia, Conta contaDestino)
        {
            if(Sacar(valorTransferencia))
            {
                contaDestino.Depositar(valorTransferencia);
            }
        }

        public override string ToString()
        {
            string retorno = "";
                   retorno += "Correntista: " + this.Nome + " | ";
                   retorno +="Tipo de Conta: " + this.TipoConta + " | ";
                   retorno +="Saldo: " + this.Saldo + " | ";
                   retorno +="Credito: " + this.CreditoAtual;
                   
            return retorno;
        }
    }
}