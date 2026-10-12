using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MunicipalServicesApp
{
    public static class IssueRepository
    {
        public static List<ReportedIssue> Issues { get; }
            = new List<ReportedIssue>();
    }
}
