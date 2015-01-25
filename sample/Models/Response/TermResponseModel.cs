using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sample.Models
{
    public class TermResponseModel : BaseResponseModel
    {
        public string Term { get; set; }

        public string Definition { get; set; }

        public Guid ExternalId { get; set; }
    }
}