using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface IUserCrud
    {
        List<User> GetAllUsers();
        void AddAUser(User user);
        bool IsUserEmailUnique(string email);
    }
}
