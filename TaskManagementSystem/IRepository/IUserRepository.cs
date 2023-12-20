using TaskManagementSystem.ViewModel;

namespace TaskManagementSystem.IRepository
{
    public interface IUserRepository
    {


        Task<object> RegisterUserAsync(UserRegisterDTO model);

        Task<object> LoginUserAsync(LoginDTO model);

    }
}