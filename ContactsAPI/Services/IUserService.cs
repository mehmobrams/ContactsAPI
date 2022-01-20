using ContactsAPI.Models;

namespace ContactsAPI.Services
{
    public interface IUserService
    {
        public User Get(UserLogin userLogin);
    }
}
