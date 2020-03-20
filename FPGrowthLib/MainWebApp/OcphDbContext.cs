using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainWebApp.Models;
using Microsoft.Extensions.Options;
using Ocph.DAL.Provider.MySql;
using Ocph.DAL.Repository;

namespace MainWebApp {

    public class OcphDbContext : MySqlDbConnection, IDisposable {
        public OcphDbContext (IOptions<AppSettings> setting) {
            this.ConnectionString = setting.Value.ConnectionString;
        }

        public IRepository<User> Users { get { return new Repository<User> (this); } }
        public IRepository<Role> Roles { get { return new Repository<Role> (this); } }
        public IRepository<UserInRole> UserInRoles { get { return new Repository<UserInRole> (this); } }

    }
}