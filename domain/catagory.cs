using System.ComponentModel.DataAnnotations;

namespace domain.Models
{
    public class catagory
    {
        public int Id { get; set; }

        [NotAbc]
        [Required]
        public string Name { get; set; } = string.Empty;

       
        public string image { get; set; } = string.Empty;
    }

}