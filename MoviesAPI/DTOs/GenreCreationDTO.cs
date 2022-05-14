using MoviesAPI.Filters;
using System.ComponentModel.DataAnnotations;

namespace MoviesAPI.DTOs
{
    public class GenreCreationDTO
    {

        [Required(ErrorMessage = "The field with name {0} is required (from genre)")]
        [StringLength(50)]
        [FirstLetterUppercase]
        public string? Name { get; set; }
    }
}
