namespace ProjectFlowManagerModels
{
    public class Approval
    {
        public int Id { get; set; }
        public string? Type { get; set; }
        public string? Reason { get; set; }
        public string? Outcome { get; set; }
        public string? ApprovedBy { get; set; }
        public DateTime ApprovedOn { get; set; }
    }
}