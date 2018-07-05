using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Threading.Tasks;
using Karakoram.Address.Lookup.Services.Interfaces;
using Karakoram.Address.Lookup.Services.Models;


namespace Karakoram.Address.Lookup.Services.LookupServices
{
    //Use C sharp SDK if available, otherwise do direct rest calls, but discuss
    //details with Noor of how to make REST API
    //Remember to gracefully handle service outages on their end

    class PostCoderAddressLookupService : IAddressLookupService
    {
        #region fields

        string key = "GN96-ET13-XR18-JF12"; // default = GN96-ET13-XR18-JF12
        string id = "";
        string text = "";
        string container = "";
        string origin = "";
        string countries = "";
        int limit = 50; // default = 50
        string language = "en"; // default = en;

        string field1format = "";
        string field2format = "";
        string field3format = "";
        string field4format = "";
        string field5format = "";
        string field6format = "";
        string field7format = "";
        string field8format = "";
        string field9format = "";
        string field10format = "";
        string field11format = "";
        string field12format = "";
        string field13format = "";
        string field14format = "";
        string field15format = "";
        string field16format = "";
        string field17format = "";
        string field18format = "";
        string field19format = "";
        string field20format = "";

        #endregion

        public void Initialise(Dictionary<string, string> intialisationData)
        {
            // See whether Dictionary contains this string.
            if (intialisationData.ContainsKey("key"))
            {
                key = intialisationData["key"];
            }

            if (intialisationData.ContainsKey("id"))
            {
                id = intialisationData["id"];
            }

            if (intialisationData.ContainsKey("text"))
            {
                text = intialisationData["text"];
            }

            if (intialisationData.ContainsKey("container"))
            {
                container = intialisationData["container"];
            }

            if (intialisationData.ContainsKey("origin"))
            {
                origin = intialisationData["origin"];
            }

            if (intialisationData.ContainsKey("countries"))
            {
                countries = intialisationData["countries"];
            }

            if (intialisationData.ContainsKey("limit"))
            {
                limit = Convert.ToInt32(intialisationData["limit"]);
            }

            if (intialisationData.ContainsKey("language"))
            {
                language = intialisationData["language"];
            }
        }

        /// <summary>
        /// Find Method for the Loqate Service
        /// </summary>
        /// <returns></returns>
        public Task<List<LoqateFindResult>> Find()
        {
            //Build the url
            var url = "https://api.addressy.com/Capture/Interactive/Find/v1.00/dataset.ws?";
            url += "&Key=" + System.Web.HttpUtility.UrlEncode(key);
            url += "&Text=" + System.Web.HttpUtility.UrlEncode(text);
            url += "&Container=" + System.Web.HttpUtility.UrlEncode(container);
            url += "&Origin=" + System.Web.HttpUtility.UrlEncode(origin);
            url += "&Countries=" + System.Web.HttpUtility.UrlEncode(countries);
            url += "&Limit=" + System.Web.HttpUtility.UrlEncode(limit.ToString(CultureInfo.InvariantCulture));
            url += "&Language=" + System.Web.HttpUtility.UrlEncode(language);

            //Create the dataset
            var dataSet = new DataSet();
            dataSet.ReadXml(url);

            //Check for an error
            if (dataSet.Tables.Count == 1 && dataSet.Tables[0].Columns.Count == 4 && dataSet.Tables[0].Columns[0].ColumnName == "Error")
                throw new Exception(dataSet.Tables[0].Rows[0].ItemArray[1].ToString());

            var results = new List<LoqateFindResult>();

            for (var i = 0; i < dataSet.Tables[0].Rows.Count; i++)
            {
                results.Add(new LoqateFindResult
                {
                    Id = Convert.ToString(dataSet.Tables[0].Rows[i]["id"]),
                    Type = Convert.ToString(dataSet.Tables[0].Rows[i]["type"]),
                    Text = Convert.ToString(dataSet.Tables[0].Rows[i]["text"]),
                    Highlight = Convert.ToString(dataSet.Tables[0].Rows[i]["highlight"]),
                    Description = Convert.ToString(dataSet.Tables[0].Rows[i]["description"])
                });
            }

            //Return the dataset
            return Task.FromResult(results);

            //FYI: The dataset contains the following columns:
            //Id
            //Type
            //Text
            //Highlight
            //Description

            //throw new NotImplementedException();
        }

        /// <summary>
        /// Retrive Method for the Loqate Service
        /// </summary>
        /// <returns></returns>
        public Task<LoqateRetrieveResult> Retrive()
        {
            //Build the url
            var url = "https://api.addressy.com/Capture/Interactive/Retrieve/v1.00/dataset.ws?";
            url += "&Key=" + System.Web.HttpUtility.UrlEncode(key);
            url += "&Id=" + System.Web.HttpUtility.UrlEncode(id);
            url += "&Field1Format=" + System.Web.HttpUtility.UrlEncode(field1format);
            url += "&Field2Format=" + System.Web.HttpUtility.UrlEncode(field2format);
            url += "&Field3Format=" + System.Web.HttpUtility.UrlEncode(field3format);
            url += "&Field4Format=" + System.Web.HttpUtility.UrlEncode(field4format);
            url += "&Field5Format=" + System.Web.HttpUtility.UrlEncode(field5format);
            url += "&Field6Format=" + System.Web.HttpUtility.UrlEncode(field6format);
            url += "&Field7Format=" + System.Web.HttpUtility.UrlEncode(field7format);
            url += "&Field8Format=" + System.Web.HttpUtility.UrlEncode(field8format);
            url += "&Field9Format=" + System.Web.HttpUtility.UrlEncode(field9format);
            url += "&Field10Format=" + System.Web.HttpUtility.UrlEncode(field10format);
            url += "&Field11Format=" + System.Web.HttpUtility.UrlEncode(field11format);
            url += "&Field12Format=" + System.Web.HttpUtility.UrlEncode(field12format);
            url += "&Field13Format=" + System.Web.HttpUtility.UrlEncode(field13format);
            url += "&Field14Format=" + System.Web.HttpUtility.UrlEncode(field14format);
            url += "&Field15Format=" + System.Web.HttpUtility.UrlEncode(field15format);
            url += "&Field16Format=" + System.Web.HttpUtility.UrlEncode(field16format);
            url += "&Field17Format=" + System.Web.HttpUtility.UrlEncode(field17format);
            url += "&Field18Format=" + System.Web.HttpUtility.UrlEncode(field18format);
            url += "&Field19Format=" + System.Web.HttpUtility.UrlEncode(field19format);
            url += "&Field20Format=" + System.Web.HttpUtility.UrlEncode(field20format);

            //Create the dataset
            var dataSet = new DataSet();
            dataSet.ReadXml(url);

            //Check for an error
            if (dataSet.Tables.Count == 1 && dataSet.Tables[0].Columns.Count == 4 && dataSet.Tables[0].Columns[0].ColumnName == "Error")
                throw new Exception(dataSet.Tables[0].Rows[0].ItemArray[1].ToString());

            var result = new LoqateRetrieveResult
            {
                Id = Convert.ToString(dataSet.Tables[0].Rows[0]["id"]),
                Company = Convert.ToString(dataSet.Tables[0].Rows[0]["company"]),
                City = Convert.ToString(dataSet.Tables[0].Rows[0]["city"]),
                Line1 = Convert.ToString(dataSet.Tables[0].Rows[0]["line1"]),
                Line2 = Convert.ToString(dataSet.Tables[0].Rows[0]["line2"]),
                Line3 = Convert.ToString(dataSet.Tables[0].Rows[0]["line3"]),
                Line4 = Convert.ToString(dataSet.Tables[0].Rows[0]["line4"]),
                Line5 = Convert.ToString(dataSet.Tables[0].Rows[0]["line5"]),
                Province = Convert.ToString(dataSet.Tables[0].Rows[0]["province"]),
                PostalCode = Convert.ToString(dataSet.Tables[0].Rows[0]["postalCode"])
            };

            //Return the dataset
            return Task.FromResult(result);

            //FYI: The dataset contains the following columns:
            //Id
            //DomesticId
            //Language
            //LanguageAlternatives
            //Department
            //Company
            //SubBuilding
            //BuildingNumber
            //BuildingName
            //SecondaryStreet
            //Street
            //Block
            //Neighbourhood
            //District
            //City
            //Line1
            //Line2
            //Line3
            //Line4
            //Line5
            //AdminAreaName
            //AdminAreaCode
            //Province
            //ProvinceName
            //ProvinceCode
            //PostalCode
            //CountryName
            //CountryIso2
            //CountryIso3
            //CountryIsoNumber
            //SortingNumber1
            //SortingNumber2
            //Barcode
            //POBoxNumber
            //Label
            //Type
            //DataLevel
            //Field1
            //Field2
            //Field3
            //Field4
            //Field5
            //Field6
            //Field7
            //Field8
            //Field9
            //Field10
            //Field11
            //Field12
            //Field13
            //Field14
            //Field15
            //Field16
            //Field17
            //Field18
            //Field19
            //Field20

            //throw new NotImplementedException();
        }
    }
}
