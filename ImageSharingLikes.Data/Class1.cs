using Microsoft.EntityFrameworkCore;

namespace ImageSharingLikes.Data
{
    public class Image
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FilePath { get; set; }
        public int Likes { get; set; }
        public DateTime Date { get; set; }
    }

   
}
