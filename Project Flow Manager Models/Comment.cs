using System.ComponentModel.DataAnnotations;

namespace ProjectFlowManagerModels
{
    public class Comment
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public DateTime Created { get; set; }
    }
}
