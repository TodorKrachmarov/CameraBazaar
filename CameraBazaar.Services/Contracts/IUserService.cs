namespace CameraBazaar.Services.Contracts
{
    using Models;

    public interface IUserService
    {
        UserProfileServiceModel Profile(string Username);
    }
}
