using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Protected;
using TaxCalculator.Clients;
using Newtonsoft.Json;
using System.IO;
using TaxCalculator.Entities;

namespace UnitTests
{
	[TestClass]
	public class TaxClientTests
	{
		[TestMethod]
		public void TaxJarClient_GetTaxForOrder_OK()
		{
			var mockMessageHandler = new Mock<HttpMessageHandler>();
			mockMessageHandler.Protected()
				.Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
				.ReturnsAsync(new HttpResponseMessage
				{
					StatusCode = System.Net.HttpStatusCode.OK,
					Content = new StringContent(LoadJson("../../JsonObjects/TaxForOrderResponse.json"))
				});
			TaxJarClient testClient = new TaxJarClient(new HttpClient(mockMessageHandler.Object), "", "http://test.com/");

			TaxForOrderRequest request = new TaxForOrderRequest();
			request = GetTestRequest();

			var result = testClient.GetTaxForOrder(request);
			result.Wait();

			Assert.AreEqual(1.35F, result.Result.tax.amount_to_collect);
			//testClient.GetTaxForOrder();
		}

		[TestMethod]
		public async Task TaxJarClient_GetTaxForOrder_NotFound()
		{
			var mockMessageHandler = new Mock<HttpMessageHandler>();
			mockMessageHandler.Protected()
				.Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
				.ReturnsAsync(new HttpResponseMessage
				{
					StatusCode = System.Net.HttpStatusCode.NotFound,
					Content = new StringContent(LoadJson("../../JsonObjects/ErrorNotFound.json"))
				});
			TaxJarClient testClient = new TaxJarClient(new HttpClient(mockMessageHandler.Object), "", "http://test.com/");

			TaxForOrderRequest request = new TaxForOrderRequest();
			request = GetTestRequest();

			try
			{
				await testClient.GetTaxForOrder(request);
				Assert.Fail();
			}
			catch (Exception ex)
			{
				Assert.IsTrue(true);
			}
			//testClient.GetTaxForOrder();
		}

		[TestMethod]
		public void TaxJarClient_GetTaxRates_OK()
		{
			var mockMessageHandler = new Mock<HttpMessageHandler>();
			mockMessageHandler.Protected()
				.Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
				.ReturnsAsync(new HttpResponseMessage
				{
					StatusCode = System.Net.HttpStatusCode.OK,
					Content = new StringContent(LoadJson("../../JsonObjects/TaxRatesForLocationResponse.json"))
				});
			TaxJarClient testClient = new TaxJarClient(new HttpClient(mockMessageHandler.Object), "", "http://test.com/");

			TaxRatesRequest request = new TaxRatesRequest();
			request = GetTestRatesRequest();

			var result = testClient.GetTaxRatesForLocation(request);
			result.Wait();

			Assert.AreEqual(0.0975F, result.Result.rate.combined_rate);
			//testClient.GetTaxForOrder();
		}

		[TestMethod]
		public async Task TaxJarClient_GetTaxRates_NotAcceptable()
		{
			var mockMessageHandler = new Mock<HttpMessageHandler>();
			mockMessageHandler.Protected()
				.Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
				.ReturnsAsync(new HttpResponseMessage
				{
					StatusCode = System.Net.HttpStatusCode.NotAcceptable,
					Content = new StringContent(LoadJson("../../JsonObjects/ErrorNotAcceptable.json"))
				});
			TaxJarClient testClient = new TaxJarClient(new HttpClient(mockMessageHandler.Object), "", "http://test.com/");

			TaxRatesRequest request = new TaxRatesRequest();
			request = GetTestRatesRequest();
			request.country = null;

			try
			{
				var result = await testClient.GetTaxRatesForLocation(request);
				Assert.Fail();
			}
			catch (Exception ex)
			{
				Assert.IsTrue(true);
			}
		}

		[TestMethod]
		public async Task TaxJarClient_GetTaxRates_NoZip()
		{
			var mockMessageHandler = new Mock<HttpMessageHandler>();
			mockMessageHandler.Protected()
				.Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
				.ReturnsAsync(new HttpResponseMessage
				{
					StatusCode = System.Net.HttpStatusCode.NotFound,
					Content = new StringContent(LoadJson("../../JsonObjects/ErrorNotFound.json"))
				});
			TaxJarClient testClient = new TaxJarClient(new HttpClient(mockMessageHandler.Object), "", "http://test.com/");

			TaxRatesRequest request = new TaxRatesRequest();
			request = GetTestRatesRequest();
			request.zip = null;

			try
			{
				var result = await testClient.GetTaxRatesForLocation(request);
				Assert.Fail();
			}
			catch (Exception ex)
			{
				Assert.IsTrue(true);
			}
		}

		private TaxRatesRequest GetTestRatesRequest()
		{
			return new TaxRatesRequest()
			{
				city = "Santa Montica",
				country = "US",
				state = "CA",
				zip = "90404"
			};
		}


		private TaxForOrderRequest GetTestRequest()
		{
			return new TaxForOrderRequest()
			{
				from_country = "US",
				from_zip = "92093",
				from_state = "CA",
				from_city = "La Jolla",
				from_street = "9500 Gilman Drive",
				to_country = "US",
				to_zip = "90002",
				to_state = "CA",
				to_city = "Los Angeles",
				to_street = "1335 E 103rd St",
				amount = 15,
				shipping = 1.5F
			};
		}



		private string LoadJson(string fileName)
		{
			Directory.GetCurrentDirectory();
			using (StreamReader r = new StreamReader(fileName))
			{
				string json = r.ReadToEnd();
				return json;
			}
		}
	}
}
