using System.ComponentModel.DataAnnotations;

namespace Project_Flow_Manager_Models
{
    public class ProcessStep
    {
        public int Id { get; set; }

        [Display(Name = "Instruction")]
        public string Value { get; set; }
        
        [Display(Name = "Position")]
        public int OrderPosition { get; set; }
    }
}
