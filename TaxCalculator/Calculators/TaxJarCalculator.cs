using System;
using System.Collections.Generic;
using System.Text;
using TaxCalculator.Models;
using System.Web;
using TaxCalculator.Clients;
using TaxCalculator.Parsers;
using TaxCalculator.Entities;

namespace TaxCalculator.Calculators
{
	public class TaxJarCalculator : ITaxCalculator
	{
		private ITaxApi TaxJar { get; set; }
		private IParser<Order, TaxForOrderRequest> getTaxForOrderRequestParser { get; set; }
		private IParser<TaxForOrderResponse, TaxReturn> getTaxForOrderResponseParser { get; set; }
		private IParser<Location, TaxRatesRequest> getTaxRatesRequestParser { get; set; }
		private IParser<TaxRatesResponse, Rates> getTaxRatesResponseParser { get; set; }

		//public TaxJarCalculator()
		//{
		//	string apiKey = "5da2f821eee4035db4771edab942a4cc";
		//	string rootUrl = "https://api.taxjar.com/v2/";
		//	TaxJar = new TaxJarClient(apiKey, rootUrl);
		//}

		public TaxJarCalculator(ITaxApi taxApi, IParser<Order, TaxForOrderRequest> taxForOrderRequestParser,
			IParser<TaxForOrderResponse, TaxReturn> taxForOrderResponseParser,
			IParser<Location, TaxRatesRequest> taxRatesRequestParser,
			IParser<TaxRatesResponse, Rates> taxRatesResponseParser)
		{
			TaxJar = taxApi;
			getTaxForOrderRequestParser = taxForOrderRequestParser;
			getTaxForOrderResponseParser = taxForOrderResponseParser;
			getTaxRatesRequestParser = taxRatesRequestParser;
			getTaxRatesResponseParser = taxRatesResponseParser;
		}
		/// <summary>
		/// Get Tax amount for given order depending on location using TaxJar api
		/// </summary>
		/// <param name="orderToTax">Order that is to be taxed, includes total price and location info of seller and customer</param>
		/// <returns>Total amount of tax for order</returns>
		public TaxReturn GetTaxForOrder(Order orderToTax)
		{
			var taxJarOrderRequest = getTaxForOrderRequestParser.Parse(orderToTax);

			var response = TaxJar.GetTaxForOrder(taxJarOrderRequest);
			response.Wait();
			var returnObj = response.Result;

			TaxReturn taxes = getTaxForOrderResponseParser.Parse(returnObj);

			return taxes;
		}

		/// <summary>
		/// Calls TaxJar api to get total tax rate for given location
		/// </summary>
		/// <param name="taxLocation">Location for tax rate, contains city, state, zip and country</param>
		/// <returns>Total Tax rate for location, use Standard Tax for EU and Combined for other regions including US/CA</returns>
		public Rates GetTaxRates(Location taxLocation)
		{
			var getTaxRatesRequest = getTaxRatesRequestParser.Parse(taxLocation);

			var response = TaxJar.GetTaxRatesForLocation(getTaxRatesRequest);
			response.Wait();
			var returnTaxes = response.Result;

			Rates rates = getTaxRatesResponseParser.Parse(returnTaxes);

			return rates;
		}
	}
}
