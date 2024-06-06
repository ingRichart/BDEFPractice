using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUDProductCatalog.Models
{
    public class PatientModel
    {
        public PatientModel()
        {
        }

        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string Email { get; set; }
        public DateTime Birth { get; set; }
    }
}