using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystemCore.ViewModels.MemberSessionViewModels
{
    public class OngoingSessionViewModel
    {
        public int MemberId { get; set; }

        public int SessionId { get; set; }

        [Display(Name = "Member Name")]
        public string MemberName { get; set; } = null!;

        public bool isAtended { get; set; }
    }
}
