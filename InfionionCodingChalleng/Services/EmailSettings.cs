using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InfionionCodingChalleng.Services
{
    public class EmailSettings
    {
        public string MailServer { get; set; }
    public int MailPort { get; set; }
    public bool UseSsl { get; set; }
    public string SenderName { get; set; }
    public string SenderEmail { get; set; }
    public string SenderPassword { get; set; }
    }
}