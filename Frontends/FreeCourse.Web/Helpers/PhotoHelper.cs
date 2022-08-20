using FreeCourse.Web.Models;
using Microsoft.Extensions.Options;

namespace FreeCourse.Web.Helpers
{
    public class PhotoHelper
    {
        private readonly ServiceApiSettings serviceApiSettings;

        public PhotoHelper(IOptions<ServiceApiSettings> serviceApiSettings)
        {
            this.serviceApiSettings = serviceApiSettings.Value;
        }
        public string GetPhotoStockUrl(string photoUrl)
        {
            return $"{serviceApiSettings.PhotoStockUri}/photos/{photoUrl}";
        }
    }
}
