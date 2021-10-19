using System;
using System.Collections.Generic;
using System.Text;
using TaxCalculator.Models;

namespace TaxCalculator.Services
{
	public interface ITaxService
	{

		/// <summary>
		/// Gets
		/// </summary>
		/// <returns></returns>
		Rates GetTaxRates(Location taxLocation);

		TaxReturn GetTaxForOrder(Order orderToTax);

	}
}
