using System;
using System.Collections.Generic;
using System.Text;
using TaxCalculator.Models;
using TaxCalculator.Entities;

namespace TaxCalculator.Parsers
{
	public class GetTaxRatesRequestParser : IParser<Location, TaxRatesRequest>
	{
		public TaxRatesRequest Parse(Location taxLocation)
		{
			return new TaxRatesRequest()
			{
				city = taxLocation.City,
				country = taxLocation.Country,
				state = taxLocation.State,
				zip = taxLocation.ZipCode
			};
		}
	}
}
