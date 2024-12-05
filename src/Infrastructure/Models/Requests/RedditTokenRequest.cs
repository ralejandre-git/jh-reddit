using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Models.Requests;

public record RedditTokenRequest([JsonProperty("grant_type")] string grant_type = "client_credentials");
