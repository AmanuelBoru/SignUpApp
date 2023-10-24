using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class UserCrud : IUserCrud
    {
        private readonly UsersDBContext _dbContext;
        public UserCrud(UsersDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddAUser(User user)
        {
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
        }

        public List<User> GetAllUsers()
        {
            return _dbContext.Users.ToList();
        }

        public bool IsUserEmailUnique(string email)
        {
            User? user = _dbContext.Users.FirstOrDefault(x => x.Email == email);
            if (user != null) return false;
            else return true;
        }
    }
}
