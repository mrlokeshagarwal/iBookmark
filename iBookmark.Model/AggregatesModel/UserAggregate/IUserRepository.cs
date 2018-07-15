using System.Threading.Tasks;

namespace iBookmark.Model.AggregatesModel.UserAggregate
{
    public interface IUserRepository
    {
        Task<int> InsertUpdateUserAsync(UserModel user);
        Task<UserModel> ValidateUserAsync(LoginModel login);
        Task<UserModel> GetUserAsync(int userId);

    }
}
