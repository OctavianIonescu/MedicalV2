namespace TestSmth2.Repos
{
    public interface IFileRepo
    {
        Task<string> UploadAsync(IFormFile file);
    }
}
