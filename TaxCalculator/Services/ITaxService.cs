using System;
using System.Collections.Generic;
using System.Text;
using TaxCalculator.Models;

namespace TaxCalculator.Services
{
	/// <summary>
	/// Defines Service for grabbing taxes from an underlying Tax Calculator
	/// </summary>
	public interface ITaxService
	{

		/// <summary>
		/// Gets Tax rates for given location
		/// </summary>
		/// <returns></returns>
		Rates GetTaxRates(Location taxLocation);

		/// <summary>
		/// Gets total tax amount for given location and order amount
		/// </summary>
		/// <param name="orderToTax"></param>
		/// <returns></returns>
		TaxReturn GetTaxForOrder(Order orderToTax);

	}
}
