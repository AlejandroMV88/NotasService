using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotasService.Models
{
    public class JwtTokenConfig
    {
		/// <summary>
		/// Llave secreta
		/// </summary>
		public String Secret { get; set; }
		/// <summary>
		/// El emisor
		/// </summary>
		public String Issuer { get; set; }
		/// <summary>
		/// El destino
		/// </summary>
		public String Audience { get; set; }
		/// <summary>
		/// Tiempo de expiración del token
		/// </summary>
		public Int32 AccessTokenExpiration { get; set; }
		/// <summary>
		/// Tiempo para el token de refresco
		/// </summary>
		public Int32 RefreshTokenExpiration { get; set; }
	}
}
