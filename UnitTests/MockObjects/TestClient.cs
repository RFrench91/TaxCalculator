using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxCalculator.Clients;
using TaxCalculator.Entities;

namespace UnitTests
{
	public class TestClient : ITaxApi
	{
		public async Task<TaxForOrderResponse> GetTaxForOrder(TaxForOrderRequest order)
		{
			var amountToCollect = 0;

			if (order.to_country == "US")
			{
				amountToCollect = 8;
			}
			if (order.to_country == "CA")
			{
				amountToCollect = 12;
			}
			if (order.to_country == "FR")
			{
				amountToCollect = 10;
			}

			TaxForOrderResponse response = new TaxForOrderResponse();
			response.tax = new OrderResponse() { amount_to_collect = amountToCollect };
			return await Task.Run(async () =>
			{
				await Task.Delay(500);
				return response;
			});
		}

		public async Task<TaxRatesResponse> GetTaxRatesForLocation(TaxRatesRequest location)
		{
			float combinedrate = 0;
			if (location.country == "US")
			{
				combinedrate = 0.08F;
			}
			if (location.country == "CA")
			{
				combinedrate = 0.12F;
			}
			if (location.country == "FR")
			{
				combinedrate = 0.1F;
			}

			TaxRatesResponse response = new TaxRatesResponse();
			response.rate = new rates() { combined_rate = combinedrate };
			return await Task.Run(async () =>
			{
				await Task.Delay(500);
				return response;
			});
		}
	}
}
