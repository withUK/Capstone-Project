using ProjectFlowManagerModels;
using System.ComponentModel.DataAnnotations;

namespace Project_Flow_Manager_Models
{
    public class ResourceRequest
    {
        public int Id { get; set; }

        [Display(Name = "Total hours")]
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

        [Display(Name = "Project Assessment Report Id")]
        public int? ProjectAssessmentReportId { get; set; }
        [Display(Name = "Project Assessment Report")]
        public virtual ProjectAssessmentReport ProjectAssessmentReport { get; set; }

        [Display(Name = "Teams")]
        public virtual ICollection<TeamResource>? Teams { get; set; }
        [Display(Name = "Technologies")]
        public virtual ICollection<Technology>? Technologies { get; set; }

    }
}
