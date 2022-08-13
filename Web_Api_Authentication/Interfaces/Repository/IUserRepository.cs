using Web_Api_Authentication.Models;

namespace Web_Api_Authentication.Interfaces.Repository
{
    public interface IUserRepository
    {
        Task<IEnumerable<UserEntityModel>> GetAllUsers();
        Task<IEnumerable<UserEntityModel>> GetUserByCode(long codigo);
        void PostUser(UserEntityModel item, UserEntityModel model);
    }
}