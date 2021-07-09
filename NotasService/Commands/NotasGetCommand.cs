using NotasService.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace NotasService.Commands
{
	public class NotasGetCommand : CommandBase
	{
		private static String fUpdateTransaction = "SELECT * FROM AMV.NOTAS";
		public NotasGetCommand(DbProviderFactory dbProvider) :
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

						updateBankPayment.Prepare();
						using (DbDataReader dataReader = updateBankPayment.ExecuteReader())
						{
							resp.Content = dataReader.ToJson();
						}
					}
				}
				if (String.IsNullOrEmpty(resp.Content) || resp.Content == "[]")
				{
					resp.HttpStatusCode = System.Net.HttpStatusCode.NotFound;
				}
				else
				{
					resp.HttpStatusCode = System.Net.HttpStatusCode.OK;
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
