using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TaxCalculator.Entities;
using TaxCalculator.Models;

namespace TaxCalculator.Clients
{
	public class TaxJarClient : BaseAPI, ITaxApi
	{
		public TaxJarClient(HttpClient client, string apiKey, string rootUrl) : base(client, apiKey, rootUrl)
		{
			this.client = client;
			this.client.DefaultRequestHeaders.Add("Authorization", "Bearer " + apiKey);
		}

		/// <summary>
		/// Call TaxJar "/taxes" api method to get taxes to be collected for order
		/// </summary>
		/// <param name="order">TaxJar TaxForOrder request object</param>
		/// <returns>TaxJar TaxForOrder response, contains the tax to be collected</returns>
		public async Task<TaxForOrderResponse> GetTaxForOrder(TaxForOrderRequest order)
		{
			StringContent content = new StringContent(JsonConvert.SerializeObject(order), Encoding.UTF8,
				"application/json");
			return await this.Post<TaxForOrderResponse>("taxes", null, content);
		}

		/// <summary>
		/// TaxJar api method for Getting Tax rates for a given location
		/// </summary>
		/// <param name="location">TaxRatesRequest, contains location for tax rates</param>
		/// <returns>List of tax rates for given location broken down by district and total</returns>
		public async Task<TaxRatesResponse> GetTaxRatesForLocation(TaxRatesRequest location)
		{
			if (string.IsNullOrEmpty(location.zip))
			{
				throw new Exception("Zip code needed for request");
			}
			return await this.Get<TaxRatesResponse>("rates/" + location.zip, location);
		}

		/// <summary>
		/// Performs an Http POST request to api defined by this client
		/// </summary>
		/// <typeparam name="T">Type of return object expected from api</typeparam>
		/// <param name="method">Relative uri method name for api found after root url</param>
		/// <param name="values"></param>
		/// <param name="body">Request object to be sent in api call</param>
		/// <returns>Object returned by POST request of type T</returns>
		public async override Task<T> Post<T>(string method, NameValueCollection values, HttpContent body)
		{
			if (body == null)
			{

			}
			var response = this.client.PostAsync(rootUrl + method, body);
			var result = await response.Result.Content.ReadAsStringAsync();
			if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
			{
				return JsonConvert.DeserializeObject<T>(result);
			}
			else
			{
				throw new Exception(string.Format("TaxJar API call failed with status {0}",
					response.Result.StatusCode));
			}
		}


		/// <summary>
		/// Performs HTTP GET request to api defined by this client
		/// </summary>
		/// <typeparam name="T">Type of object expected to be returned by GET request</typeparam>
		/// <param name="method">Relative uri method name for api found after root url</param>
		/// <param name="parameters">Any additional paramaters to be passed into the query string of the request</param>
		/// <returns>Object returned by GET Request of type T</returns>
		public override async Task<T> Get<T>(string method, object parameters)
		{
			var response = this.client.GetAsync(rootUrl + method + ToQueryString(parameters));
			var result = await response;
			if (result.StatusCode == System.Net.HttpStatusCode.OK)
			{
				var returnObj = await result.Content.ReadAsStringAsync();
				return JsonConvert.DeserializeObject<T>(returnObj);
			}
			else
			{
				throw new Exception(string.Format("TaxJar API call failed with status {0}",
					result.StatusCode));
			}
		}

		private static string ToQueryString(object obj)
		{

			var qs = new StringBuilder("?");

			var objType = obj.GetType();

			objType.GetProperties().Where(p => p.GetValue(obj) != null).ToList()
				   .ForEach(p => qs.Append($"{Uri.EscapeDataString(p.Name)}=" +
	   $"{Uri.EscapeDataString(p.GetValue(obj).ToString())}&"));

			return qs.ToString();
		}
	}
}
