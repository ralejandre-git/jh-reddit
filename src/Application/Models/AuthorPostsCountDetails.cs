using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class AuthorPostsCountDetails
    {
        public int BatchId { get; set; }
        public string Author { get; set; } = default!;
        public int NumberOfPosts { get; set; }
        public IEnumerable<InsertSubRedditPosts>? Posts { get; set; }
    }
}
