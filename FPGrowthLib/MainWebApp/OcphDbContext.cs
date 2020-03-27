using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainWebApp.Models;
using MainWebApp.Models.Data;
using Microsoft.Extensions.Options;
using Ocph.DAL.Provider.MySql;
using Ocph.DAL.Repository;

namespace MainWebApp {

    public class OcphDbContext : MySqlDbConnection, IDisposable {
        public OcphDbContext (IOptions<AppSettings> setting) {
            this.ConnectionString = setting.Value.ConnectionString;
        }

        public IRepository<User> Users { get { return new Repository<User> (this); } }
        public IRepository<Kategori> Kategori { get { return new Repository<Kategori> (this); } }
        public IRepository<Manajemen_Transaksi> ManagementTransaksi { get { return new Repository<Manajemen_Transaksi> (this); } }

    }
}