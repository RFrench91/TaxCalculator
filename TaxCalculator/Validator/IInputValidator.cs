using System;
using System.Collections.Generic;
using System.Text;

namespace TaxCalculator.Validator
{
	public interface IInputValidator<T>
	{
		ValidationResult Validate(T toValidate);
	}
}
