using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace HwidProtectionServer.Models
{
    public class HwidProtectionServerContext : DbContext
    {
        public HwidProtectionServerContext (DbContextOptions<HwidProtectionServerContext> options)
            : base(options)
        {
        }

        public DbSet<HwidProtectionServer.Models.HwidInfo> HwidInfo { get; set; }
    }
}
