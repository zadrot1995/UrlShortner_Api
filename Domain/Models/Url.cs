using Domain.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Url : Entity
    {
        public string ShortUrl { get; set; }
        public string LongUrl { get; set; }
        public Guid CreatorId { get; set; }
        public User Creator { get; set; }
    }
}
