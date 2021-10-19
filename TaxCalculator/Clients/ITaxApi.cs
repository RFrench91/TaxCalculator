using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TaxCalculator.Entities;

namespace TaxCalculator.Clients
{
	public interface ITaxApi
	{
		Task<TaxForOrderResponse> GetTaxForOrder(TaxForOrderRequest order);

		Task<TaxRatesResponse> GetTaxRatesForLocation(TaxRatesRequest location);

	}
}
