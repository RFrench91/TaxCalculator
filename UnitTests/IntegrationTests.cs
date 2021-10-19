using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TaxCalculator.Calculators;
using TaxCalculator.Clients;
using TaxCalculator.Models;
using TaxCalculator.Services;
using TaxCalculator.Validator;
using TaxCalculator.Parsers;
using System.Net.Http;

namespace UnitTests
{
	[TestClass]
	public class IntegrationTests
	{
		private const string apiKey = "5da2f821eee4035db4771edab942a4cc";
		private const string rootUrl = "https://api.taxjar.com/v2/";

		private TaxService InitializeService()
		{
			TaxJarClient client = new TaxJarClient(new HttpClient(), apiKey, rootUrl);
			var orderRequestParser = new GetTaxForOrderRequestParser();
			var orderResponseParser = new GetTaxForOrderResponseParser();
			var ratesRequestParser = new GetTaxRatesRequestParser();
			var ratesResponseParser = new GetTaxRatesResponseParser();
			TaxJarCalculator calculator = new TaxJarCalculator(client, orderRequestParser,
				orderResponseParser, ratesRequestParser, ratesResponseParser);
			var orderValidator = new GetTaxForOrderValidator();
			var locationValidator = new GetTaxRatesValidator();
			TaxService service = new TaxService(calculator, orderValidator, locationValidator);
			return service;
		}

		[TestMethod]
		public void Integration_GetTaxRates_US_08()
		{
			TaxService service = InitializeService();
			Location location = CreateTestLocationUS();

			var rate = service.GetTaxRates(location);
			Assert.AreEqual<float>(0.08F, rate.CombinedTax);
		}

		[TestMethod]
		public void Integration_GetTaxRates_CA_12()
		{
			TaxService service = InitializeService();
			Location location = CreateTestLocationCA();

			var rate = service.GetTaxRates(location);
			Assert.AreEqual<float>(0.12F, rate.CombinedTax);
		}

		[TestMethod]
		public void Integration_GetTaxForOrder_US_8()
		{
			TaxService service = InitializeService();
			Order testOrder = CreateTestOrderUS();

			var result = service.GetTaxForOrder(testOrder);
			Assert.AreEqual<float>(8F, result.TaxesToCollect);
		}

		[TestMethod]
		public void Integration_GetTaxForOrder_CA_12()
		{
			TaxService service = InitializeService();
			Order testOrder = CreateTestOrderCA();

			var result = service.GetTaxForOrder(testOrder);
			Assert.AreEqual<float>(12F, result.TaxesToCollect);
		}

		private Order CreateTestOrderUS()
		{
			return new Order()
			{
				CustomerLocation = CreateTestLocationUS(),
				SellerLocation = CreateTestLocationUS(),
				OrderAmount = 100,
				Shipping = 0
			};
		}

		private Order CreateTestOrderCA()
		{
			return new Order()
			{
				CustomerLocation = CreateTestLocationCA(),
				SellerLocation = CreateTestLocationCA(),
				OrderAmount = 100,
				Shipping = 0
			};
		}

		private Location CreateTestLocationUS()
		{
			Location location = new Location();
			location.City = "Schenectady";
			location.State = "NY";
			location.Country = "US";
			location.ZipCode = "12303";
			return location;
		}

		private Location CreateTestLocationCA()
		{
			Location location = new Location();
			location.Country = "CA";
			location.ZipCode = "V5K0A1";
			location.State = "BC";
			location.City = "Vancouver";
			return location;
		}
	}
}
