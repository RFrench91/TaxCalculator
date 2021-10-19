using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TaxCalculator.Parsers;
using TaxCalculator.Models;
using TaxCalculator.Entities;

namespace UnitTests
{
	[TestClass]
	public class ParserTests
	{
		[TestMethod]
		public void GetTaxRatesResponseParser_Parse_CombinedTax()
		{
			GetTaxRatesResponseParser testParser = new GetTaxRatesResponseParser();
			var testResponse = GenerateTestTaxRatesResponse();
			var result = testParser.Parse(testResponse);
			Assert.AreEqual(0.08F, result.CombinedTax);
		}

		private TaxRatesResponse GenerateTestTaxRatesResponse()
		{
			return new TaxRatesResponse()
			{
				rate = new rates()
				{
					combined_rate = 0.08F
				}
			};
		}

		[TestMethod]
		public void GetTaxRatesRequestParser_Parse_Country()
		{
			GetTaxRatesRequestParser testParser = new GetTaxRatesRequestParser();
			var testLocation = GenerateTestLocation();
			var location = testParser.Parse(testLocation);
			Assert.AreEqual("US", location.country);
		}

		[TestMethod]
		public void GetTaxRatesRequestParser_Parse_City()
		{
			GetTaxRatesRequestParser testParser = new GetTaxRatesRequestParser();
			var testLocation = GenerateTestLocation();
			var location = testParser.Parse(testLocation);
			Assert.AreEqual("Schenectady", location.city);
		}

		[TestMethod]
		public void GetTaxRatesRequestParser_Parse_State()
		{
			GetTaxRatesRequestParser testParser = new GetTaxRatesRequestParser();
			var testLocation = GenerateTestLocation();
			var location = testParser.Parse(testLocation);
			Assert.AreEqual("NY", location.state);
		}

		[TestMethod]
		public void GetTaxRatesRequestParser_Parse_Zip()
		{
			GetTaxRatesRequestParser testParser = new GetTaxRatesRequestParser();
			var testLocation = GenerateTestLocation();
			var location = testParser.Parse(testLocation);
			Assert.AreEqual("12303", location.zip);
		}

		private Location GenerateTestLocation()
		{
			return new Location()
			{
				City = "Schenectady",
				Country = "US",
				ZipCode = "12303",
				State = "NY"
			};
		}

		private Location GenerateTestLocationCA()
		{
			return new Location()
			{
				Country = "CA",
				ZipCode = "V5K0A1",
				State = "BC",
				City = "Vancouver"
			};
		}

		[TestMethod]
		public void GetTaxForOrderRequestParser_Parse_Amount()
		{
			GetTaxForOrderRequestParser testParser = new GetTaxForOrderRequestParser();
			Order testOrder = GenerateTestOrder();
			var result = testParser.Parse(testOrder);
			Assert.AreEqual(100, result.amount);
		}

		[TestMethod]
		public void GetTaxForOrderRequestParser_Parse_Shipping()
		{
			GetTaxForOrderRequestParser testParser = new GetTaxForOrderRequestParser();
			Order testOrder = GenerateTestOrder();
			var result = testParser.Parse(testOrder);
			Assert.AreEqual(10, result.shipping);
		}

		[TestMethod]
		public void GetTaxForOrderRequestParser_Parse_FromCity()
		{
			GetTaxForOrderRequestParser testParser = new GetTaxForOrderRequestParser();
			Order testOrder = GenerateTestOrder();
			var result = testParser.Parse(testOrder);
			Assert.AreEqual("Schenectady", result.from_city);
		}

		[TestMethod]
		public void GetTaxForOrderRequestParser_Parse_FromCountry()
		{
			GetTaxForOrderRequestParser testParser = new GetTaxForOrderRequestParser();
			Order testOrder = GenerateTestOrder();
			var result = testParser.Parse(testOrder);
			Assert.AreEqual("US", result.from_country);
		}

		[TestMethod]
		public void GetTaxForOrderRequestParser_Parse_FromState()
		{
			GetTaxForOrderRequestParser testParser = new GetTaxForOrderRequestParser();
			Order testOrder = GenerateTestOrder();
			var result = testParser.Parse(testOrder);
			Assert.AreEqual("NY", result.from_state);
		}


		[TestMethod]
		public void GetTaxForOrderRequestParser_Parse_FromZip()
		{
			GetTaxForOrderRequestParser testParser = new GetTaxForOrderRequestParser();
			Order testOrder = GenerateTestOrder();
			var result = testParser.Parse(testOrder);
			Assert.AreEqual("12303", result.from_zip);
		}

		[TestMethod]
		public void GetTaxForOrderRequestParser_Parse_ToCity()
		{
			GetTaxForOrderRequestParser testParser = new GetTaxForOrderRequestParser();
			Order testOrder = GenerateTestOrder();
			var result = testParser.Parse(testOrder);
			Assert.AreEqual("Vancouver", result.to_city);
		}

		[TestMethod]
		public void GetTaxForOrderRequestParser_Parse_ToCountry()
		{
			GetTaxForOrderRequestParser testParser = new GetTaxForOrderRequestParser();
			Order testOrder = GenerateTestOrder();
			var result = testParser.Parse(testOrder);
			Assert.AreEqual("CA", result.to_country);
		}

		[TestMethod]
		public void GetTaxForOrderRequestParser_Parse_ToState()
		{
			GetTaxForOrderRequestParser testParser = new GetTaxForOrderRequestParser();
			Order testOrder = GenerateTestOrder();
			var result = testParser.Parse(testOrder);
			Assert.AreEqual("BC", result.to_state);
		}


		[TestMethod]
		public void GetTaxForOrderRequestParser_Parse_ToZip()
		{
			GetTaxForOrderRequestParser testParser = new GetTaxForOrderRequestParser();
			Order testOrder = GenerateTestOrder();
			var result = testParser.Parse(testOrder);
			Assert.AreEqual("V5K0A1", result.to_zip);
		}

		private Order GenerateTestOrder()
		{
			return new Order()
			{
				CustomerLocation = GenerateTestLocationCA(),
				SellerLocation = GenerateTestLocation(),
				Shipping = 10,
				OrderAmount = 100
			};
		}

		[TestMethod]
		public void GetTaxForOrderResponseParser_Parse_TaxesToCollect()
		{
			GetTaxForOrderResponseParser testParser = new GetTaxForOrderResponseParser();
			TaxForOrderResponse testResponse = GenerateTestTaxForOrderResponse();
			var result = testParser.Parse(testResponse);
			Assert.AreEqual(10, result.TaxesToCollect);
		}

		private TaxForOrderResponse GenerateTestTaxForOrderResponse()
		{
			return new TaxForOrderResponse()
			{
				tax = new OrderResponse()
				{
					amount_to_collect = 10
				}
			};
		}


	}
}
