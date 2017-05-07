namespace fitnes
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
            //data source =.\SQLEXPRESS; initial catalog = PRODUCT;
            //integrated security = True; MultipleActiveResultSets = True; App = EntityFramework
        }

        public virtual DbSet<ex> ex { get; set; }
        public virtual DbSet<pr> pr { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
