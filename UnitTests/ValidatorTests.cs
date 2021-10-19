using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TaxCalculator.Validator;
using TaxCalculator.Models;

namespace UnitTests
{
	[TestClass]
	public class ValidatorTests
	{
		[TestMethod]
		public void GetTaxForOrderValidator_Validate_True()
		{
			GetTaxForOrderValidator testValidator = new GetTaxForOrderValidator();
			Order testOrder = GenerateTestOrder();
			ValidationResult result = testValidator.Validate(testOrder);
			Assert.AreEqual(true, result.Success);
		}

		[TestMethod]
		public void GetTaxForOrderValidator_Validate_NoCustomerLocation()
		{
			GetTaxForOrderValidator testValidator = new GetTaxForOrderValidator();
			Order testOrder = GenerateTestOrder();
			testOrder.CustomerLocation = null;
			ValidationResult result = testValidator.Validate(testOrder);

			Assert.AreEqual(false, result.Success);
			Assert.AreEqual("Customer location required to get tax amount", result.Message);
		}

		[TestMethod]
		public void GetTaxForOrderValidator_Validate_NoSellerLocation()
		{
			GetTaxForOrderValidator testValidator = new GetTaxForOrderValidator();
			Order testOrder = GenerateTestOrder();
			testOrder.SellerLocation = null;
			ValidationResult result = testValidator.Validate(testOrder);

			Assert.AreEqual(false, result.Success);
			Assert.AreEqual("Seller location required to get tax amount", result.Message);
		}

		[TestMethod]
		public void GetTaxForOrderValidator_Validate_NoOrder()
		{
			GetTaxForOrderValidator testValidator = new GetTaxForOrderValidator();
			Order testOrder = null;

			ValidationResult result = testValidator.Validate(testOrder);

			Assert.AreEqual(false, result.Success);
			Assert.AreEqual("Order is missing", result.Message);
		}

		[TestMethod]
		public void GetTaxRatesValidator_Validate_True()
		{
			GetTaxRatesValidator testValidator = new GetTaxRatesValidator();
			Location testLocation = CreateTestLocationUS();

			ValidationResult result = testValidator.Validate(testLocation);

			Assert.AreEqual(true, result.Success);
		}

		[TestMethod]
		public void GetTaxRatesValidator_Validate_NoLocation()
		{
			GetTaxRatesValidator testValidator = new GetTaxRatesValidator();
			Location testLocation = null;

			ValidationResult result = testValidator.Validate(testLocation);

			Assert.AreEqual(false, result.Success);
			Assert.AreEqual("Location not provided", result.Message);
		}

		[TestMethod]
		public void GetTaxRatesValidator_Validate_NoCountry()
		{
			GetTaxRatesValidator testValidator = new GetTaxRatesValidator();
			Location testLocation = CreateTestLocationUS();
			testLocation.Country = null;

			ValidationResult result = testValidator.Validate(testLocation);

			Assert.AreEqual(false, result.Success);
			Assert.AreEqual("Country required for tax rates", result.Message);
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

		private Order GenerateTestOrder()
		{
			return new Order()
			{
				CustomerLocation = CreateTestLocationCA(),
				SellerLocation = CreateTestLocationUS(),
				Shipping = 10,
				OrderAmount = 100
			};
		}
	}
}
