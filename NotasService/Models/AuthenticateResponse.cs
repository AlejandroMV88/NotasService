using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotasService.Models
{
    public class AuthenticateResponse
    {
		public Int32 Id { get; set; }
		public String Name { get; set; }
		public String LastName { get; set; }
        public String UserName { get; set; }
        public String Token { get; set; }
		public String RefreshToken { get; set; }

		public AuthenticateResponse(User user, String token, String refreshToken)
		{
			this.Id = user.Id;
			this.Name = user.Name;
			this.LastName = user.Lastname;
			this.UserName = user.Username;
			this.Token = token;
			this.RefreshToken = refreshToken;
		}
	}
}
