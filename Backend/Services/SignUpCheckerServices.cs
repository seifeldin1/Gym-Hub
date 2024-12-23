using Backend.Database;
using MySql.Data.MySqlClient;

namespace Backend.Services {
    public class SignUpCheckerServices {
        private readonly GymDatabase database;

        public SignUpCheckerServices(GymDatabase database) {
            this.database = database;
        }

        public bool IsEmailUsed(string email) {
            using (var connection = database.ConnectToDatabase()) {
                connection.Open();
                string query = "SELECT COUNT(*) FROM User WHERE Email = @Email";
                using (var command = new MySqlCommand(query, connection)) {
                    command.Parameters.AddWithValue("@Email", email);
                    int count = Convert.ToInt32(command.ExecuteScalar());
                    return count > 0;
                }
            }
        }

        public bool IsUsernameUsed(string username) {
            using (var connection = database.ConnectToDatabase()) {
                connection.Open();
                string query = "SELECT COUNT(*) FROM User WHERE Username = @Username";
                using (var command = new MySqlCommand(query, connection)) {
                    command.Parameters.AddWithValue("@Username", username);
                    int count = Convert.ToInt32(command.ExecuteScalar());
                    return count > 0;
                }
            }
        }

        public bool IsPhoneNumberUsed(string phoneNumber) {
            using (var connection = database.ConnectToDatabase()) {
                connection.Open();
                string query = "SELECT COUNT(*) FROM User WHERE Phone_Number = @PhoneNumber";
                using (var command = new MySqlCommand(query, connection)) {
                    command.Parameters.AddWithValue("@PhoneNumber", phoneNumber);
                    int count = Convert.ToInt32(command.ExecuteScalar());
                    return count > 0;
                }
            }
        }

        public bool IsNationalNumberUsed(long nationalNumber) {
            using (var connection = database.ConnectToDatabase()) {
                connection.Open();
                string query = "SELECT COUNT(*) FROM User WHERE National_Number = @NationalNumber";
                using (var command = new MySqlCommand(query, connection)) {
                    command.Parameters.AddWithValue("@NationalNumber", nationalNumber);
                    int count = Convert.ToInt32(command.ExecuteScalar());
                    return count > 0;
                }
            }
        }
    }
}
