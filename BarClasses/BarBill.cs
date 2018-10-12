using System;
using System.Collections.Generic;
using System.Text;

namespace Alchemint.Bar
{
    public class BarBill : IBarBill
    {
        public string Id { get ; set ; }
        public float Amount { get ; set ; }
        public float Tip { get ; set ; }
        public DateTime DateTime { get ; set ; }
        public string Comment { get ; set ; }
    }
}
