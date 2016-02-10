using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyMatch.Models.payment
{
    public class paypal
    {
        public static string paypal_pay(decimal amount,string username)
        {

            decimal d = new decimal();
            //Converting String Money Value Into Decimal
            //    string s = string.Format("{0:F}", amount);
                d = amount;
            
            //declaring empty String
            string returnURL = "";
            returnURL += "https://www.paypal.com/xclick/business=KDG32QWYAZ9PY";
            //PassingItem Name as dynamic
            returnURL += "&item_name=" + username;
            //Passing Amount as Dynamic
            returnURL += "&amount=" + d;
            //Passing Currency as your
            returnURL += "&currency=USD";
            //return Url if Customer wants To return to Previous Page
            returnURL += "&return=http://www.paymobd.com/Home/Thank_You";
            //return Url if Customer Wants To Cancel the Transaction
            returnURL += "&cancel_return=http://www.paymobd.com";
            //BIlling information prefilled 
            //AssigningName as Statically to Parameter
            returnURL += "&first_name=" + username;
            returnURL += "&last_name=" + "Optional";
            returnURL += "&address1=" + "Optional";
            returnURL += "&address2=" + "Optional";
            returnURL += "&city=" + "Optional";
            returnURL += "&state=" + "Optional";
            returnURL += "&zip=" + "12345";
            returnURL += "&night_phone_a=" + "91";
            returnURL += "&night_phone_b=" + "0000000000";
            return returnURL;

        }
    }
}