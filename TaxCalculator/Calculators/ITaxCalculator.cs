using System;
using System.Collections.Generic;
using System.Text;
using TaxCalculator.Models;

namespace TaxCalculator.Calculators
{
	public interface ITaxCalculator
	{
		Rates GetTaxRates(Location taxLocation);

		TaxReturn GetTaxForOrder(Order orderToTax);
	}
}
