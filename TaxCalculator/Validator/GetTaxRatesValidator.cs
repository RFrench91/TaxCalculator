using System;
using System.Collections.Generic;
using System.Text;
using TaxCalculator.Models;

namespace TaxCalculator.Validator
{
	public class GetTaxRatesValidator : IInputValidator<Location>
	{
		public ValidationResult Validate(Location location)
		{
			ValidationResult result = new ValidationResult();

			if (location == null)
			{
				result.Success = false;
				result.Message = "Location not provided";
				return result;
			}

			if (location.Country == null)
			{
				result.Success = false;
				result.Message = "Country required for tax rates";
				return result;
			}

			result.Success = true;
			return result;
		}
	}
}
