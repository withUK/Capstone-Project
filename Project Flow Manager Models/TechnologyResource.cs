using System.ComponentModel.DataAnnotations;

namespace Project_Flow_Manager_Models
{
    public class TechnologyResource
    {
        public int Id { get; set; }
        [Display(Name = "Product Name")]
        public string? ProductName { get; set; }
    }
}
