using Backend.Models;
using Backend.Database;
using MySql.Data.MySqlClient;
namespace Backend.Services {
    public class DietServices{
        public GymDatabase database ; 
        public DietServices(GymDatabase database){
            this.database = database;
        }


        //* Modification: check if he already is on diet or not 
        public (bool success , string message) ChooseDiet(Diet diet){
            using(var connection = database.ConnectToDatabase()){
                connection.Open();
                int checker;
                string checkquery="SELECT COUNT(*) FROM DIET WHERE Client_Assigned_TO_ID=@Client_Assigned_TO_ID";
                 using(var command = new MySqlCommand(checkquery , connection)){
                    command.Parameters.AddWithValue("@Client_Assigned_TO_ID" , diet.Client_Assigned_TO_ID);
                     checker = (int)command.ExecuteScalar();
                }
                string query = @"INSERT INTO DIET(Nutrition_Plan_ID , Supplement_ID , Coach_Created_ID , Client_Assigned_TO_ID, Status, Start_Date , End_Date)
                VALUES (@planID , @suppID , @coachID , @clientID , @status , @startDate , @endDate)";

                string coachQuery = "SELECT Belong_To_Coach_ID FROM Client WHERE Client_ID = @id";
                int coachID;
                using(var command = new MySqlCommand(coachQuery , connection)){
                    command.Parameters.AddWithValue("@id" , diet.Coach_Created_ID);
                    coachID = (int)command.ExecuteScalar();
                }
                    if(checker>0){
                        string deletequery="DELETE * FROM DIET WHERE Client_Assigned_TO_ID=@Client_Assigned_TO_ID";
                    }
                using(var addCommand = new MySqlCommand(query , connection)){
                    addCommand.Parameters.AddWithValue("@planID" , diet.Nutrition_Plan_ID);
                    addCommand.Parameters.AddWithValue("@suppID" , diet.Supplement_ID);
                    addCommand.Parameters.AddWithValue("@coachID" , coachID);
                    addCommand.Parameters.AddWithValue("@clientID", diet.Client_Assigned_TO_ID);
                    addCommand.Parameters.AddWithValue("status" , diet.Status);
                    addCommand.Parameters.AddWithValue("@startDate" , diet.Start_Date);
                    addCommand.Parameters.AddWithValue("@endDate" , diet.End_Date);
                    addCommand.ExecuteNonQuery();
                }
                return(true , "diet chosen successfully");

            }
        }

        // to be done later : delete a diet 
    }
}