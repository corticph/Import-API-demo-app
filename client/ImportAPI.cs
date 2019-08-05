using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ImportAPIClient.Client.Entity;

namespace ImportAPIClient.Client
{
    public class ImportAPIClient
    {
        private static readonly HttpClient client = new HttpClient();

        private readonly string Host;

        private readonly int Port;

        private readonly string Token;

        private readonly bool IsHttps = true;

        private readonly string Version = "v1.0";

        public ImportAPIClient(string host, int port, string token)
        {
            this.Host = host;
            this.Port = port;
            this.Token = token;
        }

        public ImportAPIClient(string host, int port, string token, bool isHttps, string version) : this(host, port, token)
        {
            this.IsHttps = isHttps;
            this.Version = version;
        }

        // Summary:
        //     Send a GET request to `triage-sessions` resource endpoint.
        //
        // Parameters:
        //   externalId:
        //     The external ID by which triage sessions will be queried against
        //
        // Returns:
        //     The task object with List<TriageSession> representing the asynchronous operation.
        //
        // Exceptions:
        //   T:Client.ClientException:
        //     The request failed with unsuccessful status code indicating client or server error
        //
        //   T:System.Net.Http.HttpRequestException:
        //     The request failed due to an underlying issue such as network connectivity, DNS
        //     failure, server certificate validation or timeout.

        public async Task<List<TriageSession>> FindTriageSessionsByExternalIDAsync(string externalId)
        {
            string query;
            using (var content = new FormUrlEncodedContent(new Dictionary<string, string>()
            {
                { "external_id", externalId},
            }))
            {
                query = content.ReadAsStringAsync().Result;
            }
            using (var httpRequestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = BuildUri("triage-sessions", query),
                Headers = {
                    { "x-api-key", Token }
                },
            })
            {
                HttpResponseMessage response = await client.SendAsync(httpRequestMessage);
                if (!response.IsSuccessStatusCode)
                {
                    throw new ClientException(response.StatusCode, await response.Content.ReadAsStringAsync());
                }

                return await response.Content.ReadAsAsync<List<TriageSession>>();
            }
        }

        // Summary:
        //     Send a POST request to `imports/custom-events` resource endpoint.
        //
        // Parameters:
        //   events:
        //     A list of events to be serialized into JSON and sent
        //
        // Returns:
        //     The task object with List<CustomEvent> representing the asynchronous operation.
        //
        // Exceptions:
        //   T:Client.ClientException:
        //     The request failed with unsuccessful status code indicating client or server error
        //
        //   T:System.Net.Http.HttpRequestException:
        //     The request failed due to an underlying issue such as network connectivity, DNS
        //     failure, server certificate validation or timeout.
        public async Task<List<CustomEvent>> ImportCustomEventsAsync(ICollection<CustomEvent> events)
        {
            using (var httpRequestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = BuildUri("imports/custom-events"),
                Headers = {
                    { "x-api-key", Token }
                },
                Content = new StringContent(JsonConvert.SerializeObject(events), Encoding.UTF8, "application/json"),
            })
            {
                HttpResponseMessage response = await client.SendAsync(httpRequestMessage);
                if (!response.IsSuccessStatusCode)
                {
                    throw new ClientException(response.StatusCode, await response.Content.ReadAsStringAsync());
                }

                return await response.Content.ReadAsAsync<List<CustomEvent>>();
            }
        }

        // Summary:
        //     Send a POST request to `imports/case-properties` resource endpoint.
        //
        // Parameters:
        //   events:
        //     A list of case properties to be serialized into JSON and sent
        //
        // Returns:
        //     The task object with List<CaseProperties> representing the asynchronous operation.
        //
        // Exceptions:
        //   T:Client.ClientException:
        //     The request failed with unsuccessful status code indicating client or server error
        //
        //   T:System.Net.Http.HttpRequestException:
        //     The request failed due to an underlying issue such as network connectivity, DNS
        //     failure, server certificate validation or timeout.
        public async Task<List<CaseProperties>> ImportCasePropertiesAsync(ICollection<CaseProperties> properties)
        {
            using (var httpRequestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = BuildUri("imports/case-properties"),
                Headers = {
                    { "x-api-key", Token }
                },
                Content = new StringContent(JsonConvert.SerializeObject(properties), Encoding.UTF8, "application/json"),
            })
            {
                HttpResponseMessage response = await client.SendAsync(httpRequestMessage);
                if (!response.IsSuccessStatusCode)
                {
                    throw new ClientException(response.StatusCode, await response.Content.ReadAsStringAsync());
                }

                return await response.Content.ReadAsAsync<List<CaseProperties>>();
            }
        }

        private Uri BuildUri(string path, String query)
        {
            return new UriBuilder
            {
                Scheme = IsHttps ? "https" : "http",
                Host = Host,
                Port = Port,
                Path = String.Format("/api/{0}/{1}/", Version, path),
                Query = query,
            }.Uri;
        }

        private Uri BuildUri(string path)
        {
            return BuildUri(path, null);
        }
    }
}