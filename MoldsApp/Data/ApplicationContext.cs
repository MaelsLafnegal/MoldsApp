using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using MoldsApp.Models;

namespace MoldsApp.Data
{
    public partial class ApplicationContext : DbContext
    {
        public ApplicationContext()
            : base("name=MoldsDatabase")
        {
        }

        public virtual DbSet<Molds> Molds { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
