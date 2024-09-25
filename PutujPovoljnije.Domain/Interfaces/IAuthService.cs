namespace PutujPovoljnije.Domain.Interfaces
{
    public interface IAuthService
    {
        Task<string> GetTokenAsync();
    }
}
