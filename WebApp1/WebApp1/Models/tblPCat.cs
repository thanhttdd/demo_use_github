namespace WebApp1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tblPCat")]
    public partial class tblPCat
    {
        public int Id { get; set; }

        public int? ParentId { get; set; }

        [StringLength(50)]
        public string Name { get; set; }
    }
}
