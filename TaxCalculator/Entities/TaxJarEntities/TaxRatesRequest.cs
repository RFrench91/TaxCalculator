using System;
using System.Collections.Generic;
using System.Text;

namespace TaxCalculator.Entities
{
	public class TaxRatesRequest
	{
		public string country { get; set; }
		public string zip { get; set; }
		public string state { get; set; }
		public string city { get; set; }
		public string street { get; set; }
	}
}
