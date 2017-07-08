using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;
using System.Net;
using System.Web;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;

namespace DragFinder
{
    class DictParser
    {

        private static IEnumerable<string> Split(string str, int maxChunkSize)
        {
            for (int i = 0; i < str.Length; i += maxChunkSize)
                yield return str.Substring(i, Math.Min(maxChunkSize, str.Length - i));
        }

        public static string getTranslateFromNaverAPI(string source, string text)
        {
            var jsonResponseRaw = WebRequester.getResponseTranslateAPI(source, text);
            try
            {
                JObject jsonResponse = JObject.Parse(jsonResponseRaw);

                string result = (string)jsonResponse["message"]["result"]["translatedText"];
                
                return result;
            }
            catch (Exception e)
            {
                return $@"죄송합니다. {jsonResponseRaw} 에러가 터졌네요. 여길 가서 확인해보세요 -> https://developers.naver.com/docs/common/common_error/";
            }
        }

        public static List<KeyValuePair<string, string>> getInfoFromNaverAPI(string queryWord, int currentDisplayCount)
        {
            var jsonResponseRaw = WebRequester.getResponseSearchAPI(queryWord, 5, currentDisplayCount);
            try
            {
                JObject jsonResponse = JObject.Parse(jsonResponseRaw);

                var result = new List<KeyValuePair<string, string>>();

                int idx = currentDisplayCount;
                foreach (var i in jsonResponse["items"])
                {
                    string description = HttpUtility.HtmlDecode((string)i["description"]);
                    description = Regex.Replace(description, "<.*?>", string.Empty);

                    string title = HttpUtility.HtmlDecode((string)i["title"]);
                    title = Regex.Replace(title, "<.*?>", string.Empty);

                    var pair = new KeyValuePair<string, string>($"{idx++}. {title} : {description}\r\n", (string)i["link"]);

                    result.Add(pair);
                }

                if(result.Count == 0)
                {
                    return new List<KeyValuePair<string, string>> { new KeyValuePair<string, string>($"There are no {queryWord}", $"www.google.com/#q={queryWord}") };
                }

                return result;
            }
            catch(Exception e)
            {
                return new List<KeyValuePair<string, string>> { new KeyValuePair<string, string>(jsonResponseRaw, @"https://developers.naver.com/docs/common/common_error/") } ;
            }
        }
    }
}
