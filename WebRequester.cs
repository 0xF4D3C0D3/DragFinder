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
            var request = (HttpWebRequest)WebRequest.Create($@"https://openapi.naver.com/v1/search/encyc.json?query={query}&display={displayCount}&start={startIdx}");

            request.Headers["X-Naver-Client-Id"] = APIKey.X_Naver_Client_Id;
            request.Headers["X-Naver-Client-Secret"] = APIKey.X_Naver_Client_Secret;

            request.Method = "GET";

            try
            {
                var httpResponse = (HttpWebResponse)request.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    return streamReader.ReadToEnd();
                }
            }
            catch (WebException e)
            {
                return e.Message;
            }
        }

        public static string getResponseTranslateAPI(string source, string text)
        {

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(@"https://openapi.naver.com/v1/language/translate");

            request.Headers["X-Naver-Client-Id"] = APIKey.X_Naver_Client_Id;
            request.Headers["X-Naver-Client-Secret"] = APIKey.X_Naver_Client_Secret;

            request.Method = "POST";
            byte[] byteDataParams = Encoding.UTF8.GetBytes($"source={source}&target=ko&text={text}");
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = byteDataParams.Length;

            try
            {
                Stream st = request.GetRequestStream();
                st.Write(byteDataParams, 0, byteDataParams.Length);
                st.Close();
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream stream = response.GetResponseStream();

                using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                {
                    return reader.ReadToEnd();
                }
            } catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}
