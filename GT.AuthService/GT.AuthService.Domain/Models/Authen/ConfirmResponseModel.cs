using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GT.AuthService.Domain.Models.Authen
{
    public class ConfirmResponseModel
    {
        public string UserId { get; set; }
        public string Token { get; set; }
        public string Message { get; set; }
    }
}
