using Backend.Models;
using Backend.Database;
using MySql.Data.MySqlClient;
namespace Backend.Services{
    public class CalendarServices{
        private readonly GymDatabase database;
        public CalendarServices(GymDatabase gymDatabase)
        {
            this.database = gymDatabase;
        }

        public List<CalendarEvent> GetCalendarEventsBetween(DateTime startDate , DateTime endDate){
            var calendar = new List<CalendarEvent>();
            using(var connection = database.ConnectToDatabase()){
                connection.Open();
                string eventQuery = @"SELECT Event_ID AS Id, Title, Start_Date AS StartDate, End_Date AS EndDate,
                 'Event' AS Type FROM Events WHERE Start_Date BETWEEN @startDate AND @endDate";
                using(var command = new MySqlCommand(eventQuery , connection)){
                    command.Parameters.AddWithValue("@startDate" , startDate);
                    command.Parameters.AddWithValue("@endDate" , endDate);
                    using(var reader = command.ExecuteReader()){
                        while(reader.Read()){
                            calendar.Add(new CalendarEvent{
                                Id = reader.GetInt32("Id"),
                                Title = reader.GetString("Title"),
                                StartDate = reader.GetDateTime("StartDate"),
                                EndDate = reader.GetDateTime("EndDate"),
                                Type = reader.GetString("Type")
                            });
                        }
                    }
                }

                string holidayQuery = @"SELECT Holiday_ID AS Id, Title, Start_Date AS StartDate, End_Date AS EndDate,
                                    'Holiday' AS Type FROM Holiday WHERE Start_Date BETWEEN @startDate AND @endDate";
                using(var command = new MySqlCommand(holidayQuery , connection)){
                    command.Parameters.AddWithValue("@startDate" , startDate);
                    command.Parameters.AddWithValue("@endDate" , endDate);
                    using(var reader = command.ExecuteReader()){
                        while(reader.Read()){
                            calendar.Add(new CalendarEvent{
                                Id = reader.GetInt32("Id"),
                                Title = reader.GetString("Title"),
                                StartDate = reader.GetDateTime("StartDate"),
                                EndDate = reader.GetDateTime("EndDate"),
                                Type = reader.GetString("Type")
                            });
                        }
                    }
                }

                //Time is stored at meetings table as datetime so i just want to get all meetings in that period
                string meetingQuery = @"SELECT Meeting_ID AS Id, Title, Time AS StartDate, Time AS EndDate,
                                     'Meeting' AS Type FROM Meetings WHERE Time BETWEEN @startDate AND @endDate";
                using(var command = new MySqlCommand(meetingQuery , connection)){
                    command.Parameters.AddWithValue("@startDate" , startDate);
                    command.Parameters.AddWithValue("@endDate" , endDate);
                    using(var reader = command.ExecuteReader()){
                        while(reader.Read()){
                            calendar.Add(new CalendarEvent{
                                Id = reader.GetInt32("Id"),
                                Title = reader.GetString("Title"),
                                StartDate = reader.GetDateTime("StartDate"),
                                EndDate = reader.GetDateTime("EndDate"),
                                Type = reader.GetString("Type")
                            });
                        }
                    }
                }
            }
            return calendar;
        }
    }
}