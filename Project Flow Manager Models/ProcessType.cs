using System.ComponentModel.DataAnnotations;

namespace Project_Flow_Manager_Models
{
    public class ProcessType
    {
        public int Id { get; set; }
        [Required]
        public string Value { get; set; }
    }
}
