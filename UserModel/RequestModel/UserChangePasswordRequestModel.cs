using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserModel.RequestModel
{
    public class UserChangePasswordRequestModel
    {
        public int UserId { get; set; }

        [Required(ErrorMessage = "OldPassword is required.")]
        public string? OldPassword { get; set; }

        [Required(ErrorMessage = "NewPassword is required.")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long.")]
        [RegularExpression(
            @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&#^()_\-+=])[A-Za-z\d@$!%*?&#^()_\-+=]{8,}$",
            ErrorMessage = "Password must contain uppercase, lowercase, number and special character."
        )]
        public string? NewPassword { get; set; }
    }
}
