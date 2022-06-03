using System.ComponentModel.DataAnnotations;

namespace Project_Flow_Manager_Models
{
    public class RoleAssignment
    {
        public int Id { get; set; }

        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Display(Name = "Role")]
        public int RoleId { get; set; }
        public virtual Role? Role { get; set; }
    }
}
