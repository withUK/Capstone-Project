using ProjectFlowManagerModels;
using System.ComponentModel.DataAnnotations;

namespace Project_Flow_Manager_Models
{
    public class Recommendation : Submission
    {
        public string Details { get; set; }

        public int? EffortId { get; set; }
        public virtual Effort? Effort { get; set; }

        [Display(Name = "Process Steps")]
        public virtual ICollection<ProcessStep>? ProcessSteps { get; set; }
        public virtual ICollection<Technology>? Technologies { get; set; }
        public virtual ICollection<Team>? Teams { get; set; }
    }
}
