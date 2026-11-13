namespace ECommerce.Application.Options
{
    public class ShopOptions
    {
        /*
         * "StoreName": "ECommerce",
         * "Currency": "TRY",
         * "MaxOrderItems": 20*/

        public const string SECTION_NAME = "Shop";

        public string StoreName { get; set; } = string.Empty;
        public string Currency { get; set; } = string.Empty;
        public int MaxOrderItems { get; set; }
    }
}
