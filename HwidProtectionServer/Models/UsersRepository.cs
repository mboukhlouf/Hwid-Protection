using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HwidProtectionServer.Models
{
    public static class UsersRepository
    {
        public static IEnumerable<User> Users { get; }

        static UsersRepository()
        {
            Users = new List<User>
            {
                new User {Id = 0, Username = "mrpromo", Password = "29S3XkEvCF8Lt9Mq"}
            };
        }
    }
}
