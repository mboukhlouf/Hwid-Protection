using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HwidProtectionServer.Models
{
    [Serializable]
    public class User
    {
        public int Id { get; set; }

        public String Username { get; set; }

        public String Password { get; set; }
    }
}
