namespace WebApp1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tblProduct")]
    public partial class tblProduct
    {
        public int Id { get; set; }

        public int? PCatId { get; set; }

        [StringLength(50)]
        public string Name { get; set; }
    }
}
