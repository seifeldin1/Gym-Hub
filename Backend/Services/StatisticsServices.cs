using Backend.Models;
using Backend.Database;
using MySql.Data.MySqlClient;
namespace Backend.Services
{
    public class StatisticsServices
    {
        private readonly GymDatabase database;
        public StatisticsServices(GymDatabase database)
        {
            this.database = database;
        }

        public NumericalStatistics GetOverallNumericalStatistics()
        {
            var stats = new NumericalStatistics();
            using (var connection = database.ConnectToDatabase())
            {
                connection.Open();
                string clientQuery = "SELECT COUNT(Client_ID) FROM Client WHERE AccountActivated = true";
                string coachQuery = "SELECT COUNT(Coach_ID) FROM Coach";
                string branchManagerQuery = "SELECT COUNT(Branch_Manager_ID) FROM Branch_Manager";
                string branchQuery = "SELECT COUNT(Branch_ID) FROM Branch";
                string equipmentsQuery = "SELECT COUNT(Equipment_ID) From Equipments";
                using (var command = new MySqlCommand(clientQuery, connection))
                {
                    stats.Total_Number_Of_Clients = (long)command.ExecuteScalar();
                }
                using (var command = new MySqlCommand(coachQuery, connection))
                {
                    stats.Total_Number_Of_Coaches = (long)command.ExecuteScalar();
                }
                using (var command = new MySqlCommand(branchManagerQuery, connection))
                {
                    stats.Total_Number_Of_Branch_Managers = (long)command.ExecuteScalar();
                }
                using (var command = new MySqlCommand(branchQuery, connection))
                {
                    stats.Total_Number_Of_Branches = (long)command.ExecuteScalar();
                }
                using (var command = new MySqlCommand(equipmentsQuery, connection))
                {
                    stats.Total_Number_Of_Equipments = (long)command.ExecuteScalar();
                }
                return stats;
            }
        }

        public NumericalStatistics GetBranchNumericalStatistics(int id)
        {
            var stats = new NumericalStatistics();
            using (var connection = database.ConnectToDatabase())
            {
                connection.Open();
                string coachQuery = "SELECT COUNT(Coach_ID) FROM Coach WHERE Works_For_Branch = @id";
                string equipmentsQuery = "SELECT COUNT(Equipment_ID) From Equipments WHERE Belong_To_Branch_ID = @id";
                using (var command = new MySqlCommand(coachQuery, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    stats.Total_Number_Of_Coaches_Per_Branch = (long)command.ExecuteScalar();
                }
                using (var command = new MySqlCommand(equipmentsQuery, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    stats.Total_Number_Of_Equipments_Per_Branch = (long)command.ExecuteScalar();
                }
                return stats;
            }
        }

        public List<NumericalStatistics> GetAllBranchesNumericalStatistics()
        {
            var allStats = new List<NumericalStatistics>();

            using (var connection = database.ConnectToDatabase())
            {
                connection.Open();

                // Query to get the branch statistics
                string query = @"
            SELECT 
                b.Branch_ID, 
                (SELECT COUNT(Coach_ID) FROM Coach WHERE Works_For_Branch = b.Branch_ID) AS CoachCount,
                (SELECT COUNT(Equipment_ID) FROM Equipments WHERE Belong_To_Branch_ID = b.Branch_ID) AS EquipmentCount
            FROM Branch b;
        ";

                using (var command = new MySqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var stats = new NumericalStatistics
                            {
                                Branch_ID = reader.GetInt32("Branch_ID"),
                                Total_Number_Of_Coaches_Per_Branch = reader.GetInt64("CoachCount"),
                                Total_Number_Of_Equipments_Per_Branch = reader.GetInt64("EquipmentCount")
                            };

                            allStats.Add(stats);
                        }
                    }
                }
            }

            return allStats;
        }


        public FinancialStatistics GetFinancialStatisticsOverall()
        {
            var stats = new FinancialStatistics();
            using (var connection = database.ConnectToDatabase())
            {
                connection.Open();

                // Define the SQL queries
                string membershipFeesQuery = "SELECT SUM(Fees_Of_Membership) FROM Client WHERE AccountActivated = true";
                string coachSalaryQuery = "SELECT SUM(Salary) FROM Coach";
                string branchManagerSalaryQuery = "SELECT SUM(Salary) FROM Branch_Manager";
                string equipmentFeesQuery = "SELECT SUM(Purchase_Price) FROM Equipments";
                string supplementsPurchaseFeesQuery = "SELECT SUM(Purchased_Price) FROM Supplements";
                string supplementsSellingPriceQuery = "SELECT SUM(Selling_Price) FROM Supplements";

                // Helper function to handle DBNull
                long GetLongValueFromQuery(MySqlCommand command)
                {
                    var result = command.ExecuteScalar();
                    return result == DBNull.Value ? 0 : Convert.ToInt64(result);
                }

                // Execute queries and assign values
                using (var command = new MySqlCommand(membershipFeesQuery, connection))
                {
                    stats.Total_Membership_Fees = GetLongValueFromQuery(command);
                }

                using (var command = new MySqlCommand(coachSalaryQuery, connection))
                {
                    stats.Total_Coach_Salary = GetLongValueFromQuery(command);
                }

                using (var command = new MySqlCommand(branchManagerSalaryQuery, connection))
                {
                    stats.Total_Branch_Manager_Salary = GetLongValueFromQuery(command);
                }

                using (var command = new MySqlCommand(equipmentFeesQuery, connection))
                {
                    stats.Total_Equipment_Purchase_Fees = GetLongValueFromQuery(command);
                }

                using (var command = new MySqlCommand(supplementsPurchaseFeesQuery, connection))
                {
                    stats.Total_Supplements_Purchase_Fees = GetLongValueFromQuery(command);
                }

                using (var command = new MySqlCommand(supplementsSellingPriceQuery, connection))
                {
                    stats.Total_Supplements_Selling_Price = GetLongValueFromQuery(command);
                }

                // Calculate totals
                stats.Total_Salaries = stats.Total_Branch_Manager_Salary + stats.Total_Coach_Salary;
                stats.Total_Supplement_Fees = stats.Total_Supplements_Selling_Price - stats.Total_Supplements_Purchase_Fees;
                stats.Total_Income = stats.Total_Membership_Fees + stats.Total_Supplements_Selling_Price;
                stats.Total_Outcome = stats.Total_Equipment_Purchase_Fees + stats.Total_Salaries + stats.Total_Supplements_Purchase_Fees;

                return stats;
            }
        }



    }
}