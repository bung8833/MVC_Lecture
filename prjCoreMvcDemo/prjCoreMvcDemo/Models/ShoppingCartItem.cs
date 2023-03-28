using System.ComponentModel;

namespace prjCoreMvcDemo.Models
{
    public class ShoppingCartItem
    {
        public int ProductId { get; set; }
        [DisplayName("採購量")]
        public int Count { get; set; }
        [DisplayName("金額")]
        public decimal Price { get; set; }
        public decimal 小計 { get { return this.Price * this.Count; } }
        public TProduct Product { get; set; }
    }
}
