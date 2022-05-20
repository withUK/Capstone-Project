using ProjectFlowManagerModels;

namespace Project_Flow_Manager_Models
{
    public class ResourceRequest
    {
        public int Id { get; set; }

        public int TotalHours()
        {
            int hours = 0;
            if (Teams != null)
            {
                foreach (var item in Teams)
                {
                    hours += item.Hours;
                }
            }
            return hours;
        }

        public int ProjectAssessmentId { get; set; }
        public virtual ProjectAssessmentReport ProjectAssessmentReport { get; set; }

        public virtual ICollection<TeamResource>? Teams { get; set; }
        public virtual ICollection<Technology>? Technologies { get; set; }

    }
}
