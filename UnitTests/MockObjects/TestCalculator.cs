using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxCalculator.Calculators;
using TaxCalculator.Models;

namespace UnitTests
{
	class TestCalculator : ITaxCalculator
	{
		public TaxReturn GetTaxForOrder(Order orderToTax)
		{
			var returnObj = new TaxReturn();
			if (orderToTax.CustomerLocation.Country == "CA")
			{
				returnObj.TaxesToCollect = 12;
			}
			if (orderToTax.CustomerLocation.Country == "US")
			{
				returnObj.TaxesToCollect = 8;
			}
			if (orderToTax.CustomerLocation.Country == "FR")
			{
				returnObj.TaxesToCollect = 10;
			}
			return returnObj;
		}

		public Rates GetTaxRates(Location taxLocation)
		{
			var returnObj = new Rates();
			if (taxLocation.Country == "US")
			{
				returnObj.CombinedTax = (float)0.08;
			}
			if (taxLocation.Country == "CA")
			{
				returnObj.CombinedTax = (float)0.12;
			}
			if (taxLocation.Country == "FR")
			{
				returnObj.StandardTax = (float)0.10;
			}
			return returnObj;
		}
	}
}
