using Newtonsoft.Json;
using NotasService.Commands;
using NotasService.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace NotasService.Services
{
    public class NotasGetService : INotasGetService
    {
        public ServiceResponse Execute()
        {
            ServiceResponse resp = new ServiceResponse();
            DbProviderFactory provider = NpgsqlFactory.Instance;
            NotasGetCommand command = new NotasGetCommand(provider);
            var respCommand = command.Execute();
            if (respCommand.HttpStatusCode == System.Net.HttpStatusCode.OK)
            {
                resp.notas = JsonConvert.DeserializeObject<List<Notas>>(respCommand.Content);
            }
            else
            {
                resp.ErrorMessage = respCommand.ErrorMessage;
                resp.ErrorCode = respCommand.HttpStatusCode.ToString();
            }
            return resp;
        }
    }


}
