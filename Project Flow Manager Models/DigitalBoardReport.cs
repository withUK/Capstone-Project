namespace ProjectFlowManagerModels
{
    public class DigitalBoardReport
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public int WorkRequestId { get; set; }
        public virtual WorkRequest WorkRequest { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Attachment> Attachments { get; set; }
        public virtual ICollection<Approval> Approvals { get; set; }
    }
}