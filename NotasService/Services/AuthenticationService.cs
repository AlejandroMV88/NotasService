using NotasService.Commands;
using NotasService.Middleware;
using NotasService.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace NotasService.Services
{
    public class AuthenticationService : IAuthenticationService
    {

        private readonly IJwtAuthManager jwtAuthManager;

        public AuthenticationService(IJwtAuthManager jwtAuthManager)
        {
            this.jwtAuthManager = jwtAuthManager;
        }

        public AuthenticateResponse Login(User user)
        {
            if (user != null)
            {
                String data = user.Username + user.Password;
                String hashBase64 = "";
                byte[] passwordEncoded = Encoding.ASCII.GetBytes(data);
                using (SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider())
                {
                    byte[] sha1data = sha1.ComputeHash(passwordEncoded);
                    hashBase64 = Convert.ToBase64String(sha1data);
                }

                if (!String.IsNullOrEmpty(hashBase64))
                {
                    DbProviderFactory provider = NpgsqlFactory.Instance;
                    LoginCommand authentication = new LoginCommand(provider);
                    
                        CommandResponse resp = new CommandResponse();


                    authentication.LoginRequest = new User()
                    {
                        Username = user.Username,
                        Password = hashBase64

                    };
                    resp = authentication.Execute();

                    if (resp.Id > 0)
                    {
                        user.Id = Convert.ToInt32(resp.Id);
                        var jwtAuthResult = this.GenerateToken(user);
                        return new AuthenticateResponse(user, jwtAuthResult.AccessToken, jwtAuthResult.RefreshToken.TokenString);
                    }
                }
            }
            return null;
        }
        public String MessageGet()
        {
            return "Información sensible";
        }

        private JwtAuthResult GenerateToken(User user)
        {
            var claims = new[]
                {
                     //new Claim(ClaimTypes.Name,users.Username),
                     //new Claim(ClaimTypes.Email, users.Email),
                     new Claim("id", user.Id.ToString())
                };
            var jwtResult = this.jwtAuthManager.GenerateTokens(user.Username, claims, DateTime.Now);
            return jwtResult;
        }

    }
}
