using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserModel.RequestModel
{
    public class SignupRequestModel
    {
        public int Id { get; set; }
        public string? FullName { get; set; }
        [Required(ErrorMessage = "Mobile No. is required.")]
        public string? MobileNo { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        public string? Email { get; set; }
        public int? DistrictId { get; set; }
        public int? BlockId { get; set; }
        public int? GPId { get; set; }
        public int? RoleId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? CreatedBy { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long.")]
        [RegularExpression(
            @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&#^()_\-+=])[A-Za-z\d@$!%*?&#^()_\-+=]{8,}$",
            ErrorMessage = "Password must contain uppercase, lowercase, number and special character."
        )]

        public string? Password { get; set; }
        public string? FileBase64String { get; set; }
        public string? ProfilePhotoPath { get; set; }
        public string? Status { get; set; }
    }
}
