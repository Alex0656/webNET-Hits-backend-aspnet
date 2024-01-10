namespace lab1_backend.Models
{
    public class AllMovies
    {
        public List<MovieGetByIdMeth> Movies { get; set; }

        public PageInfo pageInfo { get; set; }
    }
}
