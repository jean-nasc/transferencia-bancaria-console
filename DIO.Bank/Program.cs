using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace DIO.Bank
{
    class Program
    {
        private static List<Conta> listaDeContas = new List<Conta>();
        private readonly static string caminhoData = @"./Data/contas.json";
        
        static void Main(string[] args)
        {
            if(File.Exists(caminhoData) && !string.IsNullOrEmpty(File.ReadAllText(caminhoData)))
            {
                RecuperarListaJson();
            }

            string opcaoUsuario = ObterOpcaoUsuario();
            
            while(opcaoUsuario != "X")
            {
                switch (opcaoUsuario)
                {
                    case "1":
                        ListarContas();
                        break;
                    case "2":
                        InserirConta();
                        break;
                    case "3":
                        TransferirValor();
                        break;
                    case "4":
                        SacarValor();
                        break;
                    case "5":
                        DepositarValor();
                        break;
                    case "6":
                        SalvarLista();
                        break;
                    case "C":
                        Console.Clear();
                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }

                opcaoUsuario = ObterOpcaoUsuario();
            }

            Console.WriteLine("\nObrigado por utilizar nossos serviços!\n");
        }

        private static void ListarContas()
        {
            if(listaDeContas.Count == 0)
            {
                Console.WriteLine("\nNão há contas cadastradas!\n");
                return;
            }

            for(int i=0; i<listaDeContas.Count; i++)
            {
                Console.WriteLine("#{0} - {1}", i, listaDeContas[i].ToString());
            }
        }

        private static void InserirConta()
        {
            Console.Write("Correntista: ");
            string Nome = Console.ReadLine();

            Console.Write("Saldo: ");
            double Saldo = Convert.ToDouble(Console.ReadLine());

            Console.Write("Crédito: ");
            double Credito = Convert.ToDouble(Console.ReadLine());

            Console.Write("Tipo de Conta (1 - Física | 2 - Jurídica): ");
            int TipoConta = int.Parse(Console.ReadLine());

            Conta conta = new Conta
            (
                Nome: Nome,
                TipoConta: (TipoConta)TipoConta,
                Saldo: Saldo,
                Credito: Credito
            );

            listaDeContas.Add(conta);
            
            Console.WriteLine("\nConta criada com sucesso!\n");
        }

        private static void TransferirValor()
        {
            ListarContas();
            Console.Write("Digite o código da conta de origem:");
            int origem = int.Parse(Console.ReadLine());

            Console.Write("Digite o código da conta de destino:");
            int destino = int.Parse(Console.ReadLine());

            Console.Write("Digite o valor da transferência:");
            double valor = double.Parse(Console.ReadLine());

            listaDeContas[origem].Transferir(valor, listaDeContas[destino]);

            Console.WriteLine("\nTransferência realizada com sucesso!\n");
        }

        private static void SacarValor()
        {
            ListarContas();
            Console.Write("Digite o código de sua conta: ");
            int codigo = int.Parse(Console.ReadLine());

            Console.Write("Digite o valor do saque: ");
            double valor = double.Parse(Console.ReadLine());

            listaDeContas[codigo].Sacar(valor);

            Console.WriteLine("\nSaque realizado com sucesso!\n");
        }
        private static void DepositarValor()
        {
            ListarContas();
            Console.Write("Digite o código de sua conta: ");
            int codigo = int.Parse(Console.ReadLine());

            Console.Write("Digite o valor do depósito: ");
            double valor = double.Parse(Console.ReadLine());

            listaDeContas[codigo].Depositar(valor);

            Console.WriteLine("\nDepósito realizado com sucesso!\n");
        }

        private static void SalvarLista()
        {
            string json = JsonConvert.SerializeObject(listaDeContas, Formatting.Indented);
            File.WriteAllText(caminhoData, json);
        }

        private static void RecuperarListaJson()
        {
            string json = File.ReadAllText(caminhoData);
            listaDeContas = JsonConvert.DeserializeObject<List<Conta>>(json);
        }

        private static string ObterOpcaoUsuario()
        {
            Console.WriteLine("___DIO Bank ao seu dispor!___");
            Console.WriteLine("___Escolha a opção desejada:___");

            Console.WriteLine("1 - Listar contas");
            Console.WriteLine("2 - Inserir nova conta");
            Console.WriteLine("3 - Transferir");
            Console.WriteLine("4 - Sacar");
            Console.WriteLine("5 - Depositar");
            Console.WriteLine("6 - Salvar lista");
            Console.WriteLine("C - Limpar tela");
            Console.WriteLine("X - Sair");

            string opcao = Console.ReadLine().ToUpper();

            return opcao;
        }
    }
}
