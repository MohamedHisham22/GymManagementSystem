using GymManagementSystemDAL.Models.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystemCore.ViewModels.MemberViewModels
{
    internal class CreateMemberViewModel
    {
        [Required(ErrorMessage = "Name Is Required")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Name Must Be Between 2 And 50 Char")]
        [RegularExpression(@"^[\w\s]+$", ErrorMessage = "Name Can Contain Only Letters And Spaces")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Email Is Required")]
        [EmailAddress(ErrorMessage = "Invalid email Format")] // Validation
        [DataType(DataType.EmailAddress)] // UI HINT
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Email Must Be Between 5 and 100 Char")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Phone Is Required")]
        [Phone(ErrorMessage = "Invalid Phone Format")] // Validation
        [RegularExpression(@"^(010|011|012|015)\d{8}$", ErrorMessage = "Phone Number Must Be Valid Egyptian PhoneNumber")]
        [DataType(DataType.PhoneNumber)] // UI HINT
        public string Phone { get; set; } = null!;
        
        [Required(ErrorMessage = "Date Of Birth Is Required")]
        [DataType(DataType.Date)] // UI HINT
        public DateOnly DateOfBirth { get; set; }

        [Required(ErrorMessage = "Gender Is Required")]
        public Gender Gender { get; set; }

        [Required(ErrorMessage = "Building Number Is Required")]
        [Range(1, 9000, ErrorMessage = "Building Number Must Be Between 1 and 9000")]
        public int BuildingNumber { get; set; }

        [Required(ErrorMessage = "Street Is Required")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Street Must Be Between 2 and 30 Chars")]
        public string Street { get; set; } = null!;

        [Required(ErrorMessage = "City Is Required")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "City Must Be Between 2 and 30 Chars")]
        [RegularExpression(@"^[\w\s]+$", ErrorMessage = "City Can Contain Only Letters And Spaces")]
        public string City { get; set; } = null!;

        #region Health Record Properties Way01 (if its properties might be needed somewhere else)
        [Required(ErrorMessage = "Health Record Is Required")]
        public MemberHealthRecordViewModel HealthRecord { get; set; } = null!;
        #endregion

        #region Health Record Properties Way02 (if its properties are only needed here)
        //[Required(ErrorMessage = "Height Is Required")]
        //[Range(0.1, 300, ErrorMessage = "Height Must Be Greater Than 0 And Less Than 300")]
        //public decimal Height { get; set; }

        //[Required(ErrorMessage = "Weight Is Required")]
        //[Range(0.1, 500, ErrorMessage = "Weight Must Be Greater Than 0 And Less Than 500")]
        //public decimal Weight { get; set; }

        //[Required(ErrorMessage = "BloodType Is Required")]
        //public BloodType BloodType { get; set; }

        //public string? Note { get; set; }

        #endregion
    }

}
