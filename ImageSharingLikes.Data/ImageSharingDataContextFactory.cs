using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageSharingLikes.Data
{
    public class ImageSharingDataContextFactory : IDesignTimeDbContextFactory<ImageSharingContext>
    {
        public ImageSharingContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder()
              .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(),
              $"..{Path.DirectorySeparatorChar}ImageSharingLikes.Web"))
              .AddJsonFile("appsettings.json")
              .AddJsonFile("appsettings.local.json", optional: true, reloadOnChange: true).Build();

            return new ImageSharingContext(config.GetConnectionString("ConStr"));
        }
    }
}
