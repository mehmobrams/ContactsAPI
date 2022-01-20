using ContactsAPI.Models;

namespace ContactsAPI.Repositories
{
    public class UserRepository
    {
        public static List<User> Users = new()
        {
            new() { Username = "mehmo_admin", EmailAddress = "mehmo.admin@email.com", Password = "MyPassw0rd", GivenName = "Mem", Surname = "Ali", Role = "Administrator" },
            new() { Username = "mehmo_standard", EmailAddress = "mehmo.standard@email.com", Password = "MyPassw0rd", GivenName = "Mam", Surname = "Brams", Role = "Standard" },
        };
    }
}
