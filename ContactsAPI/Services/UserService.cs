using ContactsAPI.Data;
using ContactsAPI.Models;
using ContactsAPI.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ContactsAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IConfiguration _configuration;

        public UserService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public User Get(UserLogin userLogin)
        {
            User user = UserRepository.Users.FirstOrDefault(o => o.Username.Equals
            (userLogin.Username, StringComparison.OrdinalIgnoreCase) && o.Password.Equals
            (userLogin.Password));

            return user;
        }

        public User GetNew(UserLogin userLogin)
        {
            var connectionstring = _configuration.GetConnectionString("ContactsDB");

            var optionsBuilder = new DbContextOptionsBuilder<ContactsdbContext>();
            optionsBuilder.UseSqlServer(connectionstring);


            ContactsdbContext dbContext = new ContactsdbContext(optionsBuilder.Options);

            // Or you can also instantiate inside using

            //using (ContactsdbContext dbContext = new ContactsdbContext(optionsBuilder.Options))
            //{
            //    //...do stuff
            //}


            User user = dbContext.Users.FirstOrDefault(o => o.Username.Equals
           (userLogin.Username, StringComparison.OrdinalIgnoreCase) && o.Password.Equals
           (userLogin.Password, StringComparison.Ordinal));


            return user;
        }
    }
}
