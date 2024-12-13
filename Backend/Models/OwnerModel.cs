using Org.BouncyCastle.Asn1.Cms;

namespace Backend.Models
{
    public class OwnerModel : UserModel
    {
        public int? Owner_ID { get; set; }
        public int Share_Percentage { get; set; }
        public int Established_branches { get; set; }
    }
}