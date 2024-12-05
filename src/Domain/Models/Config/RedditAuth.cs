using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Config
{
    public class RedditAuth
    {
        public string ClientId { get; set; } = default!;
        public string Secret { get; set; } = default!;
    }
}
