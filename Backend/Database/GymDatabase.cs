using MySql.Data.MySqlClient;
using DotNetEnv;

namespace Backend.Database{
    public class GymDatabase{
        /*private static readonly string connectionStringTemplate = "Server=127.0.0.1;User=root;Password={0};";
        static GymDatabase(){
            // Load environment variables from .env file (optional, for development convenience)
            Env.Load();
        }

        public string GetConnectionString(){
            string password = Environment.GetEnvironmentVariable("MYSQL_PASSWORD");
            if (string.IsNullOrEmpty(password)){
                // Handle missing password gracefully (e.g., throw an exception)
                throw new ArgumentException("MYSQL_PASSWORD environment variable not found");
            }

            return string.Format(connectionStringTemplate, password);
        }
        //Create connection 
        public MySqlConnection ConnectToDatabase(){
            string connectionString = GetConnectionString();
            return new MySqlConnection(connectionString);
        }*/

       
        private const string connectionString = "Server=127.0.0.1;Database=GymHub;User=root;Password=AmrAshraf@0135789@;";
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
                        National_Number BIGINT NOT NULL UNIQUE
                    );
                " , connection);
                createUserTableCommand.ExecuteNonQuery();

                var createOwnerTableCommand= new MySqlCommand(@"
                    CREATE TABLE IF NOT EXISTS Owner(
                        Owner_ID INT NOT NULL PRIMARY KEY,
                        Share_Percentage INT NOT NULL, 
                        Established_branches INT NOT NULL,
                        FOREIGN KEY(Owner_ID) REFERENCES User(User_ID) ON DELETE CASCADE ON UPDATE CASCADE
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
                        Contract_Length INT,
                        FOREIGN KEY (Manages_Branch_ID) REFERENCES Branch(Branch_ID) ON DELETE SET NULL ON UPDATE CASCADE,
                        FOREIGN KEY(Branch_Manager_ID) REFERENCES User(User_ID) ON DELETE CASCADE ON UPDATE CASCADE
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
                        Status VARCHAR(50),
                        Contract_Length INT,
                        FOREIGN KEY(Works_For_Branch) REFERENCES Branch(Branch_ID) ON DELETE SET NULL ON UPDATE CASCADE,
                        FOREIGN KEY(Coach_ID) REFERENCES User(User_ID) ON DELETE CASCADE ON UPDATE CASCADE
                    );
                ", connection);
                createCoachTableCommand.ExecuteNonQuery();

                var createSkillsTableCommand = new MySqlCommand(@"
                    CREATE TABLE IF NOT EXISTS Skills(
                        Skill_Name VARCHAR(50) NOT NULL, 
                        Coach_Skilled_ID INT NOT NULL,
                        FOREIGN KEY(Coach_Skilled_ID) REFERENCES Coach(Coach_ID) ON DELETE CASCADE ON UPDATE CASCADE
                    );
                ", connection);
                createSkillsTableCommand.ExecuteNonQuery();

                var createClientTableCommand = new MySqlCommand(@"
                    CREATE TABLE IF NOT EXISTS Client(
                        Client_ID INT NOT NULL PRIMARY KEY,
                        Join_Date DATE NOT NULL, 
                        BMR INT, 
                        Weight_kg DOUBLE, 
                        Height_cm DOUBLE, 
                        Belong_To_Coach_ID INT , 
                        AccountActivated BOOLEAN DEFAULT false,
                        Start_Date_Membership DATE NOT NULL, 
                        End_Date_Membership DATE NOT NULL, 
                        Membership_Type VARCHAR(255) NOT NULL DEFAULT 'Silver',
                        Fees_Of_Membership INT NOT NULL, 
                        Membership_Period_Months INT NOT NULL,
                        FOREIGN KEY(Belong_To_Coach_ID) REFERENCES Coach(Coach_ID) ON DELETE SET NULL ON UPDATE CASCADE,
                        FOREIGN KEY(Client_ID) REFERENCES User(User_ID) ON DELETE CASCADE ON UPDATE CASCADE
                    );
                ", connection);
                createClientTableCommand.ExecuteNonQuery();
                
                var createReportTableCommand = new MySqlCommand(@"
                    CREATE TABLE IF NOT EXISTS Reports(
                        Report_ID INT AUTO_INCREMENT PRIMARY KEY, 
                        Manager_Reported_ID INT ,
                        Title VARCHAR(50) NOT NULL, 
                        Generated_Date DATE NOT NULL, 
                        Type VARCHAR(50) NOT NULL DEFAULT 'Monthly Report',
                        Status VARCHAR(50) NOT NULL DEFAULT 'To be sent',
                        Content VARCHAR(500) NOT NULL ,
                        FOREIGN KEY(Manager_Reported_ID) REFERENCES Branch_Manager(Branch_Manager_ID) ON DELETE SET NULL ON UPDATE CASCADE
                    );
                ", connection);
                createReportTableCommand.ExecuteNonQuery();

                var createFreeInterviewTimesCommand = new MySqlCommand(@"
                    CREATE TABLE IF NOT EXISTS Interview_Times(
                        Interview_ID INT AUTO_INCREMENT PRIMARY KEY,
                        Manager_ID INT, 
                        Free_Interview_Date DATETIME NOT NULL,
                        Taken BOOLEAN DEFAULT false,
                        FOREIGN KEY(Manager_ID) REFERENCES Branch_Manager(Branch_Manager_ID) ON DELETE SET NULL ON UPDATE CASCADE
                    );
                
                ", connection);
                createFreeInterviewTimesCommand.ExecuteNonQuery();

                var createCandidateTableCommand= new MySqlCommand(@"
                    CREATE TABLE IF NOT EXISTS Candidate(
                        Candidate_ID INT AUTO_INCREMENT PRIMARY KEY, 
                        First_Name VARCHAR(255) NOT NULL,
                        Last_Name VARCHAR(255) NOT NULL,
                        Age INT NOT NULL,
                        National_Number BIGINT NOT NULL UNIQUE ,
                        Phone_Number VARCHAR(100) NOT NULL UNIQUE ,
                        Email VARCHAR(255) NOT NULL UNIQUE,
                        Status VARCHAR(50), 
                        Resume_Link VARCHAR(1000) NOT NULL,
                        Linkedin_Account_Link VARCHAR(1000)
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
                        Branch_Posted_ID INT, 
                        Description VARCHAR(255) NOT NULL, 
                        Title VARCHAR(50) NOT NULL, 
                        Date_Posted DATETIME NOT NULL, 
                        Skills_Required VARCHAR(255) NOT NULL, 
                        Experience_Years_Required INT NOT NULL, 
                        Deadline DATETIME NOT NULL, 
                        Location VARCHAR(255) NOT NULL,
                        FOREIGN KEY(Branch_Posted_ID) REFERENCES Branch(Branch_ID) ON DELETE CASCADE ON UPDATE CASCADE
                    );
                ", connection);
                createJobPostingsTableCommand.ExecuteNonQuery();

                var createApplicationsTableCommand = new MySqlCommand(@"
                    CREATE TABLE IF NOT EXISTS Applications(
                        Applicant_ID INT , 
                        Post_ID INT , 
                        Applied_Date DATETIME NOT NULL,
                        Years_Of_Experience INT NOT NULL,
                        PRIMARY KEY(Applicant_ID , Post_ID),
                        FOREIGN KEY(Applicant_ID) REFERENCES Candidate(Candidate_ID) ON DELETE CASCADE ON UPDATE CASCADE, 
                        FOREIGN KEY(Post_ID) REFERENCES Job_Posting(Post_ID) ON DELETE CASCADE ON UPDATE CASCADE

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
                        FOREIGN KEY(Belong_To_Branch_ID) REFERENCES Branch(Branch_ID) ON DELETE SET NULL ON UPDATE CASCADE
                    );
                ", connection);
                createEquipmentsTableCommand.ExecuteNonQuery();

                var createRatingTableCommand = new MySqlCommand(@"
                    CREATE TABLE IF NOT EXISTS Ratings(
                        Coach_ID INT NOT NULL,
                        Rating_ID INT AUTO_INCREMENT,
                        Client_ID INT , 
                        Rate INT NOT NULL,
                        PRIMARY KEY(Rating_ID), 
                        FOREIGN KEY(Coach_ID) REFERENCES Coach(Coach_ID) ON DELETE CASCADE ON UPDATE CASCADE, 
                        FOREIGN KEY(Client_ID) REFERENCES Client(Client_ID) ON DELETE SET NULL ON UPDATE CASCADE
                    );
                ", connection);
                createRatingTableCommand.ExecuteNonQuery();

                var createWorkoutTableCommand = new MySqlCommand(@"
                    CREATE TABLE IF NOT EXISTS Workout(
                        Workout_ID INT AUTO_INCREMENT PRIMARY KEY, 
                        Muscle_Targeted VARCHAR(50) NOT NULL, 
                        Goal VARCHAR(50) NOT NULL, 
                        Created_By_Coach_ID INT , 
                        Calories_Burnt INT NOT NULL, 
                        Reps_Per_Set INT,
                        Sets INT, 
                        Duration_min INT,
                        FOREIGN KEY(Created_By_Coach_ID) REFERENCES Coach(Coach_ID) ON DELETE SET NULL ON UPDATE CASCADE
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
                        Performed BOOLEAN DEFAULT false,
                        FOREIGN KEY(Workout_ID) REFERENCES Workout(Workout_ID) ON DELETE CASCADE ON UPDATE CASCADE,
                        FOREIGN KEY(Client_ID) REFERENCES Client(Client_ID) ON DELETE CASCADE ON UPDATE CASCADE
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
                        FOREIGN KEY(Supplement_ID) REFERENCES Supplements(Supplement_ID) ON DELETE CASCADE ON UPDATE CASCADE,
                        FOREIGN KEY(Nutrition_Plan_ID) REFERENCES Nutrition(Nutrition_ID) ON DELETE CASCADE ON UPDATE CASCADE
                    );
                " , connection);
                createSupplementsNeededTableCommand.ExecuteNonQuery();
                

                var createDietTableCommand = new MySqlCommand(@"
                    CREATE TABLE IF NOT EXISTS Diet(
                        Nutrition_Plan_ID INT NOT NULL,
                        Supplement_ID INT NOT NULL,
                        Coach_Created_ID INT, 
                        Client_Assigned_TO_ID INT NOT NULL,
                        Status VARCHAR(255) NOT NULL DEFAULT 'Not choosed', 
                        Start_Date DATE NOT NULL,
                        End_Date DATE NOT NULL,
                        FOREIGN KEY(Nutrition_Plan_ID) REFERENCES Nutrition(Nutrition_ID) ON DELETE CASCADE ON UPDATE CASCADE,
                        FOREIGN KEY(Supplement_ID) REFERENCES Supplements(Supplement_ID) ON DELETE CASCADE ON UPDATE CASCADE,
                        FOREIGN KEY(Coach_Created_ID) REFERENCES Coach(Coach_ID) ON DELETE SET NULL ON UPDATE CASCADE,
                        FOREIGN KEY(Client_Assigned_TO_ID) REFERENCES Client(Client_ID) ON DELETE CASCADE ON UPDATE CASCADE ,
                        PRIMARY KEY(Nutrition_Plan_ID, Client_Assigned_TO_ID)
                    );
                " , connection);
                createDietTableCommand.ExecuteNonQuery();

                var createAnnouncementTableCommand = new MySqlCommand(@"
                    CREATE TABLE IF NOT EXISTS Announcements(
                        Announcements_ID INT AUTO_INCREMENT PRIMARY KEY, 
                        Author_ID INT NOT NULL,
                        Author_Role VARCHAR(50) NOT NULL,
                        Title VARCHAR(255) NOT NULL, 
                        Content VARCHAR(500) NOT NULL,
                        Date_Posted DATETIME NOT NULL,
                        Type VARCHAR(50) NOT NULL,
                        FOREIGN KEY(Author_ID) REFERENCES User(User_ID)ON DELETE CASCADE ON UPDATE CASCADE
                    );
                ", connection);
                createAnnouncementTableCommand.ExecuteNonQuery();

                var createProgressTableCommand = new MySqlCommand(@"
                    CREATE TABLE IF NOT EXISTS Progress(
                        Progress_ID INT PRIMARY KEY AUTO_INCREMENT,
                        Client_ID INT NOT NULL,
                        Weight_kg DOUBLE NOT NULL,
                        DateInserted DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
                        FOREIGN KEY(Client_ID) REFERENCES Client(Client_ID) ON DELETE CASCADE ON UPDATE CASCADE
                    );
                ", connection);
                createProgressTableCommand.ExecuteNonQuery();

                var createRecommendationTableCommand = new MySqlCommand(@"
                    CREATE TABLE IF NOT EXISTS Recommendation(
                        Recommendation_ID INT PRIMARY KEY AUTO_INCREMENT,
                        Client_ID INT NOT NULL,
                        Plan_ID INT,
                        Supplement_ID INT,
                        FOREIGN KEY(Client_ID) REFERENCES Client(Client_ID) ON DELETE CASCADE ON UPDATE CASCADE,
                        FOREIGN KEY(Plan_ID) REFERENCES Nutrition(Nutrition_ID) ON DELETE CASCADE ON UPDATE CASCADE,
                        FOREIGN KEY(Supplement_ID) REFERENCES Supplements(Supplement_ID) ON DELETE CASCADE ON UPDATE CASCADE
                    );
                ", connection);
                createProgressTableCommand.ExecuteNonQuery();

                var createMeetingsTableCommand = new MySqlCommand(@"
                    CREATE TABLE IF NOT EXISTS Meetings(
                        Meeting_ID INT PRIMARY KEY AUTO_INCREMENT,
                        Coach_ID INT NOT NULL,
                        Title VARCHAR(70) NOT NULL,
                        Time DATETIME NOT NULL,
                        FOREIGN KEY(Coach_ID) REFERENCES Coach(Coach_ID) ON DELETE CASCADE ON UPDATE CASCADE
                    );
                ", connection);
                createMeetingsTableCommand.ExecuteNonQuery();

                //Created by => branch manager or coach just like announcements 
                var createEventsTableCommand = new MySqlCommand(@"
                    CREATE TABLE IF NOT EXISTS Events(
                        Event_ID INT PRIMARY KEY AUTO_INCREMENT,
                        Title VARCHAR(70) NOT NULL,
                        Type VARCHAR(100) NOT NULL,
                        Start_Date DATETIME NOT NULL,
                        End_Date DATETIME NOT NULL , 
                        Description VARCHAR(500) , 
                        Location VARCHAR(200),
                        Created_By_ID INT , 
                        FOREIGN KEY(Created_By_ID) REFERENCES User(User_ID) ON DELETE CASCADE ON UPDATE CASCADE
                    );
                ", connection);
                createEventsTableCommand.ExecuteNonQuery();

                var createHolidayTableCommand = new MySqlCommand(@"
                    CREATE TABLE IF NOT EXISTS Holiday(
                        Holiday_ID INT PRIMARY KEY AUTO_INCREMENT,
                        Title VARCHAR(70) NOT NULL,
                        Start_Date DATETIME NOT NULL,
                        End_Date DATETIME NOT NULL 
                    );
                ", connection);
                createHolidayTableCommand.ExecuteNonQuery();

                var creatBlackListedTokenTable = new MySqlCommand(@"
                    CREATE TABLE IF NOT EXISTS BlacklistedTokens (
                        Token VARCHAR(500) NOT NULL
                    );
                " , connection);
                creatBlackListedTokenTable.ExecuteNonQuery();

                var createClientProgressTable = new MySqlCommand(@"
                    CREATE TABLE IF NOT EXISTS ClientProgress(
                        Client_Progress_ID INT AUTO_INCREMENT PRIMARY KEY,
                        Client_ID INT,
                        Coach_ID INT,
                        ReportDate DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
                        ProgressSummary VARCHAR(400) NOT NULL,
                        GoalsAchieved VARCHAR(200) NOT NULL,
                        ChallengesFaced VARCHAR(200) NOT NULL,
                        NextSteps VARCHAR(300) NOT NULL,
                        FOREIGN KEY (Client_ID) REFERENCES Client(Client_ID) ON DELETE CASCADE ON UPDATE CASCADE,
                        FOREIGN KEY (Coach_ID) REFERENCES Coach(Coach_ID) ON DELETE SET NULL ON UPDATE CASCADE
                    )
                " , connection);
                createClientProgressTable.ExecuteNonQuery();

                

            }
        }


    }
}