using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystemCore.ViewModels.PlanViewModels
{
    public class PlanToUpdateViewModel
    {
        [Required (ErrorMessage ="Plan Name Is Required")]
        [StringLength(50 , ErrorMessage = "Name Max Length Is 50 Characters")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Plan Description Is Required")]
        [StringLength(50, MinimumLength = 5 ,ErrorMessage = "Description Length Must Be Between 5 And 50 Characters")]
        public string Description { get; set; } = null!;

        [Required(ErrorMessage = "Duaration Days Is Required")]
        [Range(1,365 , ErrorMessage = "Days Must Be Between 1 and 365")]
        public int DurationDays { get; set; }
        
        [Required(ErrorMessage = "Price Is Required")]
        [Range(0.1,10000,ErrorMessage = "Price Must Be Between 0.1 And 10000")]
        public decimal Price { get; set; }
    }
}
