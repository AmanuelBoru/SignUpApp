using DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KastleTechnicalCodingAssessment.Pages
{
    public class UsersModel : PageModel
    {
        private readonly IUserCrud _userCrud;
        public List<User> UsersList;
        public string Error { get; set; } = "";
        public UsersModel(IUserCrud userCrud)
        {
            _userCrud = userCrud;
        }
        public void OnGet()
        {
            try
            {
                UsersList = _userCrud.GetAllUsers();
            }
            catch
            {
                Error = "An Expected Error occured. Please Refresh";
            }
        }
    }
}
