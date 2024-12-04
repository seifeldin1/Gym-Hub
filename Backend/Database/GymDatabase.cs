using MySql.Data.MySqlClient;

namespace Backend.Database{
    public class GymDatabase{
        private const string connectionString = "Server=127.0.0.1;User=root;Password=$$eif@eldin_1020;";

        //Create connection 
        public MySqlConnection ConnectToDatabase(){
            return new MySqlConnection(connectionString);
        }

        
        public void DatabaseSetUp(){
            using(var connection = ConnectToDatabase()){
                connection.Open();
                
                var createDatabaseCommand = new MySqlCommand("CREATE DATABASE IF NOT EXISTS GymHub;", connection);
                createDatabaseCommand.ExecuteNonQuery();


                var useDatabaseCommand = new MySqlCommand("USE GymHub;", connection);
                useDatabaseCommand.ExecuteNonQuery();

                var createUserTableCommand= new MySqlCommand(@"
                    CREATE TABLE IF NOT EXISTS User(
                        User_ID INT AUTO_INCREMENT PRIMARY KEY, 
                        Username VARCHAR(255) NOT NULL UNIQUE,
                        PasswordHashed VARCHAR(255) NOT NULL,
                        Type VARCHAR(50) NOT NULL, 
                        First_Name VARCHAR(255) NOT NULL, 
                        Last_Name VARCHAR(255) NOT NULL, 
                        Email VARCHAR(255) NOT NULL UNIQUE, 
                        Phone_Number VARCHAR(100) NOT NULL UNIQUE,
                        Gender VARCHAR(20), 
                        Age INT, 
                        National_Number INT NOT NULL UNIQUE
                    );
                " , connection);
                createUserTableCommand.ExecuteNonQuery();

                var createOwnerTableCommand= new MySqlCommand(@"
                    CREATE TABLE IF NOT EXISTS Owner(
                        Owner_ID INT NOT NULL PRIMARY KEY,
                        Share_Percentage INT NOT NULL, 
                        Established_branches INT NOT NULL,
                        FOREIGN KEY(Owner_ID) REFERENCES User(User_ID)
                    );
                " , connection);
                createOwnerTableCommand.ExecuteNonQuery();

                var createBranchTableCommand = new MySqlCommand(@"
                    CREATE TABLE IF NOT EXISTS Branch(
                        Branch_ID INT AUTO_INCREMENT PRIMARY KEY,
                        Branch_Name VARCHAR(255) NOT NULL,
                        Location VARCHAR(255),
                        Opening_Time TIME NOT NULL, 
                        Closing_Time TIME NOT NULL
                    )
                ", connection);
                createBranchTableCommand.ExecuteNonQuery();

                var createBranchManagerTableCommand = new MySqlCommand(@"
                    CREATE TABLE IF NOT EXISTS Branch_Manager(
                        Branch_Manager_ID INT NOT NULL PRIMARY KEY,
                        Salary INT NOT NULL DEFAULT 10000, 
                        Penalties INT DEFAULT 0 , 
                        Bonuses INT DEFAULT 0, 
                        Hire_Date DATE NOT NULL,
                        Employee_Under_Supervision INT NOT NULL DEFAULT 0,
                        Fire_Date DATE,
                        Manages_Branch_ID INT,
                        FOREIGN KEY (Manages_Branch_ID) REFERENCES Branch(Branch_ID),
                        FOREIGN KEY(Branch_Manager_ID) REFERENCES User(User_ID)
                    );
                ", connection);
                createBranchManagerTableCommand.ExecuteNonQuery();

                var createCoachTableCommand = new MySqlCommand(@"
                    CREATE TABLE IF NOT EXISTS Coach(
                        Coach_ID INT NOT NULL PRIMARY KEY,
                        Salary INT NOT NULL DEFAULT 5000, 
                        Penalties INT DEFAULT 0, 
                        Bonuses INT DEFAULT 0, 
                        Hire_Date DATE NOT NULL,
                        Fire_Date DATE,
                        Experience_Years INT NOT NULL DEFAULT 2,
                        Works_For_Branch INT, 
                        Daily_Hours_Worked INT NOT NULL, 
                        Shift_Start TIME,
                        Shift_Ends TIME, 
                        Speciality VARCHAR(50) NOT NULL,
                        FOREIGN KEY(Coach_ID) REFERENCES User(User_ID)
                    );
                ", connection);
                createCoachTableCommand.ExecuteNonQuery();

                var createSkillsTableCommand = new MySqlCommand(@"
                    CREATE TABLE IF NOT EXISTS Skills(
                        Skill_Name VARCHAR(50) NOT NULL, 
                        Coach_Skilled_ID INT NOT NULL,
                        FOREIGN KEY(Coach_Skilled_ID) REFERENCES Coach(Coach_ID)
                    );
                ", connection);
                createSkillsTableCommand.ExecuteNonQuery();

                var createClientTableCommand = new MySqlCommand(@"
                    CREATE TABLE IF NOT EXISTS Client(
                        Client_ID INT NOT NULL PRIMARY KEY,
                        Join_Date DATE NOT NULL, 
                        BMR INT, 
                        Weight_kg INT, 
                        Height_cm INT, 
                        Belong_To_Coach_ID INT , 
                        Start_Date_Membership DATE NOT NULL, 
                        End_Date_Membership DATE NOT NULL, 
                        Membership_Type VARCHAR(255) NOT NULL DEFAULT 'Silver',
                        Fees_Of_Membership INT NOT NULL, 
                        Membership_Period_Months INT NOT NULL,
                        FOREIGN KEY(Belong_To_Coach_ID) REFERENCES Coach(Coach_ID),
                        FOREIGN KEY(Client_ID) REFERENCES User(User_ID)
                    );
                ", connection);
                createClientTableCommand.ExecuteNonQuery();
                
                var createReportTableCommand = new MySqlCommand(@"
                    CREATE TABLE IF NOT EXISTS Reports(
                        Report_ID INT AUTO_INCREMENT PRIMARY KEY, 
                        Manager_Reported_ID INT NOT NULL,
                        Title VARCHAR(50) NOT NULL, 
                        Generated_Date DATE NOT NULL, 
                        Type VARCHAR(50) NOT NULL DEFAULT 'Montly Report',
                        Status VARCHAR(50) NOT NULL DEFAULT 'To be sent',
                        Content VARCHAR(500) NOT NULL ,
                        FOREIGN KEY(Manager_Reported_ID) REFERENCES Branch_Manager(Branch_Manager_ID)
                    );
                ", connection);
                createReportTableCommand.ExecuteNonQuery();

                var createFreeInterviewTimesCommand = new MySqlCommand(@"
                    CREATE TABLE IF NOT EXISTS Interview_Times(
                        Interview_ID INT AUTO_INCREMENT PRIMARY KEY,
                        Manager_ID INT NOT NULL, 
                        Free_Interview_Date DATETIME NOT NULL,
                        Taken BOOLEAN,
                        FOREIGN KEY(Manager_ID) REFERENCES Branch_Manager(Branch_Manager_ID)
                    );
                
                ", connection);
                createFreeInterviewTimesCommand.ExecuteNonQuery();

                var createCandidateTableCommand= new MySqlCommand(@"
                    CREATE TABLE IF NOT EXISTS Candidate(
                        Candidate_ID INT AUTO_INCREMENT PRIMARY KEY, 
                        First_Name VARCHAR(255) NOT NULL,
                        Last_Name VARCHAR(255) NOT NULL,
                        Age INT NOT NULL,
                        National_Number INT NOT NULL ,
                        Years_Of_Experience INT NOT NULL,
                        Date_Applied DATE NOT NULL, 
                        Phone_Number VARCHAR(100) NOT NULL,
                        Email VARCHAR(255) NOT NULL,
                        Status VARCHAR(50), 
                        Resume_Link VARCHAR(500) NOT NULL,
                        Linkedin_Account_Link VARCHAR(500)
                    );
                ", connection);
                createCandidateTableCommand.ExecuteNonQuery();


                /*var createInterviewsTableCommand = new MySqlCommand(@"
                    CREATE TABLE IF NOT EXISTS Interviews(
                        Manager_ID INT NOT NULL, 
                        Candidate_ID INT NOT NULL,
                        Interview_Date DATETIME NOT NULL,
                        FOREIGN KEY(Manager_ID) REFERENCES Branch_Manager(Branch_Manager_ID),
                        FOREIGN KEY(Candidate_ID) REFERENCES Candidate(Candidate_ID),
                    );
                " , connection);
                createInterviewsTableCommand.ExecuteNonQuery();*/ //i think we don't this as we can get its data from interview times table


                var createJobPostingsTableCommand = new MySqlCommand(@"
                    CREATE TABLE IF NOT EXISTS Job_Posting(
                        Post_ID INT AUTO_INCREMENT PRIMARY KEY, 
                        Branch_Posted_ID INT NOT NULL, 
                        Description VARCHAR(255) NOT NULL, 
                        Title VARCHAR(50) NOT NULL, 
                        Date_Posted DATETIME NOT NULL, 
                        Skills_Required VARCHAR(255) NOT NULL, 
                        Experience_Years_Required INT NOT NULL, 
                        Deadline DATETIME NOT NULL, 
                        Location VARCHAR(255) NOT NULL,
                        FOREIGN KEY(Branch_Posted_ID) REFERENCES Branch(Branch_ID)
                    );
                ", connection);
                createJobPostingsTableCommand.ExecuteNonQuery();

                var createApplicationsTableCommand = new MySqlCommand(@"
                    CREATE TABLE IF NOT EXISTS Applications(
                        Applicant_ID INT NOT NULL , 
                        Post_ID INT NOT NULL, 
                        Applied_Date DATETIME NOT NULL,
                        PRIMARY KEY(Applicant_ID , Post_ID),
                        FOREIGN KEY(Applicant_ID) REFERENCES Candidate(Candidate_ID), 
                        FOREIGN KEY(Post_ID) REFERENCES Job_Posting(Post_ID)

                    );
                ", connection);
                createApplicationsTableCommand.ExecuteNonQuery();


                var createEquipmentsTableCommand= new MySqlCommand(@"
                    CREATE TABLE IF NOT EXISTS Equipments(
                        Equipment_ID INT AUTO_INCREMENT PRIMARY KEY,
                        Status VARCHAR(50) NOT NULL DEFAULT 'Available',
                        Purchase_Price INT NOT NULL,
                        Category VARCHAR(50) NOT NULL, 
                        Purchase_Date DATETIME, 
                        Name VARCHAR(100) NOT NULL, 
                        Serial_Number VARCHAR(255) NOT NULL UNIQUE, 
                        Belong_To_Branch_ID INT, 
                        FOREIGN KEY(Belong_To_Branch_ID) REFERENCES Branch(Branch_ID)
                    );
                ", connection);
                createEquipmentsTableCommand.ExecuteNonQuery();

                var createRatingTableCommand = new MySqlCommand(@"
                    CREATE TABLE IF NOT EXISTS Ratings(
                        Coach_ID INT NOT NULL, 
                        Client_ID INT NOT NULL, 
                        Rate INT NOT NULL,
                        PRIMARY KEY(Coach_ID , Client_ID), 
                        FOREIGN KEY(Coach_ID) REFERENCES Coach(Coach_ID), 
                        FOREIGN KEY(Client_ID) REFERENCES Client(Client_ID)
                    );
                ", connection);
                createRatingTableCommand.ExecuteNonQuery();

                var createWorkoutTableCommand = new MySqlCommand(@"
                    CREATE TABLE IF NOT EXISTS Workout(
                        Workout_ID INT AUTO_INCREMENT PRIMARY KEY, 
                        Muscle_Targeted VARCHAR(50) NOT NULL, 
                        Goal VARCHAR(50) NOT NULL, 
                        Created_By_Coach_ID INT NOT NULL, 
                        Calories_Burnt INT NOT NULL, 
                        Reps_Per_Set INT,
                        Sets INT, 
                        Duration_min INT,
                        FOREIGN KEY(Created_By_Coach_ID) REFERENCES Coach(Coach_ID)
                    );
                " , connection);
                createWorkoutTableCommand.ExecuteNonQuery();

                var createPerformWorkoutTableCommad = new MySqlCommand(@"
                    CREATE TABLE IF NOT EXISTS Perform_Workout(
                        Workout_ID INT NOT NULL,
                        Client_ID INT NOT NULL, 
                        Order_Of_Workout INT NOT NULL, 
                        Type VARCHAR(50) NOT NULL, 
                        Day_Number INT NOT NULL,
                        FOREIGN KEY(Workout_ID) REFERENCES Workout(Workout_ID),
                        FOREIGN KEY(Client_ID) REFERENCES Client(Client_ID)
                    );
                " , connection);
                createPerformWorkoutTableCommad.ExecuteNonQuery();

                var createSupplementsTableCommand = new MySqlCommand(@"
                    CREATE TABLE IF NOT EXISTS Supplements(
                        Supplement_ID INT AUTO_INCREMENT PRIMARY KEY,
                        Name VARCHAR(50) NOT NULL,
                        Brand VARCHAR(50) NOT NULL,
                        Selling_Price DECIMAL(10,2) NOT NULL,
                        Purchased_Price DECIMAL(10,2) NOT NULL, 
                        Type VARCHAR(50) NOT NULL, 
                        Flavor VARCHAR(50) DEFAULT 'No Flavor',
                        Manufactured_Date DATE NOT NULL, 
                        Expiration_Date DATE NOT NULL, 
                        Purchase_Date DATE NOT NULL, 
                        Scoop_Size_grams INT NOT NULL, 
                        Scoop_Number_package INT NOT NULL, 
                        Scoop_Detail VARCHAR(255) NOT NULL,
                        CHECK (Manufactured_Date < Expiration_Date),
                        CHECK (Selling_Price >= Purchased_Price AND Purchased_Price>0),
                        CHECK (Scoop_Size_grams > 0 AND Scoop_Number_package > 0),
                        CHECK (Type IN ('Protein', 'Vitamins', 'Creatine')),
                        CHECK (Flavor IN ('Vanilla', 'Chocolate', 'No Flavor'))
                    );
                " , connection);
                createSupplementsTableCommand.ExecuteNonQuery();

                var createNutritionTableCommand = new MySqlCommand(@"
                    CREATE TABLE IF NOT EXISTS Nutrition(
                        Nutrition_ID INT AUTO_INCREMENT PRIMARY KEY,
                        Goal VARCHAR(50) NOT NULL, 
                        Protein_grams INT NOT NULL, 
                        Carbohydrates_grams INT NOT NULL,
                        Fat_grams INT NOT NULL,
                        Calories INT NOT NULL, 
                        Name VARCHAR(50) NOT NULL, 
                        Description VARCHAR(500) NOT NULL
                        );
                " , connection);
                createNutritionTableCommand.ExecuteNonQuery();

                var createSupplementsNeededTableCommand = new MySqlCommand(@"
                    CREATE TABLE IF NOT EXISTS Supplements_Needed(
                        Supplement_ID INT NOT NULL , 
                        Nutrition_Plan_ID INT NOT NULL,
                        Frequency VARCHAR(80) NOT NULL, 
                        Reason VARCHAR(255) NOT NULL, 
                        Start_Date DATE NOT NULL, 
                        End_Date DATE NOT NULL,
                        Mandatory BOOLEAN NOT NULL, 
                        Scoops_Per_Day_Of_Usage INT NOT NULL,
                        FOREIGN KEY(Supplement_ID) REFERENCES Supplements(Supplement_ID),
                        FOREIGN KEY(Nutrition_Plan_ID) REFERENCES Nutrition(Nutrition_ID)
                    );
                " , connection);
                createSupplementsNeededTableCommand.ExecuteNonQuery();
                

                var createDietTableCommand = new MySqlCommand(@"
                    CREATE TABLE IF NOT EXISTS Diet(
                        Nutrition_Plan_ID INT NOT NULL,
                        Coach_Created_ID INT NOT NULL, 
                        Client_Assigned_TO_ID INT NOT NULL,
                        Status VARCHAR(255) NOT NULL DEFAULT 'Not choosed', 
                        Start_Date DATE NOT NULL,
                        End_Date DATE NOT NULL,
                        FOREIGN KEY(Nutrition_Plan_ID) REFERENCES Nutrition(Nutrition_ID),
                        FOREIGN KEY(Coach_Created_ID) REFERENCES Coach(Coach_ID),
                        FOREIGN KEY(Client_Assigned_TO_ID) REFERENCES Client(Client_ID),
                        PRIMARY KEY(Nutrition_Plan_ID, Coach_Created_ID, Client_Assigned_TO_ID)
                    );
                " , connection);
                createDietTableCommand.ExecuteNonQuery();

                var createAnnouncmentTableCommand = new MySqlCommand(@"
                    CREATE TABLE IF NOT EXISTS Announcments(
                        Announcments_ID INT AUTO_INCREMENT PRIMARY KEY, 
                        Author_ID INT NOT NULL,
                        Author_Role VARCHAR(50) NOT NULL,
                        Title VARCHAR(255) NOT NULL, 
                        Content VARCHAR(500) NOT NULL,
                        Date_Posted DATETIME NOT NULL,
                        Type VARCHAR(50) NOT NULL,
                        FOREIGN KEY(Author_ID) REFERENCES User(User_ID)
                    );
                ", connection);
                createAnnouncmentTableCommand.ExecuteNonQuery();
            }
        }


    }
}