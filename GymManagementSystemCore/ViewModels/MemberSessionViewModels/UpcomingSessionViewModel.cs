using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystemCore.ViewModels.MemberSessionViewModels
{
    public class UpcomingSessionViewModel
    {
        public int MemberId { get; set; }

        public int SessionId { get; set; }

        [Display(Name = "Member Name")]
        public string MemberName { get; set; } = null!;

        [Display(Name = "Booking Date")]
        public DateTime BookingDate { get; set; }

        //computed proprty
        public string BookingDateDisplay => $"{BookingDate: MM/dd/yyyy hh:mm:ss tt}"; 
    }
}
