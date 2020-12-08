using Common.Utilities.Log;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Net;
using System.Text;

namespace Web.Topcourse.Helper
{
    public class Utils
    {
        public static string Domain { get { return GetAppsetting("DOMAIN"); } }
        public static string DomainMedia { get { return GetAppsetting("DOMAIN_MEDIA"); } }
        public static string GetAppsetting(string appSettingName)
        {
            return ConfigurationManager.AppSettings[appSettingName] ?? string.Empty;
        }
        public static bool IsReCaptchValid(string captchaResponse)
        {
            try
            {
                var result = false;
                var secretKey = ConfigurationManager.AppSettings["RecaptchaSecretKey"];
                var apiUrl = $"https://www.google.com/recaptcha/api/siteverify?secret={secretKey}&response={captchaResponse}";
                var requestUri = string.Format(apiUrl, secretKey, captchaResponse);
                var request = (HttpWebRequest)WebRequest.Create(requestUri);

                using (WebResponse response = request.GetResponse())
                {
                    using (StreamReader stream = new StreamReader(response.GetResponseStream()))
                    {
                        JObject jResponse = JObject.Parse(stream.ReadToEnd());
                        var isSuccess = jResponse.Value<bool>("success");
                        result = (isSuccess) ? true : false;
                    }
                }
                return result;
            }
            catch (System.Exception ex)
            {
                NLogManager.PublishException(ex);
                return false;
            }
            
        }

        public static string DbFormatString(string originString)
        {
            if (string.IsNullOrEmpty(originString))
                return string.Empty;
            return originString.Replace("'", "\"").Replace("#", "").Replace("|", "");
        }

        public static string GenerateRandomNumber(int textLength)
        {
            Random _rand = new Random();
            var TextChars = "ABCDEFGHJKMLNPIQRSTUVXYZW0123456789";
            StringBuilder sb = new StringBuilder(textLength);
            int maxLength = TextChars.Length;
            for (int n = 0; n <= textLength - 1; n++)
                sb.Append(TextChars.Substring(_rand.Next(maxLength), 1));

            return sb.ToString();
        }

        public static List<KeyValuePair<int, string>> ListService()
        {
            var list = new List<KeyValuePair<int, string>>();
            list.Add(new KeyValuePair<int, string>(10001, "S1Z8HY0JVDM4HAOHUIHG"));
            list.Add(new KeyValuePair<int, string>(10002, "MMMKL1IUJV3GZ4I02JIG"));
            list.Add(new KeyValuePair<int, string>(10003, "SW4ADGWWQTNPZIOE29EQ"));
            list.Add(new KeyValuePair<int, string>(10004, "CAHCPGC7EETT2K7CRMG2"));
            list.Add(new KeyValuePair<int, string>(10005, "UDNTA3YMVGRDM5KAIMYI"));
            list.Add(new KeyValuePair<int, string>(10006, "BA20P1UU1JWLDKNYQU0R"));
            return list;
        }
        public static DataTable GetInversedDataTable(DataTable table, string columnX,
                                            params string[] columnsToIgnore)
        {
            //Create a DataTable to Return
            DataTable returnTable = new DataTable();

            if (columnX == "")
                columnX = table.Columns[0].ColumnName;

            //Add a Column at the beginning of the table

            returnTable.Columns.Add(columnX);

            //Read all DISTINCT values from columnX Column in the provided DataTale
            List<string> columnXValues = new List<string>();

            //Creates list of columns to ignore
            List<string> listColumnsToIgnore = new List<string>();
            if (columnsToIgnore.Length > 0)
                listColumnsToIgnore.AddRange(columnsToIgnore);

            if (!listColumnsToIgnore.Contains(columnX))
                listColumnsToIgnore.Add(columnX);

            foreach (DataRow dr in table.Rows)
            {
                string columnXTemp = dr[columnX].ToString();
                //Verify if the value was already listed
                if (!columnXValues.Contains(columnXTemp))
                {
                    //if the value id different from others provided, add to the list of 
                    //values and creates a new Column with its value.
                    columnXValues.Add(columnXTemp);
                    returnTable.Columns.Add(columnXTemp);
                }
                else
                {
                    //Throw exception for a repeated value
                    throw new Exception("The inversion used must have " +
                                        "unique values for column " + columnX);
                }
            }

            //Add a line for each column of the DataTable

            foreach (DataColumn dc in table.Columns)
            {
                if (!columnXValues.Contains(dc.ColumnName) &&
                    !listColumnsToIgnore.Contains(dc.ColumnName))
                {
                    DataRow dr = returnTable.NewRow();
                    dr[0] = dc.ColumnName;
                    returnTable.Rows.Add(dr);
                }
            }

            //Complete the datatable with the values
            for (int i = 0; i < returnTable.Rows.Count; i++)
            {
                for (int j = 1; j < returnTable.Columns.Count; j++)
                {
                    returnTable.Rows[i][j] =
                      table.Rows[j - 1][returnTable.Rows[i][0].ToString()].ToString();
                }
            }

            return returnTable;
        }
    }
}