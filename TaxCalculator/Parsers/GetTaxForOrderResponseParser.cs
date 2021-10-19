using System;
using System.Collections.Generic;
using System.Text;
using TaxCalculator.Entities;
using TaxCalculator.Models;

namespace TaxCalculator.Parsers
{
	public class GetTaxForOrderResponseParser : IParser<TaxForOrderResponse, TaxReturn>
	{
		public TaxReturn Parse(TaxForOrderResponse taxForOrderToParse)
		{
			return new TaxReturn()
			{
				TaxesToCollect = taxForOrderToParse.tax.amount_to_collect
			};
		}
	}
}
