using Web_Api_Authentication.Models;

namespace Web_Api_Authentication.Interfaces.Repository
{
    public interface IUserRepository
    {
        void PostUser(UserEntityModel model);
    }
}