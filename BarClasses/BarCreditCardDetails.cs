using System;
using System.Collections.Generic;
using System.Text;

namespace Alchemint.Bar
{
    public class BarCreditCardDetails : IBarCreditCardDetails
    {
        public string CardNumber { get; set; }
        public string ExpiryDate { get; set; }
        public string CvvNumber { get; set; }
        public string NameOnCard { get; set; }
    }
}
