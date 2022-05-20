using Project_Flow_Manager_Models;
using System.ComponentModel.DataAnnotations;

namespace ProjectFlowManagerModels
{
    public class ProjectAssessmentReport
    {
        public ProjectAssessmentReport()
        {
            Approvals = new List<Approval>();
            Recommendations = new List<Recommendation>();
            Comments = new List<Comment>();
            Approvals = new List<Approval>();
        }

        public int Id { get; set; }
        public string? Title { get; set; }
        public string Status { get; set; }
        [Display(Name = "Submitted Date")]
        public DateTime? CreatedDate { get; set; }

        public int InnovationId { get; set; }
        public virtual Innovation? Innovation { get; set; }

        public int? ChosenRecommendationId { get; set; }

        public virtual ICollection<Recommendation>? Recommendations { get; set; }
        public virtual ICollection<Comment>? Comments { get; set; }
        public virtual ICollection<Attachment>? Attachments { get; set; }
        public virtual ICollection<Approval>? Approvals { get; set; }
    }
}