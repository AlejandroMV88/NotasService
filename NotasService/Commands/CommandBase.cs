using NotasService.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace NotasService.Commands
{
	public abstract class CommandBase
	{
		private DbProviderFactory fRDSProvider;
		private DbConnection fRDSConnection;
		private String fConnectionString;

		public CommandBase(DbProviderFactory dbProvider)
		{
			this.fRDSProvider = dbProvider;
			this.fConnectionString = "Username=master;Password=netc0re123;Database=DANNACH;Host=database-1-instance-1.csjg9zaanusy.us-east-1.rds.amazonaws.com;Port=5432";
		}

		public abstract CommandResponse Execute();

		protected void Dispose(Boolean disposing)
		{
			if (disposing && this.fRDSConnection != null)
			{
				this.fRDSConnection.Dispose();
				this.fRDSConnection = null;
			}
		}

		protected DbConnection RDSConnection
		{
			get
			{
				return this.fRDSConnection;
			}
			set
			{
				this.fRDSConnection = value;
			}
		}

		protected DbProviderFactory RDSProvider
		{
			get
			{
				return this.fRDSProvider;
			}
			set
			{
				this.fRDSProvider = value;
			}
		}

		protected String ConnectionString
		{
			get
			{
				return this.fConnectionString;
			}
			set
			{
				this.fConnectionString = value;
			}
		}

	}
}
