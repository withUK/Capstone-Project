using Project_Flow_Manager_Models;
using System.ComponentModel.DataAnnotations;

namespace ProjectFlowManagerModels
{
    public class ProjectAssessmentReport : Submission
    {
        public ProjectAssessmentReport()
        {
            Approvals = new List<Approval>();
            Recommendations = new List<Recommendation>();
            Comments = new List<Comment>();
            Approvals = new List<Approval>();
        }

        public string? Title { get; set; }
        [Display(Name = "Submitted Date")]
        public DateTime? CreatedDate { get; set; }

        public int InnovationId { get; set; }
        public virtual Innovation? Innovation { get; set; }

        public int? ChosenRecommendationId { get; set; }

        public virtual ICollection<Recommendation>? Recommendations { get; set; }
        public virtual ICollection<Approval>? Approvals { get; set; }
    }
}