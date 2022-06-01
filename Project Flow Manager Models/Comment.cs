using System.ComponentModel.DataAnnotations;

namespace ProjectFlowManagerModels
{
    public class Comment
    {
        public int Id { get; set; }
        [Display(Name = "Comment")]
        public string Value { get; set; }
        [Display(Name = "Submitted Date")]
        public DateTime Created { get; set; }
        [Display(Name = "Submitted By")]
        public string? CreatedBy { get; set; }

    }
}
