using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class SubRedditTop
    {
        public string SubRedditName { get; set; } = default!;

        public string SubRedditTimeFrameType { get; set; } = default!;

        public ushort Limit { get; set; }
    }
}
