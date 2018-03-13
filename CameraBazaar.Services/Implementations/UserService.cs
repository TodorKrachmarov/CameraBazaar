namespace CameraBazaar.Services.Implementations
{
    using Contracts;
    using Data;
    using Models;
    using System.Linq;

    public class UserService : IUserService
    {
        private readonly CameraBazaarDbContext db;

        public UserService(CameraBazaarDbContext db)
        {
            this.db = db;
        }

        public UserProfileServiceModel Profile(string Username)
        {
            return this.db
                    .Users
                    .Where(u => u.UserName == Username)
                    .Select(u => new UserProfileServiceModel
                    {
                        Username = u.UserName,
                        Email = u.Email,
                        PhoneNumber = u.PhoneNumber,
                        Cameras = u.Cameras
                                    .Select(c => new AllCamerasServiceModel
                                    {
                                        Id = c.Id,
                                        Make = c.Make.ToString(),
                                        Model = c.Model,
                                        Price = c.Price.ToString("F2"),
                                        Quantity = c.Quantity,
                                        ImageURL = c.ImageURL,
                                        Username = c.User.UserName
                                    })
                    })
                    .FirstOrDefault();
        }
    }
}
