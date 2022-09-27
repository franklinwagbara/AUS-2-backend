using AUS2.Core.ViewModels.Dto.Request;
using System;
using System.Collections.Generic;
using System.Text;

namespace AUS2.Core.ViewModels.AdminModel
{
    public class DashboardModel
    {
        public int DeskCount { get; set; }
        public int ApprovedCount { get; set; }
        public int RejectedCount { get; set; }
        public int ProcessingCount { get; set; }
        public List<AppRequestViewModel> OnStaffDeskForFiveDays { get; set; }
        public List<AppRequestViewModel> InProcessingForThreeWeeks { get; set; }
    }
}
