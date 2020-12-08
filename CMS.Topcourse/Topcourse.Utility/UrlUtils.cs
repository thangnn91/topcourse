using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Topcourse.Utility
{

    public class UrlUtils
    {
        private static String[] aEncode = "0|1|2|3|4|5|6|7|8|9|-".Split(new Char[] { '|' });
        private static String[] aDecode = "a|b|c|d|e|f|g|h|i|j|k".Split(new Char[] { '|' });
        public int CateID
        {
            get;
            set;
        }
        public long ItemID
        {
            get;
            set;
        }
        public UrlUtils()
        {
            CateID = 0;
            ItemID = 0;
        }
        public UrlUtils(int cateID)
        {
            CateID = cateID;
            ItemID = 0;
        }
        public UrlUtils(int cateID, long itemID)
        {
            CateID = cateID;
            ItemID = itemID;
        }
        public string Encode()
        {
            String sRet = CateID.ToString() + "-" + ItemID.ToString();
            //Int32 nLimitChar = aEncode.GetUpperBound(0);
            //for (int i = 0; i <= nLimitChar; i++)
            //{
            //    sRet = sRet.Replace(aEncode[i], aDecode[i]);
            //}
            return sRet;
        }
        public void Decode(string cipherText)
        {
            try
            {
                Int32 nLimitChar = aDecode.GetUpperBound(0);
                for (int i = 0; i <= nLimitChar; i++)
                {
                    cipherText = cipherText.Replace(aDecode[i], aEncode[i]);
                }
                string[] arrDecode = cipherText.Split("-".ToCharArray());
                CateID = int.Parse(arrDecode[0]);
                if (arrDecode.Length > 1)
                {
                    ItemID = long.Parse(arrDecode[1]);
                }
            }
            catch
            {
                CateID = 0;
                ItemID = 0;
            }
        }
    }
}
