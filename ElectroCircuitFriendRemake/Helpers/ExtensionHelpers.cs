using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Primitives;

namespace ElectroCircuitFriendRemake.Helpers
{
    public static class ExtensionHelpers
    {
        public static string GetQueryParam(this string url, string queryParam)
        {
            Uri myUri = new Uri(url);
            var queryParams = Microsoft.AspNetCore.WebUtilities.QueryHelpers.ParseQuery(myUri.Query);
            return queryParams.TryGetValue(queryParam, out StringValues queryValue) ? queryValue[0] : null;
        }
    }
}
