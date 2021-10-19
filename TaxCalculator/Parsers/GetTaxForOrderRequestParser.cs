using System;
using System.Collections.Generic;
using System.Text;
using TaxCalculator.Models;
using TaxCalculator.Entities;

namespace TaxCalculator.Parsers
{
	public class GetTaxForOrderRequestParser : IParser<Order, TaxForOrderRequest>
	{
		public TaxForOrderRequest Parse(Order orderToParse)
		{
			return new TaxForOrderRequest()
			{
				shipping = orderToParse.Shipping,
				amount = orderToParse.OrderAmount,
				to_zip = orderToParse.CustomerLocation.ZipCode,
				to_country = orderToParse.CustomerLocation.Country,
				to_state = orderToParse.CustomerLocation.State,
				to_city = orderToParse.CustomerLocation.City,
				from_city = orderToParse.SellerLocation.City,
				from_country = orderToParse.SellerLocation.Country,
				from_state = orderToParse.SellerLocation.State,
				from_zip = orderToParse.SellerLocation.ZipCode
			};
		}
	}
}
