using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace sample.Models
{
    [DataContract]
    public class TermRequestModel
    {
        [DataMember]
        public Guid? ExternalId { get; set; }

        [DataMember(IsRequired = true)]
        [Required(ErrorMessage = "A term is required")]
        [StringLength(100, ErrorMessage =
          "Only terms of 100 characters or fewer are accepted.")]
        public string Term { get; set; }

        [DataMember(IsRequired = true)]
        [Required(ErrorMessage = "A definition is required")]
        public string Definition { get; set; }
    }
}