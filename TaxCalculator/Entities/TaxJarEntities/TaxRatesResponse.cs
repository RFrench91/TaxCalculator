using System;
using System.Collections.Generic;
using System.Text;

namespace TaxCalculator.Entities
{
	public class TaxRatesResponse
	{
		public rates rate { get; set; }
	}

	public class rates
	{
		public string zip { get; set; }
		public string country { get; set; }
		public float country_rate { get; set; }
		public string state { get; set; }
		public float state_rate { get; set; }

		public string county { get; set; }
		public float county_rate { get; set; }
		public string city { get; set; }
		public float city_rate { get; set; }
		public float combined_district_rate { get; set; }

		public float combined_rate { get; set; }
		public bool freight_taxable { get; set; }
		public float standard_rate { get; set; }
		public float reduced_rate { get; set; }
		public float super_reduced_rate { get; set; }
		public float parking_rate { get; set; }
		public float distance_sale_threshold { get; set; }
	}
}
