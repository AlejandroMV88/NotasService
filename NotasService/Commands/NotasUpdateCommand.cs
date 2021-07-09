using NotasService.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace NotasService.Commands
{
    public class NotasUpdateCommand: CommandBase
	{
		private static String ParId = "Id";
		private static String ParTitle = "Title";
		private static String ParContent = "Content";
		//private static String ParCreationDate = "CreationDate";
		private static String ParUpdateDate = "Update_Date";
		private static String fUpdateTransaction =
			"UPDATE AMV.NOTAS SET  titulo = " +
			"    @" + ParTitle + ", " + "content = " +
			"    @" + ParContent + ", " + "update_date = " +
			"    @" + ParUpdateDate +" WHERE id = " +
			"    @" + ParId + ";";
		public Notas NotasRequest { get; set; }

		public NotasUpdateCommand(DbProviderFactory dbProvider) :
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
						updateBankPayment.AddParameterWithValue(ParTitle, this.NotasRequest.Titulo);
						updateBankPayment.AddParameterWithValue(ParContent, this.NotasRequest.Content);
						//updateBankPayment.AddParameterWithValue(ParCreationDate, DateTime.Now);
						updateBankPayment.AddParameterWithValue(ParUpdateDate, DateTime.Now);
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
