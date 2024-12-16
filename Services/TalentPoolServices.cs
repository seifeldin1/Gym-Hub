using Backend.Models;
using Backend.Database;
using MySql.Data.MySqlClient;
namespace Backend.Services{
    public class TalentPoolServices{
        private readonly GymDatabase database;
        public TalentPoolServices(GymDatabase database){
            this.database = database;
        }

        /*public List<Skills> ViewTalentPool(){
            var skills = new List<Skills>();
            using(var connection = database.ConnectToDatabase()){
                connection.Open();
                string query = "SELECT "
            }
        }*/
    }
}