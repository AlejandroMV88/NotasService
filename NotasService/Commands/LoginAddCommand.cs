using NotasService.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace NotasService.Commands
{
    public class LoginAddCommand: CommandBase
    {
		private static String ParName = "name";
		private static String ParLastName = "lastName";
		private static String ParUserName = "userName";
		private static String ParPassword = "password";
		private static String fUpdateTransaction =
			"INSERT INTO AMV.User( name, lastname, userName, password) " +
			" VALUES (" +
			"    @" + ParName + ", " +
			"    @" + ParLastName + ", " +
			"    @" + ParUserName + "," +
			"    @" + ParPassword + ");";

		public User LoginRequest { get; set; }

		public LoginAddCommand(DbProviderFactory dbProvider) :
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
						updateBankPayment.CommandText = fUpdateTransaction;
						updateBankPayment.AddParameterWithValue(ParName, this.LoginRequest.Name);
						updateBankPayment.AddParameterWithValue(ParLastName, this.LoginRequest.Lastname);
						updateBankPayment.AddParameterWithValue(ParUserName, this.LoginRequest.Username);
						updateBankPayment.AddParameterWithValue(ParPassword, this.LoginRequest.Password);
						updateBankPayment.ExecuteNonQuery();
						resp.HttpStatusCode = System.Net.HttpStatusCode.OK;
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
