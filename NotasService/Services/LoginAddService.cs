using NotasService.Commands;
using NotasService.Models;
using NotasService.Utils;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace NotasService.Services
{
    public class LoginAddService : ILoginAddService
    {
        public ServiceResponse Execute(User user)
        { 
            ServiceResponse resp = new ServiceResponse();
            DbProviderFactory provider = NpgsqlFactory.Instance;
            LoginAddCommand command = new LoginAddCommand(provider);
            command.LoginRequest = user;
            user.Password = Encriptar.EncriptarPassword(user.Password);
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
