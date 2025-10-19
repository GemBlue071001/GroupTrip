﻿

using Microsoft.AspNetCore.Identity;

namespace GT.AuthService.Domain.Entities
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        /// <summary>
        /// Tên người dùng
        /// </summary>
        public string? FullName { get; set; }

        /// <summary>
        /// Mật khẩu người dùng
        /// </summary>
        public string Status {  get; set; }
        public string? BankAccount {get; set; }
        public string? BankName { get; set; }
        /// <summary>
        /// Ngày tạo tài khoản
        /// </summary>
        public DateTimeOffset CreatedTime { get; set; }

        /// <summary>
        /// Lần cuối cập nhật tài khoản
        /// </summary>
        public DateTimeOffset LastUpdatedTime { get; set; }

        public string? Gender { get; set; }
        public DateTime? DayOfBirth { get; set; }
        /// <summary>
        /// Ngày xóa tài khoản (nếu chưa xóa thì 
        /// </summary>
        public DateTimeOffset? DeletedTime { get; set; }
        public ApplicationUser()
        {
            CreatedTime = DateTime.Now;
            LastUpdatedTime = CreatedTime;
        }
    }
}
