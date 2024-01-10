namespace lab1_backend.Models
{
    public class GenresToMovies
    {
        public string Id;
        public string Name;

        public static string ToJson(List<GenresToMovies> list)
        {
            string ret = "{ \n";

            foreach (GenresToMovies pair in list)
            {
                
                ret += "\t" + pair.Id + " " + pair.Name + ",\n";
            }
            ret += "\n}";
            return ret;
        }
    }
}
