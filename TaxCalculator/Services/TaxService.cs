using System;
using System.Collections.Generic;
using System.Text;
using TaxCalculator.Calculators;
using TaxCalculator.Models;
using TaxCalculator.Validator;

namespace TaxCalculator.Services
{
	public class TaxService : ITaxService
	{
		private ITaxCalculator taxCalulator { get; set; }
		private IInputValidator<Order> getTaxForOrderValidator { get; set; }
		private IInputValidator<Location> getTaxRatesValidator { get; set; }

		public TaxService(ITaxCalculator calculator, IInputValidator<Order> taxForOrderValidator,
			IInputValidator<Location> taxRatesValidator)
		{
			taxCalulator = calculator;
			getTaxForOrderValidator = taxForOrderValidator;
			getTaxRatesValidator = taxRatesValidator;
		}


		/// <summary>
		/// Gets tax amount for given order
		/// </summary>
		/// <param name="orderToTax">Contains amount of transaction and location of seller and customer</param>
		/// <returns>Returns total amount of taxes to collect on order</returns>
		public TaxReturn GetTaxForOrder(Order orderToTax)
		{
			var validationResult = getTaxForOrderValidator.Validate(orderToTax);
			if (!validationResult.Success)
			{
				throw new Exception(validationResult.Message);
			}
			return taxCalulator.GetTaxForOrder(orderToTax);
		}

		/// <summary>
		/// Gets total tax rate for given location
		/// </summary>
		/// <param name="taxLocation">City, state, zip and country of location user wants tax rates of</param>
		/// <returns>Total tax rate</returns>
		public Rates GetTaxRates(Location taxLocation)
		{
			var validationResult = getTaxRatesValidator.Validate(taxLocation);
			if (validationResult.Success == false)
			{
				throw new Exception(validationResult.Message);
			}
			return taxCalulator.GetTaxRates(taxLocation);
		}
	}
}
