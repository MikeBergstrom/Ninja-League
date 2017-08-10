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
            get {
                return new MySqlConnection(connectionString);
            }
        }
        public List<Dojo> GetDojos(){
            using(IDbConnection dbConnection = Connection){
                string query = "SELECT * FROM dojos";
                dbConnection.Open();
                return dbConnection.Query<Dojo>(query).ToList();
            }
        }
    }
}