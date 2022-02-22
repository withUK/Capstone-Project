namespace ProjectFlowManagerModels
{
    public class WorkRequest
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Priority { get; set; }
        public DateOnly Created { get; set; }
        public DateOnly TargetDate { get; set; }
        
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Attachment> Attachments { get; set; }
        public virtual ICollection<Approval> Approvals { get; set; }
    }
}