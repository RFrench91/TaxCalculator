using System;
using System.Collections.Generic;
using System.Text;

namespace TaxCalculator.Models
{
	public class Order
	{
		public float OrderAmount { get; set; }
		public float Shipping { get; set; }
		public Location CustomerLocation { get; set; }
		public Location SellerLocation { get; set; }
	}
}
