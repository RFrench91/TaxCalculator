using System;
using System.Collections.Generic;
using System.Text;

namespace TaxCalculator.Entities
{

	/// <summary>
	/// JSON Object that will be returned by TaxForOrder TaxJar api call
	/// </summary>
	public class TaxForOrderResponse
	{
		public OrderResponse tax { get; set; }
	}

	public class OrderResponse
	{
		public float order_total_amount { get; set; }
		public float shipping { get; set; }
		public float taxable_amount { get; set; }
		public float amount_to_collect { get; set; }
		public float rate { get; set; }
		public bool has_nexus { get; set; }
		public bool freight_taxable { get; set; }
		public string tax_source { get; set; }
		public string exemption_type { get; set; }
		public object jurisdictions { get; set; }
		public object breakdown { get; set; }
	}
}
