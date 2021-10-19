using System;
using System.Collections.Generic;
using System.Text;
using TaxCalculator.Entities;
using TaxCalculator.Models;

namespace TaxCalculator.Parsers
{
	public class GetTaxRatesResponseParser : IParser<TaxRatesResponse, Rates>
	{
		public Rates Parse(TaxRatesResponse apiTaxRates)
		{
			return new Rates()
			{
				CombinedTax = apiTaxRates.rate.combined_rate,
				StandardTax = apiTaxRates.rate.standard_rate
			};
		}
	}
}
