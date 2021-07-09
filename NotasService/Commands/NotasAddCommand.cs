using NotasService.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace NotasService.Commands
{
	public class NotasAddCommand : CommandBase
	{
		private static String ParTitulo = "Titulo";
		private static String ParContent = "Content";
		private static String ParCreationDate = "Creation_Date";
		private static String ParUpdateDate = "Update_Date";
		private static String fUpdateTransaction =
			"INSERT INTO AMV.NOTAS( titulo, content, creation_date, update_date) " +
			" VALUES (" +
			"    @" + ParTitulo + ", " +
			"    @" + ParContent + ", " +
			"    @" + ParCreationDate + "," +
			"    @" + ParUpdateDate + ");";

		public Notas NotasRequest { get; set; }

		public NotasAddCommand(DbProviderFactory dbProvider) :
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
						updateBankPayment.AddParameterWithValue(ParTitulo, this.NotasRequest.Titulo);
						updateBankPayment.AddParameterWithValue(ParContent, this.NotasRequest.Content);
						updateBankPayment.AddParameterWithValue(ParCreationDate, DateTime.Now);
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
