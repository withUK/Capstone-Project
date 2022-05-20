namespace Project_Flow_Manager_Models
{
    public class TeamResource
    {
        public int Id { get; set; }

        public int TeamId { get; set; }
        public virtual Team Team { get; set; }

        public int Hours { get; set; }
    }
}
