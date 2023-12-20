using TaskManagementSystem.ViewModel;

namespace TaskManagementSystem.IRepository
{
    public interface IUserService
    {


        Task<object> RegisterUserAsync(UserRegisterDTO model);

        Task<object> LoginUserAsync(LoginDTO model);

    }
}