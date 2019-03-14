namespace FinalExam
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class ProductContext : DbContext
    {
        public ProductContext(): base("name=ProductContext"){}
        
        public  DbSet<Product> Products { get; set; }
    }
}