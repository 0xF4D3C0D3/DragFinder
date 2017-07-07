using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;
using System.IO;
namespace DragFinder
{
    class WebRequester
    {
        private static HttpWebRequest request;

        public static string getResponseSearchAPI(string query, int displayCount = 10, int startIdx = 0)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create($@"https://openapi.naver.com/v1/search/encyc.json?query={query}&display={displayCount}&start={startIdx}");

            httpWebRequest.Headers["X-Naver-Client-Id"] = APIKey.X_Naver_Client_Id;
            httpWebRequest.Headers["X-Naver-Client-Secret"] = APIKey.X_Naver_Client_Secret;

            httpWebRequest.Method = "GET";

            try
            {
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    return streamReader.ReadToEnd();
                }
            } catch (WebException e)
            {
                return e.Message;
            }
        }
    }
}
