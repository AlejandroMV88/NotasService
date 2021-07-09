using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotasService.Models
{
    public class JwtAuthResult
    {
		[JsonProperty("accessToken")]
		//[JsonPropertyName("accessToken")]
		public string AccessToken { get; set; }

		//[JsonPropertyName("refreshToken")]
		[JsonProperty("refreshToken")]
		public RefreshToken RefreshToken { get; set; }
	}
}
