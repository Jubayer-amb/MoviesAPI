namespace MoviesAPI.DTOs
{
    public class PagedResultDTO
    {
        public List<GenreDTO> data { get; set; }
        public int totalData { get; set; }
    }
}
