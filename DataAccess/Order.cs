//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataAccess
{
    using System;
    using System.Collections.Generic;
    
    public partial class Order
    {
        public int OrderID { get; set; }
        public Nullable<int> Product_ID { get; set; }
        public Nullable<int> BuyerID { get; set; }
        public string Order_Status { get; set; }
    }
}
