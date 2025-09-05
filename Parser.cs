using System;
using System.IO;
using Newtonsoft.Json;
using System.Net;
using Newtonsoft.Json.Linq;

namespace JsonOffsetParser
{
    internal class Parser
    {
        static readonly string uri_clientdll = "https://raw.githubusercontent.com/a2x/cs2-dumper/refs/heads/main/output/client_dll.json";
        static readonly string uri_offsetcs = "https://raw.githubusercontent.com/a2x/cs2-dumper/refs/heads/main/output/offsets.json";
        static string clientDll_Content = default;
        static string offsetcs_content = default;
        public JObject client_obj = null;
        public JObject offset_obj = null;
        public Parser()
        {
            clientDll_Content = get_content(uri_clientdll);
            offsetcs_content = get_content(uri_offsetcs);
            client_obj = JObject.Parse(clientDll_Content);
            Console.WriteLine("[~] parsed client.dll");
            offset_obj = JObject.Parse(offsetcs_content);
            Console.WriteLine("[~] parsed offsets.cs");
        }

        public string get_content(string uri)
        {
            string result = default; 
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    result = client.GetStringAsync(uri).Result;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine($"[!] {ex.Message}");
                return string.Empty;
            }
            if (result == null || result == string.Empty)
            {
                return "result is equal to null or empty_string.";
            }
            Console.WriteLine($"[~] loaded web content of length -> {result.Length}");
            return result;

        }

        /// <summary>
        /// "type" is basically if we are scanning Clientdll.cs or offsets.cs
        /// 0 = client, 1 = offsets
        /// :-]
        /// </summary>
        public IntPtr get_offset_by_classname(int type, string classname, string offset_name)
        {
            switch (type) {
                case 0:
                    string value = (string)client_obj?["client.dll"]?["classes"]?[classname]?["fields"]?[offset_name];
                    if (value != string.Empty || value != null)
                        return (nint)Convert.ToInt64(value);
                    break;
                case 1:
                    string value1 = (string)offset_obj["client.dll"]?[offset_name];
                    if (value1 != string.Empty || value1 != null)
                        return (nint)Convert.ToInt64(value1);
                    break;

                default:
                    return 0;
            }

            return 0; 
        }



    }
}
