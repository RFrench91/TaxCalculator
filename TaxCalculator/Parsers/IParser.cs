using System;
using System.Collections.Generic;
using System.Text;

namespace TaxCalculator.Parsers
{
	public interface IParser<T, U>
	{
		U Parse(T inputObj);
	}
}
