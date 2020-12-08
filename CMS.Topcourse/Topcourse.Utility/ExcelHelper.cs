using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Web;

namespace Topcourse.Utility
{
	public class ExcelHelper
	{



		public DataTable Exceldata(string filelocation)
		{
			return new ExcelObject(filelocation).ReadTable("Sheet1$");
		}

		



		public static string WriteData(DataTable dataTable,string fileName,Dictionary<string,string>tableDefination)
		{
			var filePath = System.Configuration.ConfigurationManager.AppSettings["TEMPLATE.URL"] + "{0}.xlsx";	
			File.Copy(string.Format(filePath,"template"),string.Format(filePath,fileName));
			var excelObject = new ExcelObject(string.Format(filePath, fileName));
		    excelObject.WriteTable("Sheet1$", tableDefination);
		    excelObject.InsertStatement(dataTable, "Sheet1$");
			excelObject.Dispose();
			return fileName;
		}


		public DataTable ConvertTo<T>(IList<T> lst)
		{
			//create DataTable Structure
			var tbl = CreateTable<T>();
			var entType = typeof(T);

			var properties = TypeDescriptor.GetProperties(entType);
			//get the list item and add into the list
			foreach (var item in lst)
			{
				var row = tbl.NewRow();
				foreach (PropertyDescriptor prop in properties)
				{
					row[prop.Name] = prop.GetValue(item);
				}
				tbl.Rows.Add(row);
			}

			return tbl;
		}
		private static DataTable CreateTable<T>()
		{
			//T –> ClassName
			var entType = typeof(T);
			//set the datatable name as class name
			var tbl = new DataTable(entType.Name);
			//get the property list
			var properties = TypeDescriptor.GetProperties(entType);
			foreach (PropertyDescriptor prop in properties)
			{
				//add property as column
				try
				{
					tbl.Columns.Add(prop.Name, prop.PropertyType);
				}
				catch
				{
					if (prop.PropertyType.ToString().ToLower().Contains("datetime"))
					{
						tbl.Columns.Add(prop.Name, typeof(DateTime));
						continue;
					}

					if (prop.PropertyType.ToString().ToLower().Contains("boolean"))
					{
						tbl.Columns.Add(prop.Name, typeof(bool));
						continue;
					}
					if (prop.PropertyType.ToString().ToLower().Contains("int"))
					{
						tbl.Columns.Add(prop.Name, typeof(int));
						continue;
					}
				}
			}
			return tbl;
		}


		public static void Download(string filepath, string filename)
		{
			System.IO.Stream iStream = null;
			var buffer = new Byte[100000];
			try
			{
				iStream = new System.IO.FileStream(filepath, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.Read);
				long dataToRead = iStream.Length;
				HttpContext.Current.Response.ContentType = "application/force-download";
				HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + filename);
				while (dataToRead > 0)
				{
					if (HttpContext.Current.Response.IsClientConnected)
					{
						int length = iStream.Read(buffer, 0, 100000);
						HttpContext.Current.Response.OutputStream.Write(buffer, 0, length);
						HttpContext.Current.Response.Flush();
						buffer = new Byte[100000];
						dataToRead = dataToRead - length;
					}
					else
					{
						dataToRead = -1;
					}
				}
			}
			catch (Exception ex)
			{
				HttpContext.Current.Response.Write("Error : " + ex.Message);
			}
			finally
			{
				if (iStream != null)
				{
					iStream.Close();
				}
				HttpContext.Current.Response.Close();
			}

		}
	}
}
