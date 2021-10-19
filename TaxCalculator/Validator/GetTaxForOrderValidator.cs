using System;
using System.Collections.Generic;
using System.Text;
using TaxCalculator.Models;

namespace TaxCalculator.Validator
{
	public class GetTaxForOrderValidator : IInputValidator<Order>
	{
		public ValidationResult Validate(Order toValidate)
		{
			var successResult = new ValidationResult();
			successResult.Success = true;

			if (toValidate == null)
			{
				return new ValidationResult()
				{
					Success = false,
					Message = "Order is missing"
				};
			}

			if (toValidate.SellerLocation == null || toValidate.SellerLocation.Country == null)
			{

				return new ValidationResult()
				{
					Success = false,
					Message = "Seller location required to get tax amount"
				};
			}

			if (toValidate.CustomerLocation == null || toValidate.CustomerLocation.Country == null)
			{

				return new ValidationResult()
				{
					Success = false,
					Message = "Customer location required to get tax amount"
				};
			}

			return successResult;
		}
	}
}
