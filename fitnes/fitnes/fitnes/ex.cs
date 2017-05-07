namespace fitnes
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ex")]
    public partial class ex
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int nomer { get; set; }

        [StringLength(100)]
        public string exercises { get; set; }

        [StringLength(100)]
        public string kalorii { get; set; }
    }
}
