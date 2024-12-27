namespace Backend.Models
{
    public class Application{
        public int Applicant_ID {get; set;}
        public int Post_ID {get; set;}
        public DateTime applicationDate{get; set;}
        public int Years_Of_Experience{get; set;}

        public string Applicant_Name {get; set;}
        public string Resume_Link {get; set;}
        
    }
}