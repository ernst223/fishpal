namespace FishPalAPI.Models.UserInformation.Training
{
    public class TrainingDTO
    {
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
