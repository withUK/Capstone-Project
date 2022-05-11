namespace Project_Flow_Manager_Models
{
    public class Effort
    {
        public int Id { get; set; }
        public int? Amount { get; set; }
        public string? Measure { get; set; }
        public string DisplayEffort() { return string.Concat(Amount, " ", Measure); }
    }
}
