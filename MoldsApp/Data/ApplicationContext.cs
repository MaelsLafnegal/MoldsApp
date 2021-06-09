using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using MoldsApp.Models;

namespace MoldsApp.Data
{
    public partial class ApplicationContext : DbContext
    {
        static ApplicationContext()
        {
            Database.SetInitializer<ApplicationContext>(new MoldsInitializer());
        }

        private static ApplicationContext _context;
        public ApplicationContext()
            : base("name=MoldsDatabase")
        {
        }

        public static ApplicationContext GetContext()
        {
            if (_context == null)
                _context = new ApplicationContext();
            return _context;
        }

        public virtual DbSet<Molds> Molds { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }

        
    }
}
