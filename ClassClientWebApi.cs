using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
//download at nuget package 
using System.Text.Json;
using System.Threading.Tasks;
using webapplication_console_web_proj1;
using webapplication_console_web_proj1.Models;


namespace console_web_proj1
{
    public class ClassClientWebApi
    {
        private static readonly HttpClient httpClientEdit1 = new HttpClient();
        public async Task FnGetAllShoeItemsEdit1()
        {
            string url = "https://localhost:44390/api/Application/getAllListShoeInventory";
            try
            {
                HttpResponseMessage response = await httpClientEdit1.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    string jsonResult = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("\nSuccess! Here is the data:");
                    Console.WriteLine(jsonResult);
                }
                else
                {
                    Console.WriteLine($"Error: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Something went wrong: {ex.Message}");
            }
        }

        public async Task FnGetAllShoeItemsEdit2()
        {
            // Define the API endpoint
            
            string apiUrl = "https://localhost:44390/api/Application/getAllListShoeInventory";

            // Create HttpClient instance
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    // Send GET request
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    // Ensure success status code
                    response.EnsureSuccessStatusCode();

                    // Read response as string
                    string responseBody = await response.Content.ReadAsStringAsync();

                    // Print result
                    Console.WriteLine("API Response:");
                    Console.WriteLine(responseBody);
                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine($"Request error: {e.Message}");
                }
            }
        }

        //get all shoe list
        public async Task FnGetAllShoeItems()
        {
            //get all shoe items
            string apiUrl = "https://localhost:44390/api/Application/getAllListShoeInventory";
            // Create HttpClient instance
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    // Send GET request
                    HttpResponseMessage response = await client.GetAsync(apiUrl);
                    // Ensure success status code
                    response.EnsureSuccessStatusCode();
                    // Read response as string
                    string responseBody = await response.Content.ReadAsStringAsync();
                    System.Console.WriteLine("");
                    List<ShoeInventoryTableModel> listShoeInventoryTableModel = 
                        JsonSerializer.Deserialize<List<ShoeInventoryTableModel>>(responseBody);
                    foreach (ShoeInventoryTableModel shoeInventoryTableModel in listShoeInventoryTableModel)
                    {
                        string shoeId = shoeInventoryTableModel.id.ToString();
                        string shoeName = shoeInventoryTableModel.shoeName;
                        string shoeDescription = shoeInventoryTableModel.shoeDescription;
                        string shoePrice = shoeInventoryTableModel.shoePrice;
                        string shoeSize = shoeInventoryTableModel.shoeSize.ToString();
                        string shoeQuantity = shoeInventoryTableModel.quantity.ToString();
                        System.Console.WriteLine("Id: " + shoeId);
                        System.Console.WriteLine("Shoe Name: " + shoeName);
                        System.Console.WriteLine("Shoe Description: "+ shoeDescription);
                        System.Console.WriteLine("Shoe Price: " + shoePrice);
                        System.Console.WriteLine("Shoe Quantity: " + shoeQuantity);
                        System.Console.WriteLine();
                    }
                    
                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine($"Request error: {e.Message}");
                }
            }
        }

        //get shoe item by id
        public async Task FnGetShoeInventoryItemId(int shoeId)
        {
            string url = "https://localhost:44390/api/Application/getShoeInventoryItemId";
            using (HttpClient httpClient = new HttpClient())
            {
                try
                {
                    url = url + "?id=" + shoeId.ToString();
                    // Send GET request
                    HttpResponseMessage httpResponseMessage = await httpClient.GetAsync(url);
                    //not found status (404)
                    if (httpResponseMessage.StatusCode == HttpStatusCode.NotFound)
                    {
                        System.Console.WriteLine("shoe item with id " +shoeId.ToString() + " not found.");
                    }
                    //ok status (200)
                    if (httpResponseMessage.StatusCode == HttpStatusCode.OK)
                    {
                        string httpResponseMessageContent = await httpResponseMessage.Content.ReadAsStringAsync();
                        ShoeInventoryTableModel shoeInventoryTableModel = JsonSerializer.Deserialize<ShoeInventoryTableModel>(httpResponseMessageContent);
                        string shoeId2 = shoeInventoryTableModel.id.ToString();
                        string shoeName = shoeInventoryTableModel.shoeName;
                        string shoeDescription = shoeInventoryTableModel.shoeDescription;
                        string shoePrice = shoeInventoryTableModel.shoePrice;
                        string shoeSize = shoeInventoryTableModel.shoeSize.ToString();
                        string shoeQuantity = shoeInventoryTableModel.quantity.ToString();
                        System.Console.WriteLine("Id: " + shoeId2);
                        System.Console.WriteLine("Shoe Name: " + shoeName);
                        System.Console.WriteLine("Shoe Description: " + shoeDescription);
                        System.Console.WriteLine("Shoe Price: " + shoePrice);
                        System.Console.WriteLine("Shoe Quantity: " + shoeQuantity);
                        System.Console.WriteLine();
                    }
                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine($"Request error: {e.Message}");
                }
            }
        }

        //add new shoe item
        public async Task FnAddNewShoeInventoryItem(PostShoeInventoryTableModel postShoeInventoryTableModel)
        {
            string url = "https://localhost:44390/api/Application/addNewShoeInventoryItem2";
            // Serialize to JSON
            string jsonContent = JsonSerializer.Serialize(postShoeInventoryTableModel);
            // Wrap JSON in HttpContent
            HttpContent httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            using (HttpClient httpClient = new HttpClient())
            {
                try
                {
                    // Send POST request
                    HttpResponseMessage httpResponseMessage = await httpClient.PostAsync(url, httpContent);
                    //bad request 
                    if (httpResponseMessage.StatusCode == HttpStatusCode.BadRequest)
                    {
                        string httpResponseMessageContent = await httpResponseMessage.Content.ReadAsStringAsync();
                        System.Console.WriteLine(httpResponseMessageContent);
                    }
                    if (httpResponseMessage.StatusCode == HttpStatusCode.OK)
                    {
                        System.Console.WriteLine("new shoe item added.");
                    }
                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine($"Request error: {e.Message}");
                }
            }
        }

        //edit a shoe item by id
        public async Task FnUpdateShoeInventoryItemDetail(ShoeInventoryTableModel shoeInventoryTableModel)
        {
            string url = "https://localhost:44390/api/Application/updateShoeInventoryItemDetail";
            // Serialize to JSON
            string jsonContent = JsonSerializer.Serialize(shoeInventoryTableModel);
            // Wrap JSON in HttpContent
            HttpContent httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            using (HttpClient httpClient = new HttpClient())
            {
                try
                {
                    HttpResponseMessage httpResponseMessage = await httpClient.PutAsync(url, httpContent);
                    //bad request (400)
                    if (httpResponseMessage.StatusCode == HttpStatusCode.BadRequest)
                    {
                        string httpResponseMessageContent = await httpResponseMessage.Content.ReadAsStringAsync();
                        System.Console.WriteLine(httpResponseMessageContent);
                    }
                    //not found (404)
                    if (httpResponseMessage.StatusCode == HttpStatusCode.NotFound)
                    {
                        System.Console.WriteLine("shoe item with entered id not found.");
                    }
                    //ok (200)
                    if (httpResponseMessage.StatusCode == HttpStatusCode.OK)
                    {
                        System.Console.WriteLine("shoe item details edit successfully.");
                    }
                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine($"Request error: {e.Message}");
                }
            }
        }

        //delete a shoe item by id
        public async Task FnDeleteShoeInventoryItemId(int shoeIdDelete)
        {
            string url = "https://localhost:44390/api/Application/deleteShoeInventoryItemId";
            url = url + "?shoeId=" + shoeIdDelete.ToString();
            //url = url + "?id=" + shoeId.ToString();
            using (HttpClient httpClient = new HttpClient())
            {
                try
                {
                    HttpResponseMessage httpResponseMessage = await httpClient.DeleteAsync(url);
                    //not found (404)
                    if (httpResponseMessage.StatusCode == HttpStatusCode.NotFound)
                    {
                        System.Console.WriteLine("shoe item with entered id not found.");
                    }
                    //ok (200)
                    if (httpResponseMessage.StatusCode == HttpStatusCode.OK)
                    {
                        System.Console.WriteLine("shoe item deleted successfully.");
                    }
                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine($"Request error: {e.Message}");
                }
            }
        }


    }//end-class
}//end-namespace
