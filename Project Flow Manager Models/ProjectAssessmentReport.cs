using Project_Flow_Manager_Models;

namespace ProjectFlowManagerModels
{
    public class ProjectAssessmentReport
    {
        public int Id { get; set; }
        public string? Title { get; set; }

        public int InnovationId { get; set; }
        public virtual Innovation Innovation { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Attachment> Attachments { get; set; }
        public virtual ICollection<Approval> Approvals { get; set; }
    }
}