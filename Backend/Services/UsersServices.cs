using Backend.Database;
using Backend.Models;
using MySql.Data.MySqlClient;

namespace Backend.Services
{
    public class UsersServices
    {
        private readonly GymDatabase database;
        public UsersServices(GymDatabase gymDatabase)
        {
            this.database = gymDatabase;
        }



    }
}