using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxCalculator.Calculators;
using TaxCalculator.Clients;
using TaxCalculator.Models;
using TaxCalculator.Parsers;
using TaxCalculator.Entities;
using Moq;

namespace UnitTests
{
	[TestClass]
	public class TaxCalculatorTests
	{
		private ITaxApi apiClient { get; set; }
		private static IParser<Location, TaxRatesRequest> taxRateRequestParser { get; set; }
		private static IParser<TaxRatesResponse, Rates> taxRateResponseParser { get; set; }
		private static IParser<Order, TaxForOrderRequest> taxForOrderRequestParser { get; set; }
		private static IParser<TaxForOrderResponse, TaxReturn> taxForOrderResponseParser { get; set; }
		private static float taxForUS = 8;
		private static float taxForCA = 12;
		private static float taxRateForUS = 0.08F;
		private static float taxRateForCA = 0.12F;

		[ClassInitialize]
		public static void Init(TestContext context)
		{
			var mockTaxRateRequestParser = new Mock<IParser<Location, TaxRatesRequest>>();
			mockTaxRateRequestParser.Setup(x => x.Parse(It.Is<Location>(l => l.Country == "US")))
				.Returns(GetTestTaxRatesRequest());
			mockTaxRateRequestParser.Setup(x => x.Parse(It.Is<Location>(l => l.Country == "CA")))
				.Returns(GetTestTaxRatesRequestCA());
			taxRateRequestParser = mockTaxRateRequestParser.Object;

			var mockTaxRateResponseParser = new Mock<IParser<TaxRatesResponse, Rates>>();
			mockTaxRateResponseParser.Setup(x => x.Parse(It.Is<TaxRatesResponse>(t =>
				t.rate.combined_rate == 0.08F))).Returns(GetTestRates());
			mockTaxRateResponseParser.Setup(x => x.Parse(It.Is<TaxRatesResponse>(t =>
				t.rate.combined_rate == 0.12F))).Returns(GetTestRatesCA());
			taxRateResponseParser = mockTaxRateResponseParser.Object;

			var mockTaxForOrderRequestParser = new Mock<IParser<Order, TaxForOrderRequest>>();
			mockTaxForOrderRequestParser.Setup(x => x.Parse(It.Is<Order>(o => o.CustomerLocation.Country == "US")))
				.Returns(GetTestTaxForOrderRequest());
			mockTaxForOrderRequestParser.Setup(x => x.Parse(It.Is<Order>(o => o.CustomerLocation.Country == "CA")))
				.Returns(GetTestTaxForOrderRequestCA());
			taxForOrderRequestParser = mockTaxForOrderRequestParser.Object;

			var mockTaxForOrderResponseParser = new Mock<IParser<TaxForOrderResponse, TaxReturn>>();
			mockTaxForOrderResponseParser.Setup(x => x.Parse(It.Is<TaxForOrderResponse>(t =>
				t.tax.amount_to_collect == 8))).Returns(GetTestTaxReturn());
			mockTaxForOrderResponseParser.Setup(x => x.Parse(It.Is<TaxForOrderResponse>(t => t.tax.amount_to_collect == 12F)))
				.Returns(GetTestTaxReturnCA());
			taxForOrderResponseParser = mockTaxForOrderResponseParser.Object;
		}

		private static TaxReturn GetTestTaxReturn()
		{
			return new TaxReturn()
			{
				TaxesToCollect = 8
			};
		}

		private static TaxReturn GetTestTaxReturnCA()
		{
			return new TaxReturn()
			{
				TaxesToCollect = 12
			};
		}


		private static TaxForOrderRequest GetTestTaxForOrderRequest()
		{
			return new TaxForOrderRequest()
			{
				amount = 100,
				shipping = 0,
				from_city = "Schenectady",
				from_state = "NY",
				from_zip = "12303",
				from_country = "US",
				to_city = "Schenectady",
				to_state = "NY",
				to_zip = "12303",
				to_country = "US"
			};
		}

		private static TaxForOrderRequest GetTestTaxForOrderRequestCA()
		{
			return new TaxForOrderRequest()
			{
				amount = 100,
				shipping = 0,
				from_city = "Vancouver",
				from_state = "BC",
				from_zip = "12303",
				from_country = "CA",
				to_city = "Vancouver",
				to_state = "BC",
				to_zip = "12303",
				to_country = "CA"
			};
		}

		private static Rates GetTestRates()
		{
			return new Rates()
			{
				CombinedTax = 0.08F
			};
		}

		private static Rates GetTestRatesCA()
		{
			return new Rates()
			{
				CombinedTax = 0.12F
			};
		}

		private static TaxRatesRequest GetTestTaxRatesRequest()
		{
			return new TaxRatesRequest()
			{
				city = "Schenectady",
				state = "NY",
				zip = "12303",
				country = "US"
			};
		}

		private static TaxRatesRequest GetTestTaxRatesRequestCA()
		{
			return new TaxRatesRequest()
			{
				city = "Vancouver",
				state = "BC",
				zip = "V5K0A1",
				country = "CA"
			};
		}

		[TestMethod]
		public void TaxJarCalculator_GetTaxRates_US_08()
		{
			TestClient client = new TestClient();
			TaxJarCalculator calculator = new TaxJarCalculator(client, taxForOrderRequestParser,
				taxForOrderResponseParser, taxRateRequestParser, taxRateResponseParser);
			Location location = CreateTestLocationUS();

			var rates = calculator.GetTaxRates(location);
			Assert.AreEqual<float>(taxRateForUS, rates.CombinedTax);
		}

		[TestMethod]
		public void TaxJarCalculator_GetTaxRates_CA_12()
		{
			TestClient client = new TestClient();
			TaxJarCalculator calculator = new TaxJarCalculator(client, taxForOrderRequestParser,
				taxForOrderResponseParser, taxRateRequestParser, taxRateResponseParser);
			Location location = CreateTestLocationCA();

			var rates = calculator.GetTaxRates(location);
			Assert.AreEqual<float>(taxRateForCA, rates.CombinedTax);
		}

		[TestMethod]
		public void TaxJarCalculator_GetTaxForOrder_US_8()
		{
			TestClient client = new TestClient();
			TaxJarCalculator calculator = new TaxJarCalculator(client, taxForOrderRequestParser,
						taxForOrderResponseParser, taxRateRequestParser, taxRateResponseParser);
			Order testOrder = CreateTestOrderUS();
			var order = calculator.GetTaxForOrder(testOrder);
			Assert.AreEqual<float>(taxForUS, order.TaxesToCollect);
		}

		[TestMethod]
		public void TaxJarCalculator_GetTaxForOrder_CA_12()
		{
			TestClient client = new TestClient();
			TaxJarCalculator calculator = new TaxJarCalculator(client, taxForOrderRequestParser,
						taxForOrderResponseParser, taxRateRequestParser, taxRateResponseParser);
			Order testOrder = CreateTestOrderCA();
			var order = calculator.GetTaxForOrder(testOrder);
			Assert.AreEqual<float>(taxForCA, order.TaxesToCollect);
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
	}
}
