using NotasService.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace NotasService.Commands
{
    public class LoginCommand: CommandBase
    {
		//private static String ParId = "Id";
		private static String ParUsername = "Username";
		private static String ParPassword = "Password";
		private static String fUpdateTransaction =
			"SELECT id  from amv.user WHERE username =" + " @" + ParUsername + " AND password =" + " @" + ParPassword + ";";
		public User LoginRequest { get; set; }
		//public CommandResponse resp { get; set; }

		public LoginCommand(DbProviderFactory dbProvider) :
			base(dbProvider)
		{
			this.RDSProvider = dbProvider;
		}
		public override CommandResponse Execute()
		{
			CommandResponse resp = new CommandResponse();

			try
			{
				using (DbConnection connection = this.RDSProvider.CreateConnection())
				{
					connection.ConnectionString = this.ConnectionString;
					connection.Open();
					using (DbCommand updateBankPayment = connection.CreateCommand())
					{
						//updateBankPayment.AddParameterWithValue(ParId, this.LoginRequest.Id);
						updateBankPayment.AddParameterWithValue(ParUsername, this.LoginRequest.Username);
						updateBankPayment.AddParameterWithValue(ParPassword, this.LoginRequest.Password);
						updateBankPayment.CommandText = fUpdateTransaction;
						object id = updateBankPayment.ExecuteScalar();


						if (id != null)
						{
							resp.Id = Convert.ToInt32(id);
							resp.HttpStatusCode = HttpStatusCode.Found;
						}

						if (resp.Id == 0)
						{
							resp.HttpStatusCode = HttpStatusCode.NotFound;
						}
						
					}
				}
			}
			catch (Exception e)
			{
				resp.ErrorMessage = e.Message;
				resp.HttpStatusCode = HttpStatusCode.InternalServerError;
			}
			return resp;
		}
	}
}
