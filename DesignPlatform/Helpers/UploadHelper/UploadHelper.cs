using DesignPlatform.Enums;

namespace DesignPlatform.Helpers.UploadHelper
{
    public class UploadHelper : IUploadHelper
    {
        private readonly IWebHostEnvironment hostingEnvironment;

        public UploadHelper(IWebHostEnvironment hostingEnvironment)
        {
            this.hostingEnvironment = hostingEnvironment;
        }

        public string Upload(IFormFile Photo, int FolderName)
        {
            string folderName = System.Enum.GetName(typeof(FolderName), FolderName);
            string uniqueFileName = string.Empty;
            if (Photo != null)
            {
                string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, $"Images/{folderName}");
                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                uniqueFileName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(Photo.FileName);
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    Photo.CopyTo(fileStream);
                }
            }
            return $"Images/{folderName}/" + uniqueFileName;
        }
    }
}
