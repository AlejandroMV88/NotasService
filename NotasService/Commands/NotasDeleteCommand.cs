using NotasService.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace NotasService.Commands
{
    public class NotasDeleteCommand : CommandBase
    {

		private static String ParId = "Id";
		private static String fUpdateTransaction =
			"DELETE FROM AMV.NOTAS WHERE id = "+" @" + ParId + ";"; 

		public Notas NotasRequest { get; set; }

		public NotasDeleteCommand(DbProviderFactory dbProvider) :
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
						updateBankPayment.AddParameterWithValue(ParId, this.NotasRequest.Id);
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
