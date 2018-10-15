using System;
using System.Collections.Generic;
using System.Text;
using Alchemint.Client.JsonAccess ;

namespace Sam.Api.UnitTests
{
    public static class StaticFunctions
    {
     
        public static FabricJsonAccess FabricAccess = new FabricJsonAccess(
            StaticFunctions.GetApiUrl(),
            "33c35730-2deb-44ae-9a16-1dec27960052");



        public static string GetApiUrl ()
        {

            string env = "Development"; // Environment.GetEnvironmentVariable("TEST_ENVIRONMENT");
            if (env == "Development")
            {
                return @"https://localhost:5001/api/";
            }
            else if (env == "DevDocker")
            {
                return @"https://localhost:44329/api/";
            }
            else
            {
                return "";
            }
        }

    }
}
