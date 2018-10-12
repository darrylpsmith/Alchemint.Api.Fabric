using System;
using System.Collections.Generic;
using System.Text;
using Alchemint.Core;

namespace Alchemint.Bar
{
    public interface IBarBill
    {
        [PrimaryKey]
        string Id { get; set; }
        float Amount { get; set; } 
        float Tip { get; set; }
        DateTime DateTime { get; set; }
        string Comment { get; set; }
    }
}
