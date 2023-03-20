//------------------------------------------------------------------------------
// <auto-generated>
//     這個程式碼是由範本產生。
//
//     對這個檔案進行手動變更可能導致您的應用程式產生未預期的行為。
//     如果重新產生程式碼，將會覆寫對這個檔案的手動變更。
// </auto-generated>
//------------------------------------------------------------------------------

namespace prjMvcDemo
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Web;

    public partial class tProduct
    {
        public int fId { get; set; }

        [DisplayName("品名")]
        [Required(ErrorMessage = "品名是必填欄位")]
        public string fName { get; set; }

        [DisplayName("數量")]
        [Required(ErrorMessage = "數量是必填欄位")]
        public Nullable<int> fQty { get; set; }

        [DisplayName("成本")]
        [Required(ErrorMessage = "成本是必填欄位")]
        public Nullable<decimal> fCost { get; set; }

        [DisplayName("售價")]
        [Required(ErrorMessage = "售價是必填欄位")]
        public Nullable<decimal> fPrice { get; set; }


        public string fImagePath { get; set; }


        public HttpPostedFileBase photo { get; set; }
    }
}
