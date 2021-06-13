namespace MoldsApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Molds
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(50)]
        public string Type { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(50)]
        public string Kus { get; set; }

        public int? Matrix_Amount { get; set; }

        public int? Punch_Amount { get; set; }

        public int? Ejector_Amount { get; set; }

        public int? Centre_Amount { get; set; }

        [StringLength(50)]
        public string Punch_Size { get; set; }

        public double? Center_Size { get; set; }

        public double? Matrix_Height { get; set; }

        [StringLength(50)]
        public string Press { get; set; }

        public string Note { get; set; }
    }
}
