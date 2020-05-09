using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
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

        public OcphDbContext (string constring) {
            this.ConnectionString = constring;
        }

        public IRepository<User> Users { get { return new Repository<User> (this); } }
        public IRepository<Kategori> Kategori { get { return new Repository<Kategori> (this); } }
        public IRepository<Manajemen_Transaksi> ManagementTransaksi { get { return new Repository<Manajemen_Transaksi> (this); } }
        public IRepository<Barang> Barang { get { return new Repository<Barang> (this); } }
        public IRepository<Penjual> Penjual { get { return new Repository<Penjual> (this); } }
        public IRepository<Pembeli> Pembeli { get { return new Repository<Pembeli> (this); } }
        public IRepository<Order> Order { get { return new Repository<Order> (this); } }
        public IRepository<Transaksi> Transaksi { get { return new Repository<Transaksi> (this); } }
        public IRepository<Pembayaran> Pembayaran { get { return new Repository<Pembayaran> (this); } }
        public IRepository<Komentar> Komentar { get { return new Repository<Komentar> (this); } }
        public IRepository<Pesan> Chat { get { return new Repository<Pesan> (this); } }
        public IRepository<Pengiriman> Pengiriman { get { return new Repository<Pengiriman> (this); } }
        public IRepository<Parameter> Parameters { get { return new Repository<Parameter> (this); } }

        public IEnumerable<dynamic> SelectDynamic (string sql) {

            var command = this.CreateCommand ();
            command.CommandText = sql;
            command.CommandType = System.Data.CommandType.Text;
            using (var reader = command.ExecuteReader ()) {
                var names = Enumerable.Range (0, reader.FieldCount).Select (reader.GetName).ToList ();
                foreach (IDataRecord record in reader as IEnumerable) {
                    var expando = new Dictionary<string, object> ();
                    foreach (var name in names)
                        expando[name] = record[name];

                    yield return expando;
                }
            }

        }

    }
}