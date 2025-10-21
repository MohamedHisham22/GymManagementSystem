using GymManagementSystemCore.ViewModels.HomeViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystemCore.Services.Interfaces
{
    public interface IHomeServices
    {
        public AnalyticsViewModel? GetAnalytics();
    }
}
