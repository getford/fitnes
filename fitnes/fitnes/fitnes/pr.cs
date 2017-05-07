namespace fitnes
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("pr")]
    public partial class pr
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int nomer { get; set; }

        [StringLength(100)]
        public string product { get; set; }

        [StringLength(100)]
        public string kalorii { get; set; }
    }
}
