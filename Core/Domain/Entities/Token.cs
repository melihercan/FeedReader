using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Token
    {
        public DateTime Timestamp { get; set; }

        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public string IdToken { get; set; }

        public DateTime? AccessTokenExpiresIn { get; set; }
        public DateTime? RefreshTokenExpiresIn { get; set; }
        public DateTime? IdTokenExpiresIn { get; set; }
    }
}
