using System.ComponentModel.DataAnnotations;

namespace TrainingApi.Model
{
    public class PointOfInterestForUpdateDto
    {

        [Required(ErrorMessage = "Please provide a good name.")]
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(200)]
        public string Description { get; set; }
    }
}
