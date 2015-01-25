using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sample.Models
{
    public class GlossaryViewModel : BaseViewModel
    {
        public List<TermResponseModel> GlossaryOfTerms { get; set; }
    }
}