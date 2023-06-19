using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class UrlCreatingModel
    {
        public string LongUrl { get; set; }
        public Guid Creator { get; set; }

    }
}
