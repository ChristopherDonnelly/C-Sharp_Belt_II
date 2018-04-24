using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace C_Sharp_Belt_II.Models
{
    public class RegisterViewModel : BaseEntity
    {
        [Display(Name = "Name: ")]
        [Required(ErrorMessage = "Name is required!")]
        [MinLength(2, ErrorMessage = "Name must contain at least 2 characters!")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Name can only contain letters!")]
        public string Name { get; set; }

        [Display(Name = "Alias: ")]
        [Required(ErrorMessage = "Alias is required!")]
        [MinLength(2, ErrorMessage = "Alias must contain at least 2 characters!")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Alias can only contain letters!")]
        public string Alias { get; set; }
 
        [Display(Name = "Email: ")]
        [Required(ErrorMessage = "Email is required!")]
        [EmailAddress(ErrorMessage="Please ensure that you have entered a valid email address!")]
        public string Email { get; set; }
 
        //EMAIL_REGEX = re.compile(r'^[a-zA-Z0-9.+_-]+@[a-zA-Z0-9._-]+\.[a-zA-Z]+$')
        [Display(Name = "Password: ")]
        [Required(ErrorMessage = "Password is required!")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters!")]
        // [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[$@$!%*?&])[A-Za-z\d$@$!%*?&]{8,}", ErrorMessage = "Password must contain at least 1 number, 1 uppercase, 1 lowercase, and 1 special character!")]
        // [RegularExpression(@"^((?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])|(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[^a-zA-Z0-9])|(?=.*?[A-Z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])|(?=.*?[a-z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])).{8,}$", ErrorMessage = "Passwords must be at least 8 characters and contain at 3 of 4 of the following: upper case (A-Z), lower case (a-z), number (0-9) and special character (e.g. !@#$%^&*)")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
 
        [Display(Name = "Passwrod Confirm: ")]
        [Compare("Password", ErrorMessage = "Password and confirmation must match.")]
        public string PasswordConfirmation { get; set; }

        [Range(0, 0, ErrorMessage = "Email already exists, please use unique email or login!")]
        public int Unique { get; set; }

        public User createUser(BeltExamContext _context){
            
            User newUser = new User{
                Name = this.Name,
                Alias = this.Alias,
                Email = this.Email,
                Password = this.Password
            };

            PasswordHasher<User> Hasher = new PasswordHasher<User>();
            newUser.Password = Hasher.HashPassword(newUser, newUser.Password);
            
            _context.users.Add(newUser);
            _context.SaveChanges();

            return newUser;
        }
    }
}