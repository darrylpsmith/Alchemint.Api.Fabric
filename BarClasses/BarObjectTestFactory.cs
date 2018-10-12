using System;
using System.Collections.Generic;
using System.Text;

namespace Alchemint.Bar
{
    public static class BarObjectTestFactory
    {
        public static BarInstitution GetBarInstitution ()
        {
            return new BarInstitution { Name = "The George", Id = new Guid().ToString(), Email = "test@testgeorge.co.za", Password = "abc", Telephone = "555-5555" };
        }
            
        public static BarUser GetBarUser ()
        {
            return new BarUser { UserName = "darryl", Id = new Guid().ToString(), Telephone = "555-5555", Password = "pswd", Email = "darryl@alchemint.com" };
        }

        public static BarUser GetBarUserReceiver()
        {
            return new BarUser { UserName = "grant", Id = new Guid().ToString(), Telephone = "555-5555", Password = "pswd", Email = "grant@alchemint.com" };
        }

        public static BarCreditCardDetails GetBarCreditCardDetails()
        {
            return new BarCreditCardDetails { CardNumber = "1111-1111-1111-1111", ExpiryDate = "10/19", CvvNumber = "555", NameOnCard = "MR J.Bloggs" };
        }

        public static BarWallet GetBarWallet ()
        {
            return new BarWallet { PublicKey = "asdasdasd", PrivateKey = "kkkkk", ReceiveAddress = "receiveadd1", CreationTime = DateTime.UtcNow };
        }
    }
}
