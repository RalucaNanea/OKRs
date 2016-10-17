namespace OKRs.DataContract
{
    public class ObjectiveDto
    {
        public int ObjectiveId { get; set; }
        public int TeamId { get; set; }
        public string Objective { get; set; }
        public int Score { get; set; }
        public int TotalScore { get; set; }
        public string QuarterNo { get; set; }
        public string Year { get; set; }
    }
}
