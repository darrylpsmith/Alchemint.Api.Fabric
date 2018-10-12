using System;
using System.Collections.Generic;
using System.Text;
using Alchemint.Core;

namespace Alchemint.Bar
{
    public class ApiKey
    {
        [PrimaryKey]
        public string Id { get; set; }
        [UniqueKey]
        public string ApiKeyValue { get; set; }

        [UniqueKey]
        public string TenantCode { get; set; }

    }
}
