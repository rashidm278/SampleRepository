namespace SearchProject.Services.Interfaces
{
    public interface IJwtTokenGenerateService
    {
        string GenerateJwt(string userId, string username, string role);
    }
}
