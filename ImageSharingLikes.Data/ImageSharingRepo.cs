using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace ImageSharingLikes.Data
{
    public class ImageSharingRepo
    {
        private string _connectionString;
        public ImageSharingRepo(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Image> GetAll()
        {
            using var isc = new ImageSharingContext(_connectionString);
            return isc.Images.OrderByDescending(i => i.Id).ToList();
        }

        public void Add(Image image)
        {
            using var isc = new ImageSharingContext(_connectionString);
            isc.Images.Add(image);
            isc.SaveChanges();
        }

        public Image GetImageById(int id)
        {
            using var isc = new ImageSharingContext(_connectionString);
            return isc.Images.FirstOrDefault(i => i.Id == id);
        }

        public void LikeImage(Image image)
        {
            //using var isc = new ImageSharingContext(_connectionString);
            //var img = GetImageById(image.Id);
            //img.Likes += 1;
            //isc.SaveChanges();

            using var isc = new ImageSharingContext(_connectionString);
            isc.Database.ExecuteSqlInterpolated($"UPDATE Images SET Likes = Likes + 1 WHERE Id = {image.Id}");
        }
    }
}
