using System;
using System.Collections.Generic;
using System.Text;
using Alchemint.Core;

namespace Alchemint.Bar
{

    public class BarUser : IBarUser
    {
        public BarUser()
        {
        }

        [PrimaryKey]
        public string Id { get; set; }

        [UniqueKey]
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }

    }
}
