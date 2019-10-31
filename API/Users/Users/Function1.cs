using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using Users.Models;

namespace Users
{
    public class Function1
    {
        [FunctionName("CreateUser")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log, ExecutionContext context)
        {
            var config = new ConfigurationBuilder()
               .SetBasePath(context.FunctionAppDirectory)
               .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
               .AddEnvironmentVariables()
               .Build();
            
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var data = JsonConvert.DeserializeObject<UserPayload>(requestBody);

            string connectionString = config["ConnectionString"];

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string sqlString = "insert into Users(Nome, Email, Senha) values('" + data.Nome + "', '" + data.Email + "', '" + data.Senha + "')";

                    using (SqlCommand cmd = new SqlCommand(sqlString, conn))
                    {
                        var rows = await cmd.ExecuteNonQueryAsync();
                        log.LogInformation($"{rows} infos");
                    }
                }
                return new OkResult();
            }
            catch(Exception e)
            {
                return new BadRequestObjectResult("connection error");
            }
        }


        [FunctionName("LoginUser")]
        public async Task<IActionResult> Login(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log, ExecutionContext context)
        {
            var config = new ConfigurationBuilder()
               .SetBasePath(context.FunctionAppDirectory)
               .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
               .AddEnvironmentVariables()
               .Build();

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var data = JsonConvert.DeserializeObject<UserPayload>(requestBody);

            string connectionString = config["ConnectionString"];

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string sqlString = ("select Nome, Email, Id from Users where Users.Email = '"+data.Email+"' and Users.Senha = '"+data.Senha+"'");

                using (SqlCommand cmd = new SqlCommand(sqlString, conn))
                {
                    var response = await cmd.ExecuteReaderAsync();

                    if (response.Read())
                    {
                        return new OkObjectResult(new { Nome = response["Nome"], Email = response["Email"], Id = response["Id"]});
                    }
                    else
                    {
                        return new BadRequestObjectResult("Invalid email or password");
                    }
                }
            }
        }

        [FunctionName("EditUser")]
        public async Task<IActionResult> Edit(
           [HttpTrigger(AuthorizationLevel.Function, "put", Route = null)] HttpRequest req,
           ILogger log, ExecutionContext context)
        {
            var config = new ConfigurationBuilder()
               .SetBasePath(context.FunctionAppDirectory)
               .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
               .AddEnvironmentVariables()
               .Build();

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var data = JsonConvert.DeserializeObject<UserPayload>(requestBody);

            string connectionString = config["ConnectionString"];

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string sqlString = ("update Users set Email = '" + data.Email + "', Nome = '" + data.Nome + "', Senha = '" + data.Senha + "' where Id = '" + data.id + "'");

                    using (SqlCommand cmd = new SqlCommand(sqlString, conn))
                    {
                        var rows = await cmd.ExecuteNonQueryAsync();
                        log.LogInformation($"{rows} infos");
                    }
                }
                return new OkObjectResult(data);
            }
            catch
            {
                return new BadRequestObjectResult("User not found");
            }
        }

        [FunctionName("DeleteUser")]
        public async Task<IActionResult> Delete(
           [HttpTrigger(AuthorizationLevel.Function, "put", Route = null)] HttpRequest req,
           ILogger log, ExecutionContext context)
        {
            var config = new ConfigurationBuilder()
               .SetBasePath(context.FunctionAppDirectory)
               .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
               .AddEnvironmentVariables()
               .Build();

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var data = JsonConvert.DeserializeObject<UserPayload>(requestBody);

            string connectionString = config["ConnectionString"];

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string sqlString = ("delete from Users where Id = '" + data.id + "'");

                    using (SqlCommand cmd = new SqlCommand(sqlString, conn))
                    {
                        var rows = await cmd.ExecuteNonQueryAsync();
                        log.LogInformation($"{rows} infos");
                    }
                }

                return new OkObjectResult("Deleted");
            }
            catch
            {
                return new BadRequestObjectResult("ID not found");
            }
        }
    }
}
