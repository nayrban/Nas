using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NasDTOUtils.Dto.Response
{
    public class MinistryCodeResponse
    {
        public int id { get; set; }
        public string code { get; set; }
        public bool used { get; set; }
                
        public int ministryId { get; set; }
    }
}