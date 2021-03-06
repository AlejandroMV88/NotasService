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
    public class NotasUpdateService : INotasUpdateService
    {
        public ServiceResponse Execute(Notas notas)
        {
            ServiceResponse resp = new ServiceResponse();
            DbProviderFactory provider = NpgsqlFactory.Instance;
            NotasUpdateCommand command = new NotasUpdateCommand(provider);
            command.NotasRequest = notas;
            var respCommand = command.Execute();
            if (respCommand.HttpStatusCode != System.Net.HttpStatusCode.OK)
            {
                resp.ErrorMessage = respCommand.ErrorMessage;
                resp.ErrorCode = respCommand.HttpStatusCode.ToString();
            }
            return resp;
        }
    }
}
