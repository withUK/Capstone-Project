using ProjectFlowManagerModels;
using System.ComponentModel.DataAnnotations;

namespace Project_Flow_Manager_Models
{
    public class Recommendation
    {
        public int Id { get; set; }
        
        public string Details { get; set; }

        [Display(Name = "Created By")]
        public string CreatedBy { get; set; }
        
        [Display(Name = "Created Date")]
        public DateTime CreatedDate { get; set; }

        public virtual ICollection<Attachment> Attachments { get; set; }
    }
}
