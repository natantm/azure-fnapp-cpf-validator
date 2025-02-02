using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace validatorcpf
{
    public static class fnvalidacpf
    {
        [FunctionName("fnvalidacpf")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Iniciando a validação do CPF.");   

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            if (data?.cpf == null)
            {
                return new BadRequestObjectResult("Por favor, informe um CPF válido.");
            }

            string cpf = data?.cpf;

            if (!ValidaCPF(cpf))
            {
                return new BadRequestObjectResult("CPF inválido.");
            }

            string responseMessage = "CPF valido, e não se encontra na base de dados de fraudes.";

            return new OkObjectResult(responseMessage);
        }

        public static bool ValidaCPF(string cpf)
        {
            if (string.IsNullOrEmpty(cpf))
            {
                return false;
            }

            if (cpf.Length != 11)
            {
                return false;
            }

            if (cpf == "00000000000" || cpf == "11111111111" || cpf == "22222222222" || cpf == "33333333333" || cpf == "44444444444" || cpf == "55555555555" || cpf == "66666666666" || cpf == "77777777777" || cpf == "88888888888" || cpf == "99999999999")
            {
                return false;
            }

            int[] numbers = new int[11];

            for (int i = 0; i < 11; i++)
            {
                numbers[i] = int.Parse(cpf[i].ToString());
            }

            int sum = 0;

            for (int i = 0; i < 9; i++)
            {
                sum += numbers[i] * (10 - i);
            }

            int result = sum % 11;

            if (result == 1 || result == 0)
            {
                if (numbers[9] != 0)
                {
                    return false;
                }
            }
            else
            {
                if (numbers[9] != 11 - result)
                {
                    return false;
                }
            }

            sum = 0;

            for (int i = 0; i < 10; i++)
            {
                sum += numbers[i] * (11 - i);
            }

            result = sum % 11;

            if (result == 1 || result == 0)
            {
                if (numbers[10] != 0)
                {
                    return false;
                }
            }
            else
            {
                if (numbers[10] != 11 - result)
                {
                    return false;
                }
            }

            return true;
        }
    } 
}
