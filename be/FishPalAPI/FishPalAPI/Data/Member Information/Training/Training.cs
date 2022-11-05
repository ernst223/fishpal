using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FishPalAPI.Data.Member_Information.Training
{
    public class Training
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string ManagerYearCompleted { get; set; }
        public string ManagerPointsReceived { get; set; }
        public string CoachLvl1YearCompleted { get; set; }
        public string CoachLvl1PointsReceived { get; set; }
        public string CoachLvl2YearCompleted { get; set; }
        public string CoachLvl2PointsReceived { get; set; }

        public string CaptainYearCompleted { get; set; }
        public string CaptainPointsReceived { get; set; }
        public string AdminYearCompleted { get; set; }
        public string AdminPointsReceived { get; set; }
    }
}
