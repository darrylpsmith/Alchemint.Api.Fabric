using System;
using System.Collections.Generic;
using System.Text;
using Alchemint.Core;

namespace Alchemint.Bar
{
    public class PatientConsult
    {
        
        [PrimaryKey]
        public string Id { get; set; }

        [UniqueKey]
        public string PatientIdNumber { get; set; }
        [UniqueKey]
        public DateTime ConsultDate { get; set; }

        public string Comments { get; set; }

    }
}
