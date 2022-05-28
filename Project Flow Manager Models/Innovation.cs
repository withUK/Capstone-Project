using ProjectFlowManagerModels;
using System.ComponentModel.DataAnnotations;

namespace Project_Flow_Manager_Models
{
    public class Innovation : Submission
    {
        public Innovation()
        {
            Status = "New";
        }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        [Display(Name = "Process Duration")]
        public int ProcessDuration { get; set; }

        [Display(Name = "Number Of People Included")]
        public int NumberOfPeopleIncluded { get; set; }

        [Display(Name = "Process Type")]
        public string ProcessType { get; set; }

        [Display(Name = "Required Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime RequiredDate { get; set; }

        [Display(Name = "Process Steps")]
        public virtual ICollection<ProcessStep>? ProcessSteps { get; set; }
        public virtual ICollection<Technology>? Technologies { get; set; }
        
        public int? ApprovalId { get; set; }
        public virtual Approval? Approval { get; set; }
    }
}
