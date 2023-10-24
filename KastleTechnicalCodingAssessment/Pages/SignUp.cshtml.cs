using DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace KastleTechnicalCodingAssessment.Pages
{
    public class SignUpModel : PageModel
    {
        private readonly IUserCrud _userCrud;
        public User user { get; set; }
        public bool IsSuccess { get; set; }
        public string StatusMessage { get; set; } = "";

        // Data Annotations in conjunction with asp-validation-for provide Client side Validation.
        // This attributes Binds the Property to the HTML code and Requires the input
        [BindProperty]
        [Required(ErrorMessage ="Please Input First Name")]
        public string FirstName { get; set; } = "";

        // This attributes Binds the Property to the HTML code and Requires the input
        [BindProperty]
        [Required(ErrorMessage = "Please Input Last Name")]
        public string LastName { get; set; } = "";

        // This attributes Binds the Property to the HTML code and Requires the input. It also has a regex that checks if it is a valid email address
        [BindProperty]
        [Required(ErrorMessage = "Please Input Email")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Please input a valid email")]
        public string Email { get; set; } = "";

        // This attributes Binds the Property to the HTML code and Requires the input. It also has a regex that checks if the password meets requirements.
        [BindProperty]
        [Required(ErrorMessage = "Please Input Password")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@#$%^&+=!*_])(.{10,50})$", ErrorMessage = "Password requirements must be met: must be between 10 and 50 characters,must be at least 1 lower case, must be at least 1 upper case, must be at least 1 number, must be at least 1 special character")]
        public string Password { get; set; } = "";

        // This attributes Binds the Property to the HTML code and Requires the input. It also checks if the password and the confirmation are matching.
        [BindProperty]
        [Required(ErrorMessage = "Please Confirm Password")]
        [Compare(nameof(Password), ErrorMessage = "Passwords must match")]
        public string passwordConfirmation { get; set; } = "";

        public SignUpModel(IUserCrud userCrud)
        {
            _userCrud = userCrud;
        }
        public void OnGet()
        {
        }

        public void OnPost() {
            //if the inputs are not valid
            if (!ModelState.IsValid)
            {
                IsSuccess = false;
                StatusMessage = "Invalid input, Please check your submission";
                return;
            }
            //this checks if the email is unique from the DB. This is a server side check
            else if (!_userCrud.IsUserEmailUnique(Email))
            {
                IsSuccess = false;
                StatusMessage = "Email Already exists";
                return;
            }

            //If a valid input is enterd it will submit the code.
            SubmitUser();
            
            return;
        }

        /// <summary>
        /// This Method adds the user information in to the DB.
        /// </summary>
        private void SubmitUser()
        {
            try
            {
                IsSuccess = true;
                StatusMessage = "Form Submitted";
                user = new User(FirstName, LastName, Email, Password);
                _userCrud.AddAUser(user);
                ModelState.Clear();
                ResetTheState();
            }
            catch
            {
                IsSuccess = false;
                StatusMessage = "Unexpected error stopped the prosses. Please try again";
            }
            
        }

        /// <summary>
        /// THis method resets the Properties to empty state once the value has been submitted.
        /// </summary>
        private void ResetTheState()
        {
            FirstName = "";
            LastName = "";
            Email = "";
            Password = "";
            passwordConfirmation = "";
        }
    }
}
