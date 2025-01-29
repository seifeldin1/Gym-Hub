using System.Numerics;

namespace Backend.Models
{
    public class UserModel
    {
        public int  User_ID {get; set;}
        public string Username { get; set; }
        public string PasswordHashed { get; set; }
        public string Type { get; set; }
        public string First_Name { get; set; }
        public string Last_Name  { get; set; }
        public string Email { get; set; }
        public string Phone_Number { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public long National_Number { get; set; }
    }
}