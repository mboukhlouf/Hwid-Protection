using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HwidProtectionServer.Models
{
    public class HwidInfo
    {
        public int Id { get; set; }

        [RegularExpression(@"[0-9A-F]{40}", ErrorMessage = "Hwid not valid")]
        public string Value { get; set; }
    }
}
