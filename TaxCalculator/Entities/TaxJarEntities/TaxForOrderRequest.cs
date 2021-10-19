using System;
using System.Collections.Generic;
using System.Text;

namespace TaxCalculator.Entities
{
	public class TaxForOrderRequest
	{
		public string from_country { get; set; }
		public string from_zip { get; set; }
		public string from_state { get; set; }
		public string from_city { get; set; }
		public string from_street { get; set; }
		public string to_country { get; set; }
		public string to_zip { get; set; }
		public string to_state { get; set; }
		public string to_city { get; set; }
		public float amount { get; set; }
		public float shipping { get; set; }
		public string customer_id { get; set; }
		public string exemption_type { get; set; }
		public nexus_address[] nexus_addresses { get; set; }
		public line_item[] line_items { get; set; }
		public string to_street { get; set; }
	}

	public class nexus_address
	{
		public string id { get; set; }
		public string country { get; set; }
		public string zip { get; set; }
		public string state { get; set; }
		public string city { get; set; }
		public string street { get; set; }
	}

	public class line_item
	{
		public string id { get; set; }
		public string product_tax_code { get; set; }
		public int quantity { get; set; }
		public float unit_price { get; set; }
		public float discount { get; set; }
	}
}
