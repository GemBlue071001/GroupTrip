﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GT.AuthService.Domain.Base
{
    public class EmailSettings
    {
        public string SmtpServer { get; set; } = "";
        public int SmtpPort { get; set; }
        public string SmtpUser { get; set; } = "";
        public string SmtpPass { get; set; } = "";
        public string FromName { get; set; } = "";
        public string FromEmail { get; set; } = "";
    }
}
