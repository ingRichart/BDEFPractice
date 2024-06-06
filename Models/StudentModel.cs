using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUDProductCatalog.Models
{
    public class StudentModel
    {
        public StudentModel()
        {
        }

        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public int Tetra { get; set; }
        public double Cuota { get; set; }
    }
}