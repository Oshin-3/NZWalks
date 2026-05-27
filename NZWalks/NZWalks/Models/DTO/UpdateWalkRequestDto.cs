using NZWalks.Models.Domain;
using System.ComponentModel.DataAnnotations;

namespace NZWalks.Models.DTO
{
    public class UpdateWalkRequestDto
    {
        [Required]
        [MaxLength(255, ErrorMessage = "Name cannot be more than 255 characters long.")]
        public string Name { get; set; }
        [Required]
        [MaxLength(255, ErrorMessage = "Description cannot be more than 255 characters long.")]
        public string Description { get; set; }
        [Required]
        [Range(1, 200, ErrorMessage = "Length must be a positive number.")]
        public string LengthInKm { get; set; }
        public string? WalkImageUrl { get; set; }
        public Guid RegionId { get; set; }
        public Guid DifficultyId { get; set; }
    }
}
