namespace SalesData.BL.DomainModels
{
    public class Sale
    {
        public int Id { get; set; }

        public ArticleNumber ArticleNumber { get; set; }
        
        public double SalesPrice { get; set; }
    }
}
