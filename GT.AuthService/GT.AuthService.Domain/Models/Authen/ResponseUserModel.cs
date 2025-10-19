using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GT.AuthService.Domain.Models.Authen
{
    public class ResponseUserModel
    {
        public string? FullName { get; set; }

        /// <summary>
        /// Mật khẩu người dùng
        /// </summary>
        public string Status { get; set; }
        public string? BankAccount { get; set; }
        public string? BankName { get; set; }
        /// <summary>
        /// Ngày tạo tài khoản
        /// </summary>
        public DateTimeOffset CreatedTime { get; set; }

        public string? Gender { get; set; }
        public DateTime? DayOfBirth { get; set; }
    }
}
