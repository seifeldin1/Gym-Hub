using Backend.Models;
using Backend.Database;
using MySql.Data.MySqlClient;
namespace Backend.Services{
    public class BonusServices{
        private readonly GymDatabase database;
        public BonusServices(GymDatabase database){
            this.database = database;
        }

        //add bonus , update bonus , remove bonuses
        //view bonus is automatically done when his profile is viewed 
        
    }
}



