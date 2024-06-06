using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace CRUDProductCatalog.Entities
{
    public class Student
    {
        public Guid Id { get; set; }
        [StringLength(250)]
        public string? Name { get; set; }
        [StringLength(250)]
        public string? LastName { get; set; }
        public int Tetra { get; set; }
        public double Cuota { get; set; }

    }
}