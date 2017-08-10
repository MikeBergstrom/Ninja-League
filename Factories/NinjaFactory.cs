using System.Collections.Generic;
using System.Linq;
using Dapper;
using System.Data;
using MySql.Data.MySqlClient;
using league.Models;
 
namespace league.Factory
{
    public class NinjaFactory : IFactory<Ninja>
    {
        private string connectionString;
        public NinjaFactory()
        {
            connectionString = "server=localhost;userid=root;password=root;port=3306;database=league;SslMode=None";
        }
        internal IDbConnection Connection
        {
            get {
                return new MySqlConnection(connectionString);
            }
        }
        public void AddNinja(Ninja item)
        {
            using (IDbConnection dbConnection = Connection) {
                string query =  "INSERT INTO ninjas (Name, Level, Description, CreatedAt, UpdatedAt, dojo_id) VALUES (@Name, @Level, @Description, NOW(), NOW(), @dojo_id)";
                dbConnection.Open();
                dbConnection.Execute(query, item);
            }
        }
        public List<Ninja> GetNinjas(){
            using(IDbConnection dbConnection = Connection){
                string query = "SELECT * FROM ninjas JOIN dojos ON ninjas.dojo_id WHERE dojos.id = ninjas.dojo_id";
                dbConnection.Open();
                var allNinjas =  dbConnection.Query<Ninja, Dojo, Ninja>(query, (ninja, dojo) => {ninja.dojo = dojo; return ninja;}).ToList();
                return allNinjas;
            }
        }
        public Ninja GetOne(int id){
            using(IDbConnection dbConnection = Connection){
                string query = $"SELECT * FROM ninjas JOIN dojos ON ninjas.dojo_id WHERE dojos.id = ninjas.dojo_id AND ninjas.id = {id}";
                dbConnection.Open();
                var thisNinja=  dbConnection.Query<Ninja, Dojo, Ninja>(query, (ninja, dojo) => {ninja.dojo = dojo; return ninja;}).FirstOrDefault();
                return thisNinja;
            }
        }
    }
}