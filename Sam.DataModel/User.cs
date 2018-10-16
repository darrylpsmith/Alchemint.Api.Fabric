using System;
using System.Collections.Generic;
using System.Text;
using Alchemint.Core;
namespace Sam.DataModel
{
    public class User
    {
        public User()
        {
        }

        [PrimaryKey]
        public string Id { get; set; }
        [UniqueKey]
        public string UserName { get; set; }
        public string Password { get; set; }


        public string Email { get; set; }


    }
}
