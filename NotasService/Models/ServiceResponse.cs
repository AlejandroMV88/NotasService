using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotasService.Models
{
	public class ServiceResponse
	{
		/// <summary>
		/// Cóodigo de error
		/// </summary>
		public String ErrorCode { get; set; }
		/// <summary>
		/// Mensaje del error
		/// </summary>
		public String ErrorMessage { get; set; }
		/// <summary>
		/// Datos del usuario, si va nulo ocurrió un error
		/// </summary>
		public List<Notas> notas { get; set; }
		
	}
}
