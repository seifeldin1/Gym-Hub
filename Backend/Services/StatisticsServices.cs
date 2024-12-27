using Backend.Models;
using Backend.Database;
using MySql.Data.MySqlClient;
namespace Backend.Services{
    public class StatisticsServices{
        private readonly GymDatabase database;
        public StatisticsServices(GymDatabase database){
            this.database = database;
        }

        public NumericalStatistics GetOverallNumericalStatistics(){
            var stats = new NumericalStatistics();
            using(var connection = database.ConnectToDatabase()){
                connection.Open();
                string clientQuery = "SELECT COUNT(Client_ID) FROM Client WHERE AccountActivated = true";
                string coachQuery = "SELECT COUNT(Coach_ID) FROM Coach";
                string branchManagerQuery= "SELECT COUNT(Branch_Manager_ID) FROM Branch_Manager";
                string branchQuery = "SELECT COUNT(Branch_ID) FROM Branch";
                string equipmentsQuery = "SELECT COUNT(Equipment_ID) From Equipments";      
                using (var command = new MySqlCommand(clientQuery, connection))
                {
                    stats.Total_Number_Of_Clients = (int)command.ExecuteScalar();
                }
                using (var command = new MySqlCommand(coachQuery, connection)){
                    stats.Total_Number_Of_Coaches = (int)command.ExecuteScalar();
                }
                using (var command = new MySqlCommand(branchManagerQuery, connection)){
                    stats.Total_Number_Of_Branch_Managers = (int)command.ExecuteScalar();
                }
                using (var command = new MySqlCommand(branchQuery, connection)){
                    stats.Total_Number_Of_Branches = (int)command.ExecuteScalar();
                }
                using (var command = new MySqlCommand(equipmentsQuery, connection)){
                    stats.Total_Number_Of_Equipments = (int)command.ExecuteScalar();
                }
                return stats;
            }
        }

        public NumericalStatistics GetBranchNumericalStatistics(int id){
            var stats = new NumericalStatistics();
            using(var connection = database.ConnectToDatabase()){
                connection.Open();
                string coachQuery = "SELECT COUNT(Coach_ID) FROM Coach WHERE Works_For_Branch = @id";
                string equipmentsQuery = "SELECT COUNT(Equipment_ID) From Equipments WHERE Belong_To_Branch_ID = @id";
                using (var command = new MySqlCommand(coachQuery, connection)){
                    command.Parameters.AddWithValue("@id", id);
                    stats.Total_Number_Of_Coaches_Per_Branch = (int)command.ExecuteScalar();
                }
                using (var command = new MySqlCommand(coachQuery, connection)){
                    command.Parameters.AddWithValue("@id", id);
                    stats.Total_Number_Of_Equipments_Per_Branch = (int)command.ExecuteScalar();
                }
                return stats;
            }
        }

        public FinancialStatistics GetFinancialStatisticsOverall(){
            var stats = new FinancialStatistics();
            using(var connection = database.ConnectToDatabase()){
                connection.Open();
                string membershipFeesQuery = "SELECT SUM(Fees_Of_Membership) from Client where AccountActivated = true";
                string coachSalaryQuery = "SELECT SUM(Salary) From Coach";
                string branchManagerSalaryQuery = "SELECT SUM(Salary) From Branch_Manager";
                string equipmentFeesQuery = "SELECT SUM(Purchase_Price) From Equipments";
                string supplementsPurchaseFeesQuery = "SELECT SUM(Purchased_Price) FROM Supplements";
                string supplementSellingPriceQuery = "SELECT SUM(Selling_Price) FROM Supplements";
                using (var command = new MySqlCommand(membershipFeesQuery, connection)){
                    stats.Total_Membership_Fees = (int)command.ExecuteScalar();
                }
                using (var command = new MySqlCommand(coachSalaryQuery, connection)){
                    stats.Total_Coach_Salary = (int)command.ExecuteScalar();
                }
                using (var command = new MySqlCommand(branchManagerSalaryQuery, connection)){
                    stats.Total_Branch_Manager_Salary = (int)command.ExecuteScalar();
                }
                using (var command = new MySqlCommand(equipmentFeesQuery, connection)){
                    stats.Total_Equipment_Purchase_Fees = (int)command.ExecuteScalar();
                }
                using (var command = new MySqlCommand(supplementsPurchaseFeesQuery, connection)){
                    stats.Total_Supplements_Purchase_Fees = (int)command.ExecuteScalar();
                }
                using (var command = new MySqlCommand(supplementSellingPriceQuery, connection)){
                    stats.Total_Supplements_Selling_Price = (int)command.ExecuteScalar();
                }
                stats.Total_Salaries = stats.Total_Branch_Manager_Salary + stats.Total_Coach_Salary;           
                stats.Total_Income = stats.Total_Membership_Fees + stats.Total_Supplement_Selling_Price;
                stats.Total_Outcome = stats.Total_Equipment_Fees + stats.Total_Salaries + stats.Total_Supplements_Purchase_Fees;
                return stats;


            }
        }


    }
}