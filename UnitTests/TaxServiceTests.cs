using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TaxCalculator.Services;
using TaxCalculator.Calculators;
using TaxCalculator.Models;
using TaxCalculator.Clients;
using Moq;
using TaxCalculator.Entities;
using System.Threading.Tasks;
using TaxCalculator.Validator;

namespace UnitTests
{
	[TestClass]
	public class TaxServiceTests
	{
		private static IInputValidator<Location> LocationValidator { get; set; }
		private static IInputValidator<Order> OrderValidator { get; set; }
		private static float USTaxRate = 0.08F;
		private static float CATaxRate = 0.12F;
		private static float USTaxToCollect = 8F;
		private static float CATaxToCollect = 12F;

		[ClassInitialize]
		public static void Init(TestContext context)
		{
			var mockLocationValidator = new Mock<IInputValidator<Location>>();
			mockLocationValidator.Setup(x => x.Validate(It.IsAny<Location>()))
				.Returns(new ValidationResult() { Success = true });
			LocationValidator = mockLocationValidator.Object;

			var mockOrderValidator = new Mock<IInputValidator<Order>>();
			mockOrderValidator.Setup(x => x.Validate(It.IsAny<Order>()))
				.Returns(new ValidationResult() { Success = true });
			OrderValidator = mockOrderValidator.Object;
		}

		[TestMethod]
		public void TaxService_GetTaxForOrder_US_8()
		{
			TestCalculator calculator = new TestCalculator();
			TaxService service = new TaxService(calculator, OrderValidator, LocationValidator);
			Order testOrder = CreateTestOrderUS();

			var result = service.GetTaxForOrder(testOrder);
			Assert.AreEqual<float>(USTaxToCollect, result.TaxesToCollect);
		}

		[TestMethod]
		public void TaxService_GetTaxForOrder_CA_12()
		{
			TestCalculator calculator = new TestCalculator();
			TaxService service = new TaxService(calculator, OrderValidator, LocationValidator);
			Order testOrder = CreateTestOrderCA();

			var result = service.GetTaxForOrder(testOrder);
			Assert.AreEqual<float>(CATaxToCollect, result.TaxesToCollect);
		}

		[TestMethod]
		public void TaxService_GetTaxRates_US_08()
		{
			TestCalculator calculator = new TestCalculator();
			TaxService service = new TaxService(calculator, OrderValidator, LocationValidator);
			Location location = CreateTestLocationUS();

			var rate = service.GetTaxRates(location);
			Assert.AreEqual<float>(USTaxRate, rate.CombinedTax);
		}

		[TestMethod]
		public void TaxService_GetTaxRates_CA_12()
		{
			TestCalculator calculator = new TestCalculator();
			TaxService service = new TaxService(calculator, OrderValidator, LocationValidator);
			Location location = CreateTestLocationCA();

			var rate = service.GetTaxRates(location);
			Assert.AreEqual<float>(CATaxRate, rate.CombinedTax);
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
