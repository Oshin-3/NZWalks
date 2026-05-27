using System.ComponentModel.DataAnnotations;

namespace NZWalks.Models.DTO
{
    public class UpdateRegionRequestDto
    {
        [Required]
        [MinLength(3, ErrorMessage = "Code must be at least 3 characters long.")]
        [MaxLength(3, ErrorMessage = "Code cannot be more than 3 characters long.")]
        public string Code { get; set; }
        [Required]
        [MaxLength(255, ErrorMessage = "Name cannot be more than 255 characters long.")]
        public string Name { get; set; }
        public string? RegionImageUrl { get; set; }
    }
}
