using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Collections.Specialized;
using System.Threading.Tasks;

namespace TaxCalculator.Clients
{
	public abstract class BaseAPI
	{
		protected string apiKey { get; set; }

		protected string rootUrl { get; set; }

		protected HttpClient client { get; set; }

		public BaseAPI(HttpClient client, string apiKey, string rootUrl)
		{
			this.apiKey = apiKey;
			this.rootUrl = rootUrl;
			this.client = client;
		}

		public abstract Task<T> Post<T>(string method, NameValueCollection values, HttpContent body);
		public abstract Task<T> Get<T>(string method, object parameters);
	}
}
