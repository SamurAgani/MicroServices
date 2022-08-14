using System.ComponentModel.DataAnnotations;

namespace FreeCourse.Web.Models.Catalog
{
    public class FeatureVM
    {
        [Required]
        public int Duration { get; set; }
    }
}
