using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web;
using CMS.Topcourse.Models;
using Topcourse.DataAccess.DTO;
using Topcourse.Utility;
using Topcourse.Utility.Security;

namespace CMS.Topcourse.Controllers.Common
{
    public class CommonLib
    {
        private UserFunction Permission { get { return ((UserFunction)HttpContext.Current.Session[SessionsManager.SESSION_PERMISSION]); } }

        public List<TreeFunction> GetListTreeFunction(int ParentsID, List<Functions> roots)
        {
            var tmp = new List<TreeFunction>();
            var levesub = roots.FindAll(c => c.ParentID == ParentsID);
            levesub.Sort((f1, f2) => f1.FunctionID.CompareTo(f2.FunctionID));
            if (levesub.Count <= 0) return null;
            foreach (var t in levesub)
            {
                roots.Remove(t);
                var data = new TreeFunction
                {
                    FuntionId = t.FunctionID,
                    ParentId = t.ParentID,
                    text = t.FunctionName,
                    icon = t.CssIcon,
                    IsBtnGrant = false
                };
                if (!string.IsNullOrEmpty(t.Url))
                {
                    data.IsBtnGrant = true;
                }
                tmp.Add(data);
                var childrens = GetListTreeFunction(t.FunctionID, roots);
                data.nodes = childrens;
            }
            return tmp;


        }

        // Select nhiều
        public static List<TreeCategory> GetListTreeCategory(int parentId, List<Category> roots)
        {
            var tmp = new List<TreeCategory>();
            var levesub = roots.FindAll(c => c.ParentID == parentId);
            levesub.Sort((f1, f2) => f1.Id.CompareTo(f2.Id));
            if (levesub.Count <= 0) return null;
            foreach (var t in levesub)
            {
                roots.Remove(t);
                var data = new TreeCategory
                {
                    CategoryId = t.Id,
                    ParentId = t.ParentID,
                    SystemId = t.SystemID,
                    LanguageId = t.LanguageID,
                    status = t.Status,
                    selectable = false,
                    text = "(" + t.Id + ")" + t.CategoryName,
                    state = new state()
                };
                tmp.Add(data);
                var childrens = GetListTreeCategory(t.Id, roots);
                data.nodes = childrens;
            }
            return tmp;
        }

        public static List<TreeCategory> GetListTreeCategoryByArticleId(int parentId, List<Category> roots, List<Article2Cate> listArticle, int mainCateID)
        {
            var tmp = new List<TreeCategory>();
            var levesub = roots.FindAll(c => c.ParentID == parentId);
            levesub.Sort((f1, f2) => f1.Id.CompareTo(f2.Id));
            if (levesub.Count <= 0) return null;
            foreach (var t in levesub)
            {
                roots.Remove(t);
                var data = new TreeCategory
                {
                    CategoryId = t.Id,
                    ParentId = t.ParentID,
                    SystemId = t.SystemID,
                    LanguageId = t.LanguageID,
                    status = t.Status,
                    text = t.CategoryName,
                    selectable = false,
                    state = new state()
                };
                if (listArticle.Any(o => o.CateId == t.Id))
                {
                    data.state.@checked = true;
                    data.state.disabled = false;
                    data.state.expanded = true;
                    data.state.selected = false;
                }
                else
                {
                    data.state.@checked = false;
                    data.state.disabled = false;
                    data.state.expanded = false;
                    data.state.selected = false;
                }
                if (mainCateID == t.Id) // Chuyên mục chính
                {
                    //data.state.@checked = true;
                    //data.state.disabled = false;
                    //data.state.expanded = true;
                    //data.state.selected = false;
                    data.color = "#f45042";
                }
                tmp.Add(data);
                var childrens = GetListTreeCategoryByArticleId(t.Id, roots, listArticle, mainCateID);
                data.nodes = childrens;
            }
            return tmp;
        }

        public static List<TreeCategory> GetListTreeCategoryMain(int parentId, List<Category> roots, List<Article2Cate> listArticle, int mainCateID)
        {
            var tmp = new List<TreeCategory>();
            var levesub = roots.FindAll(c => c.ParentID == parentId);
            levesub.Sort((f1, f2) => f1.Id.CompareTo(f2.Id));
            if (levesub.Count <= 0) return null;
            foreach (var t in levesub)
            {
                roots.Remove(t);
                var data = new TreeCategory
                {
                    CategoryId = t.Id,
                    ParentId = t.ParentID,
                    SystemId = t.SystemID,
                    LanguageId = t.LanguageID,
                    status = t.Status,
                    text = t.CategoryName,
                    selectable = true,
                    state = new state()
                };

                if (mainCateID == t.Id) // Chuyên mục chính
                {
                    data.state.@checked = false;
                    data.state.disabled = false;
                    data.state.expanded = true;
                    data.state.selected = true;
                }
                else
                {
                    data.state.@checked = false;
                    data.state.disabled = false;
                    data.state.expanded = false;
                    data.state.selected = false;
                }
                tmp.Add(data);
                var childrens = GetListTreeCategoryMain(t.Id, roots, listArticle, mainCateID);
                data.nodes = childrens;
            }
            return tmp;
        }

        // select 1
        public static List<TreeCategory> GetListTreeCategoryOne(int parentId, List<Category> roots)
        {
            var tmp = new List<TreeCategory>();
            var levesub = roots.FindAll(c => c.ParentID == parentId);
            levesub.Sort((f1, f2) => f1.Id.CompareTo(f2.Id));
            if (levesub.Count <= 0) return null;
            foreach (var t in levesub)
            {
                roots.Remove(t);
                var data = new TreeCategory
                {
                    CategoryId = t.Id,
                    ParentId = t.ParentID,
                    SystemId = t.SystemID,
                    LanguageId = t.LanguageID,
                    status = t.Status,
                    selectable = true,
                    text = t.CategoryName,
                    state = new state()
                };
                tmp.Add(data);
                var childrens = GetListTreeCategoryOne(t.Id, roots);
                data.nodes = childrens;
            }
            return tmp;
        }

        public static string GenerateSign(string dataSign, string keySign)
        {
            var sign = string.Empty;
            try
            {
                var ticks = DateTime.Now.Ticks;
                var plaintextSign = string.Format("{0}|{1}|{2}", dataSign, keySign, ticks);
                sign = string.Format("{0}.{1}", ticks, Encrypt.Md5(plaintextSign));
                return sign;
            }
            catch (Exception ex)
            {
                Log4Net.PublishException(ex);
                return sign;
            }
        }

        public static long GenUnixTime(DateTime DTime)
        {
            long unixTimeStamp;
            DateTime currentTime = DateTime.Now;
            DateTime zuluTime = DTime.ToUniversalTime().AddHours(+7);
            DateTime unixEpoch = new DateTime(1970, 1, 1);
            unixTimeStamp = (Int64)(zuluTime.Subtract(unixEpoch)).TotalMilliseconds;

            return unixTimeStamp / 1000;
        }



        #region Chuyen list -> datatable
        public DataTable ToDataTable<T>(List<T> items)
        {
            var tb = new DataTable(typeof(T).Name);

            PropertyInfo[] props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (PropertyInfo prop in props)
            {
                Type t = GetCoreType(prop.PropertyType);
                tb.Columns.Add(prop.Name, t);
            }


            foreach (T item in items)
            {
                var values = new object[props.Length];

                for (int i = 0; i < props.Length; i++)
                {
                    values[i] = props[i].GetValue(item, null);
                }

                tb.Rows.Add(values);
            }

            return tb;
        }
        public static Type GetCoreType(Type t)
        {
            if (t != null && IsNullable(t))
            {
                if (!t.IsValueType)
                {
                    return t;
                }
                else
                {
                    return Nullable.GetUnderlyingType(t);
                }
            }
            else
            {
                return t;
            }
        }
        public static bool IsNullable(Type t)
        {
            return !t.IsValueType || (t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>));
        }

        #endregion
    }
    public static class RandomLetter
    {
        static Random _random = new Random();
        public static char GetLetter()
        {
            // This method returns a random lowercase letter.
            // ... Between 'a' and 'z' inclusize.
            int num = _random.Next(0, 26); // Zero to 25
            char let = (char)('a' + num);
            return let;
        }
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }

    public class Converter
    {
        public static readonly string[] VietNamChar = new string[]
        {
            "aAeEoOuUiIdDyY",
            "áàạảãâấầậẩẫăắằặẳẵ",
            "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ",
            "éèẹẻẽêếềệểễ",
            "ÉÈẸẺẼÊẾỀỆỂỄ",
            "óòọỏõôốồộổỗơớờợởỡ",
            "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ",
            "úùụủũưứừựửữ",
            "ÚÙỤỦŨƯỨỪỰỬỮ",
            "íìịỉĩ",
            "ÍÌỊỈĨ",
            "đ",
            "Đ",
            "ýỳỵỷỹ",
            "ÝỲỴỶỸ"
        };
        public static string LocDau(string str)
        {
            //Thay thế và lọc dấu từng char      
            for (int i = 1; i < VietNamChar.Length; i++)
            {
                for (int j = 0; j < VietNamChar[i].Length; j++)
                    str = str.Replace(VietNamChar[i][j], VietNamChar[0][i - 1]);
            }
            return str.Replace(" ", "-");
        }

        public static string[] SplitFullName(string fullName)
        {
            if (string.IsNullOrEmpty(fullName))
                return new string[] { "Tên", "Mặc Định" };
            if (!fullName.Contains(" "))
                return new string[] { fullName, fullName };
            fullName = Regex.Replace(fullName, @"\s+", " ");
            var partOfName = fullName.Split(' ');
            return new string[] { partOfName[0], string.Join(" ", partOfName.Skip(1)) };
        }
    }

   

}