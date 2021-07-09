using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace NotasService.Commands
{
	public static class DbCommandExtension
	{
		public static DbParameter AddParameterWithValue(this DbCommand command, String parameterName, Object value)
		{
			DbParameter parameter = command.CreateParameter();
			parameter.ParameterName = parameterName;
			parameter.Value = value;
			command.Parameters.Add(parameter);
			return parameter;
		}
	}
}
