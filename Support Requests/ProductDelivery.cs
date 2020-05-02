using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading;

namespace Delevery_Service
{
 //Generates random details of delivery service and pushes them to a Power BI streaming dataset.


    class ProductDelivery
    {
        // Use C# Random class to generate the random product prices
        static Random random = new Random();

        // Use class-level HttpClient
        static HttpClient client = new HttpClient();
        static List<JamDeliveryServices> jamDelivery;

        static void Main()
        {
            const int duration = 60; // Length of time in minutes to push data
            const int pauseInterval = 2; // Frequency in seconds that data will be pushed
            const string timeFormat = "yyyy-MM-ddTHH:mm:ss.fffZ"; // Time format required by Power BI

            jamDelivery = new List<JamDeliveryServices>();
            jamDelivery.Add(new JamDeliveryServices { productID = 1, productName = "", powerBiPostUrl = "https://api.powerbi.com/beta/94f2a593-88af-4f56-8f14-74dbdd29971d/datasets/ee0b99a0-08bd-4df3-bd75-890ad721be68/rows?key=LdPMqrJmlY65wIn7ihMqKrahqe8pIJKcOgfwy%2FuwjtXYt12Nmr4w6ueKpKj1bifZGbdTj6VinxMdpK%2FWa%2BrWFQ%3D%3D" });

            GeneratePowerBiData(duration, pauseInterval, timeFormat);
        }

        public static void GeneratePowerBiData(int duration, int pauseInterval, string timeFormat)
        {
            DateTime startTime = GetDateTimeUtc();
            DateTime currentTime;
            int count = 0;

            do
            {
                foreach (var delever in jamDelivery)
                {
                    count++;

                    PushDataToPowerBi(delever,count,timeFormat);


                }
                Thread.Sleep(pauseInterval * 1000); // Pause for n seconds.
                currentTime = GetDateTimeUtc();
            } while ((currentTime - startTime).TotalMinutes <= duration);
        }

        public static void PushDataToPowerBi(JamDeliveryServices jamDelivery, int count, string timeFormat)
        {
            DeliveryDetails deleveryDetails = new DeliveryDetails();
            deleveryDetails = FillDeleveryDetails(count, jamDelivery , timeFormat);
            var jsonString = JsonConvert.SerializeObject(deleveryDetails);
            var postToPowerBi = HttpPostAsync(jamDelivery.powerBiPostUrl, "[" + jsonString + "]"); // Add brackets for Power BI
            Console.WriteLine(jsonString);
        }

        public static DeliveryDetails FillDeleveryDetails(int count, JamDeliveryServices jamDelevery, string timeFormat)
        {
            DeliveryDetails deleveryDetails = new DeliveryDetails();

            if (count < 50)
            {
                deleveryDetails.productID = jamDelevery.productID;
                deleveryDetails.productName = "SAMSUNG NOTE 10";
                deleveryDetails.productPrice = GenerateRandom(10);
                deleveryDetails.region = "North America";
                deleveryDetails.city = "Halifax,NS";
                deleveryDetails.country = "Canada";
                deleveryDetails.deleveryDateTime = GetDateTimeUtc().ToString(timeFormat);


            }
            else if (count > 50 || count < 100)
            {
                deleveryDetails.productID = jamDelevery.productID;
                deleveryDetails.productName = "SONEY TV 75 INCH";
                deleveryDetails.productPrice = GenerateRandom(10);
                deleveryDetails.region = "North America";
                deleveryDetails.city = "Vancouver , BC";
                deleveryDetails.country = "Canada";
                deleveryDetails.deleveryDateTime = GetDateTimeUtc().ToString(timeFormat);
            }

            else if (count > 100 || count < 200)
            {
                deleveryDetails.productID = jamDelevery.productID;
                deleveryDetails.productName = "IPHONE 11 MAX";
                deleveryDetails.productPrice = GenerateRandom(10);
                deleveryDetails.region = "America";
                deleveryDetails.city = "New York";
                deleveryDetails.country = "USA";
                deleveryDetails.deleveryDateTime = GetDateTimeUtc().ToString(timeFormat);
            }

            else if (count > 200 || count < 500)
            {
                deleveryDetails.productID = jamDelevery.productID;
                deleveryDetails.productName = "MICROSOFT SURFACE PRO";
                deleveryDetails.productPrice = GenerateRandom(10);
                deleveryDetails.region = "America";
                deleveryDetails.city = "Houston";
                deleveryDetails.country = "USA";
                deleveryDetails.deleveryDateTime = GetDateTimeUtc().ToString(timeFormat);
            }

            else if (count > 500 || count < 2500)
            {
                deleveryDetails.productID = jamDelevery.productID;
                deleveryDetails.productName = "CANON EOS R";
                deleveryDetails.productPrice = GenerateRandom(10);
                deleveryDetails.region = "America";
                deleveryDetails.city = "Los Angeles";
                deleveryDetails.country = "USA";
                deleveryDetails.deleveryDateTime = GetDateTimeUtc().ToString(timeFormat);
            }

            else if (count > 2500 || count < 3500)
            {
                deleveryDetails.productID = jamDelevery.productID;
                deleveryDetails.productName = "CANON EOS R";
                deleveryDetails.productPrice = GenerateRandom(10);
                deleveryDetails.region = "North America";
                deleveryDetails.city = "Montreal";
                deleveryDetails.country = "Canada";
                deleveryDetails.deleveryDateTime = GetDateTimeUtc().ToString(timeFormat);
            }

            else if (count > 3500 || count < 10000)
            {
                deleveryDetails.productID = jamDelevery.productID;
                deleveryDetails.productName = "LAPTOPS";
                deleveryDetails.productPrice = GenerateRandom(10);
                deleveryDetails.region = "North America";
                deleveryDetails.city = "Toronto";
                deleveryDetails.country = "Canada";
                deleveryDetails.deleveryDateTime = GetDateTimeUtc().ToString(timeFormat);
            }

            else if (count > 10000 || count < 10500)
            {
                deleveryDetails.productID = jamDelevery.productID;
                deleveryDetails.productName = "Stationary Items";
                deleveryDetails.productPrice = GenerateRandom(10);
                deleveryDetails.region = "America";
                deleveryDetails.city = "Maxico City";
                deleveryDetails.country = "Maxico";
                deleveryDetails.deleveryDateTime = GetDateTimeUtc().ToString(timeFormat);
            }
            else
            {
                deleveryDetails.productID = jamDelevery.productID;
                deleveryDetails.productName = "Grocery Items";
                deleveryDetails.productPrice = GenerateRandom(10);
                deleveryDetails.region = "America";
                deleveryDetails.city = "Houston";
                deleveryDetails.country = "USA";
                deleveryDetails.deleveryDateTime = GetDateTimeUtc().ToString(timeFormat);
            }

            return deleveryDetails;
        }

        public class DeliveryDetails
        {
            public int productID { get; set; }
            public string productName { get; set; }
            public int productPrice { get; set; }
            public string deleveryDateTime { get; set; }
            public string region { get; set; }
            public string city { get; set; }
            public string country { get; set; }
        }

        public class JamDeliveryServices
        {
            public int productID { get; set; }
            public string productName { get; set; }
            public string powerBiPostUrl { get; set; }
        }

        static int GenerateRandom(int max)
        {
            int n = random.Next(max);
            return n;
        }

        static async Task<HttpResponseMessage> HttpPostAsync(string url, string data)
        {
            // Construct an HttpContent object from StringContent
            HttpContent content = new StringContent(data);
            HttpResponseMessage response = await client.PostAsync(url, content);
            response.EnsureSuccessStatusCode();
            return response;
        }

        static DateTime GetDateTimeUtc()
        {
            return DateTime.UtcNow;
        }
    }
}
