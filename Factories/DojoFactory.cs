using System.Collections.Generic;
using System.Linq;
using Dapper;
using System.Data;
using MySql.Data.MySqlClient;
using league.Models;

namespace league.Factory
{
    public class DojoFactory : IFactory<Dojo>
    {
        private string connectionString;
        public DojoFactory()
        {
            connectionString = "server=localhost;userid=root;password=root;port=3306;database=league;SslMode=None";
        }
        internal IDbConnection Connection
        {
            get
            {
                return new MySqlConnection(connectionString);
            }
        }
        public List<Dojo> GetDojos()
        {
            using (IDbConnection dbConnection = Connection)
            {
                string query = "SELECT * FROM dojos";
                dbConnection.Open();
                return dbConnection.Query<Dojo>(query).ToList();
            }
        }
        public void AddDojo(Dojo item)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string query = "INSERT INTO dojos (Name, Location, Description, CreatedAt, UpdatedAt) VALUES (@Name, @Location, @Description, NOW(), NOW())";
                dbConnection.Open();
                dbConnection.Execute(query, item);
            }
        }
        public Dojo GetOne(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string query =
                @"
                SELECT * FROM dojos WHERE id = @Id;
                SELECT * FROM ninjas WHERE dojo_id = @Id;
                ";
                dbConnection.Open();
                using (var multi = dbConnection.QueryMultiple(query, new {Id = id}))
        {
            var dojo = multi.Read<Dojo>().Single();
            dojo.ninjas = multi.Read<Ninja>().ToList();
            return dojo;
        }
            }
        }
    }
}