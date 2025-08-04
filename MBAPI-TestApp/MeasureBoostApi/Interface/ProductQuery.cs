namespace MeasureBoostApi.Interface
{
    public class ProductQuery
    {
        public string searchString { get; set; } = "";
        public bool isSKU { get; set; } = false;
        public string? limitToProductType { get; set; } = "";
    }
}
