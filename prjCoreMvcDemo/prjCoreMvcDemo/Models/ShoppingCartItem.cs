namespace prjCoreMvcDemo.Models
{
    public class ShoppingCartItem
    {
        // undone
        public int ProductId { get; set; }
        public int Count { get; set; }
        public decimal Price { get; set; }
        public decimal 小計 { get { return this.Price * this.Count; } }
        public TProduct Product { get; set; }
    }
}
