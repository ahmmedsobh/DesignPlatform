namespace DesignPlatform.Helpers.UploadHelper
{
    public interface IUploadHelper
    {
        string Upload(IFormFile Photo, int FolderName);

    }
}
