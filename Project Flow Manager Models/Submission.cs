using ProjectFlowManagerModels;
using Syncfusion.DocIO.DLS;
using System.ComponentModel.DataAnnotations;

namespace Project_Flow_Manager_Models
{
    public class Submission
    {
        public int Id { get; set; }
        public string Status { get; set; }

        [Display(Name = "Submitted Date")]
        public DateTime Created { get; set; }
        [Display(Name = "Submitted By")]
        public string? CreatedBy { get; set; }

        public virtual ICollection<Attachment>? Attachments { get; set; }
        public virtual ICollection<Comment>? Comments { get; set; }
        public virtual ICollection<Tag>? Tags { get; set; }
    }
}
