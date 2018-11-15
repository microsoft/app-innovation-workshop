using System;

namespace ContosoMaintenance.Web.Models
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        public Exception ServerException { get; set; }

        public bool Is404 { get; set; }
    }
}