using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WatchMovieWeb.Models
{
    public class Category
    {
        [Key]
        public int Id{ get; set; }

        [Required]  
        public String Name { get; set; }
        [DisplayName("Display Order")]
        [Range(1,50,ErrorMessage="The Display Order must lie in between 1 and 50")]
        public int DisplayOrder { get; set; }

        public DateTime CreatedDateTime { get; set; } = DateTime.Now;   
    }
}
