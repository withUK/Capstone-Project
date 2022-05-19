using ProjectFlowManagerModels;
using System.ComponentModel.DataAnnotations;

namespace Project_Flow_Manager_Models
{
    public class Recommendation
    {
        public int Id { get; set; }

        public string Details { get; set; }

        public int? EffortId { get; set; }
        public virtual Effort? Effort { get; set; }

        [Display(Name = "Created By")]
        public string? CreatedBy { get; set; }

        [Display(Name = "Created Date")]
        public DateTime? CreatedDate { get; set; }

        [Display(Name = "Process Steps")]
        public virtual ICollection<ProcessStep>? ProcessSteps { get; set; }
        public virtual ICollection<Attachment>? Attachments { get; set; }
        public virtual ICollection<Technology>? Technologies { get; set; }
        public virtual ICollection<Team>? Teams { get; set; }

        [Display(Name = "Project Assessment Reports")]
        public int? ProjectAssessmentReportId { get; set; }
        public virtual ProjectAssessmentReport? ProjectAssessmentReport { get; set; }
    }
}
