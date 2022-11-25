using IntegrationCelcoin.Models;
using IntegrationCelcoin.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace TesteCelcoin
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Digite uma opção: (1-Recarga 2-Pix)");
            var option = Console.ReadLine();

            if (option == "1")
                CreateRecharge();
            else
                Console.WriteLine("Opão inválida");

            Console.ReadKey();
        }

        private static void CreateRecharge()
        {
            Console.WriteLine("Digite um tipo: (0-Todos 1-PIN 2-Online)");
            var type = Console.ReadLine();

            Console.WriteLine("Digite uma categoria: (0-Todos 1-Telefone 2-Jogos 3-TV 4-Transporte 5-Conteúdo digital)");
            var category = Console.ReadLine();

            var tokenRechargeCelcoinService = new TokenRechargeCelcoinService();
            var resultToken = tokenRechargeCelcoinService.GetToken();

            if (resultToken.First().Key)
            {
                var accessToken = resultToken.First().Value;

                var rechargeService = new RechargeCelcoinService(accessToken);
                var resultProviders = rechargeService.GetRechargeProviders(Convert.ToInt32(type), Convert.ToInt32(category));

                if(string.IsNullOrEmpty(resultProviders.First().Key) && resultProviders.First().Value != null && resultProviders.First().Value.Any())
                {
                    var formattedProviders = FormatValueProviders(resultProviders.First().Value);

                    Console.WriteLine("Digite a opção do provider: " + formattedProviders);
                    var idProvider = Console.ReadLine();

                    var providersValues = rechargeService.GetRechargeProvidersValue(Convert.ToInt32(idProvider));
                    var formattedProvidersValue = FormatValueProvidersValues(providersValues.First().Value);

                    Console.WriteLine(formattedProvidersValue);
                    Console.WriteLine(Environment.NewLine);

                    Console.WriteLine("Digite o CPF:");
                    var cpf = Console.ReadLine();

                    Console.WriteLine("Digite o código do pedido:");
                    var orderId = Console.ReadLine();

                    Console.WriteLine("Digite o valor da recarga:");
                    var price = Console.ReadLine();

                    var objRecharge = GetObjectRecharge(cpf, orderId, Convert.ToInt32(idProvider), Convert.ToDouble(price));
                    var resultCreateRecharge = rechargeService.CreateRecharge(objRecharge);

                    if(string.IsNullOrEmpty(resultCreateRecharge.First().Key))
                    {
                        var idTransaction = resultCreateRecharge.First().Value.transactionId;

                        Console.WriteLine("Confirma a recarga? (1-Sim 2-Não)");

                        var resultConfirmRecharge = rechargeService.ConfirmRecharge(idTransaction, new RechargeConfimModel()
                        {
                            externalNSU = objRecharge.externalNsu,
                            externalTerminal = objRecharge.externalTerminal
                        });

                        if (resultConfirmRecharge.First().Key)
                            Console.WriteLine("Recarga efetuada com sucesso.");
                        else
                            Console.WriteLine(resultConfirmRecharge.First().Value);
                    }
                    else
                        Console.WriteLine(resultCreateRecharge.First().Key);
                }
                else
                    Console.WriteLine(resultProviders.First().Key);
            }
            else
                Console.WriteLine(resultToken.First().Value);
        }

        private static string FormatValueProviders(List<ResultSearchRechargeProviderCelcoinProvider> providers)
        {
            var result = new StringBuilder();

            foreach(var item in providers)
            {
                result.Append($"{item.providerId} - {item.name} | ");
            }

            return result.ToString();
        }

        private static string FormatValueProvidersValues(List<ResultSearchRechargeProviderValueCelcoinValue> providersValues)
        {
            var result = new StringBuilder();

            foreach (var item in providersValues)
            {
                result.Append($"{item.productName} - valor min: {item.minValue} - valor máx: {item.maxValue} | ");
            }

            return result.ToString();
        }

        private static RechargeCelcoinModel GetObjectRecharge(string cpf, string orderId, int provider, double valueRecharge)
        {
            var objRecharge = new RechargeCelcoinModel();
            objRecharge.cpfCnpj = cpf;
            objRecharge.externalNsu = orderId;
            objRecharge.externalTerminal = cpf;

            objRecharge.phone = new RechargeCelcoinPhone();
            objRecharge.phone.countryCode = 55;
            objRecharge.phone.stateCode = 11;
            objRecharge.phone.number = 999999999;

            objRecharge.providerId = provider;
            objRecharge.signerCode = cpf;

            objRecharge.topupData = new RechargeCelcoinTopupData();
            objRecharge.topupData.value = valueRecharge;
            
            return objRecharge;
        }
    }
}
