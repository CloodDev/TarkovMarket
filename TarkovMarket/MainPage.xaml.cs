using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Xamarin.Forms;
using System.Diagnostics;
using Newtonsoft.Json;

namespace TarkovMarket
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            GetDataFromWebApi("PACA");
        }

        private async Task<string> GetDataFromWebApi(string item)
        {
            using (HttpClient client = new HttpClient())
            {
                var query = new
                {
                    query = @"
                        query ($item: String!) {
                            item(name: $item) {
                                name
                                price
                                traderPrices {
                                    trader
                                    price
                                }
                            }
                        }",
                    variables = new { item }
                };

                var json = Newtonsoft.Json.JsonConvert.SerializeObject(query);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync("https://api.tarkov.dev/graphql", content);
                var responseContent = await response.Content.ReadAsStringAsync();

                // Process the responseContent here
                Debug.WriteLine(responseContent);
                return responseContent;
            }
        }

    }
}
