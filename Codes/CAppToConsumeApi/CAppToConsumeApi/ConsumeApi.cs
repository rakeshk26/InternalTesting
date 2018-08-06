using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net;

namespace ConsoleApp2
{
    class ConsumeApi
    {

        // Get All Function to get all records from Airtable/any onther db Using WebApi 
        // change webapi link and authentication token  
        public void GetAllEventData() 
        {
            using (var client = new WebClient()) //WebClient 
            {
                client.Headers.Add("Content-Type:application/json"); //Content-Type  
                client.Headers.Add("Accept:application/json");
                string Url = "<Web Api Link>";  
                string authenticationKey = "<Authentication Key or Token>";    

                
                var request = WebRequest.Create(Url) as HttpWebRequest;
                request.Method = "GET";
                request.Headers.Add(HttpRequestHeader.Authorization, "Bearer " + authenticationKey); //example: "Bearer F4dfghuhgudhfgJL3"
                var response = request.GetResponse() as HttpWebResponse;
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    // string result = File.ReadAllText(@"C:\temp\test.txt");
                   
                    StreamReader sReader = new StreamReader(response.GetResponseStream(), true);
                    var result = sReader.ReadToEnd();
                    dynamic data = JObject.Parse(result);  // parsing  json result into object. 
                    foreach (var item in data.records)      // code to retreive the data and update it.
                    {
                        if (item.fields.Link.ToString().Contains("drive.google.com"))
                        {
                            item.fields["File Type"] = "Drive";
                        }
                        else
                        {
                            item.fields["File Type"] = "Something Else";
                        }
                    }

                    Console.WriteLine(data);
                    Console.ReadLine();
                }
            }
        }

       
    }
}
