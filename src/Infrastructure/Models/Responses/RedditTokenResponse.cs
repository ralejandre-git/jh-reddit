﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Models.Responses;
public class RedditTokenResponse
{
    [JsonProperty("access_token")]
    public string AccessToken { get; set; } = default!;

    [JsonProperty("token_type")]
    public string TokenType { get; set; } = default!;

    [JsonProperty("expires_in")]
    public int? ExpiresIn { get; set; }

    [JsonProperty("scope")]
    public string Scope { get; set; } = default!;
}
