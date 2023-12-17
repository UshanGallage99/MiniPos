namespace MiniPos.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }

        public string? Code { get; set; }

        public string? Name { get; set; }

        public double? Price { get; set; }

        public int? Qty { get; set; }

        public double? MRP { get; set; }

        public int? MinQty { get; set; }

        public double? DefaultDiscount { get; set; }

        public double? MinDiscount { get; set; }

        public double? MaxDiscount { get; set; }
    }
}
