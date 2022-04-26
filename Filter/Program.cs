

using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
namespace Filter
{
    public class Program
    {
        static string filterFlag = "--filter=";
        //Creating stream nad checkiing json for completeness
        static void Main(string[] args)
        {
            var filters = gettingPar(args);
            using (FileStream s = File.Open(args[0], FileMode.Open))
            using (StreamReader sr = new StreamReader(s))
            using (JsonReader reader = new JsonTextReader(sr))
            {
                int beginIndex = 0;
                int endIndex = 0;
                int beginJson = 0;
                string thisJson = "";
                while (!sr.EndOfStream)
                {
                    var jsonInput = sr.ReadLine();
                    while (!String.IsNullOrEmpty(jsonInput))
                    {
                        if (beginJson == 0)
                        {
                            if (jsonInput.Contains("{"))
                            {
                                beginIndex = jsonInput.IndexOf("{");
                                beginJson = 1;
                                jsonInput = jsonInput.Substring(beginIndex, jsonInput.Length - beginIndex);
                            }
                            else
                            {
                                jsonInput = "";
                            }
                        }
                        else
                        {
                            if (jsonInput.Contains("}"))
                            {
                                endIndex = jsonInput.IndexOf("}");
                                thisJson = thisJson + jsonInput.Substring(0, endIndex + 1);
                                jsonInput = jsonInput.Substring(endIndex + 1, jsonInput.Length - endIndex - 1);
                                Filtering(thisJson, filters);
                                thisJson = "";
                                beginJson = 0;
                            }
                            else
                            {
                                thisJson = thisJson + jsonInput;
                                jsonInput = "";
                            }
                        }

                    }

                }
            }

        }
        //Checking input for valid Flag and replacing the Flag for further filtration
        public static string gettingPar(string[] args)
        {
            string result = "";
            for (var c = 1; c < args.Length; c++)
            {
                if (args[c].StartsWith(filterFlag))
                {
                    result = args[c].Replace(filterFlag, "");
                    break;
                }
                else
                {
                    Console.WriteLine($"Command flag {args[c]} is incorrect. Should be: --filter=");
                }
            }
            return result;
        }

        //Generating JSON path to filtering JSON file
        public static string Filtering(string thisJson, string filterText)
        {
            List<JToken> tokensToKeep = new List<JToken>();//not necessary it is for testing
            if (filterText.Length > 1)
            {
                var recordIndicator = "@@";
                var filter = filterText.Replace("@@", "@.");
                var result = "$..[?(" + filter + ")]";
                JObject source = JObject.Parse(thisJson);
                tokensToKeep = source.SelectTokens(result).ToList();
                if (tokensToKeep.Count != 0)
                {
                    Console.WriteLine(thisJson);
                    
                }
            }
            return tokensToKeep.Count.ToString();//not necessary it is for testing


        }
    }
}


//dotnet run output.txt --filter="@@minimumOrder==1 && @@_sequenceNumber == 1"
//dotnet run output.txt --filter="@@minimumOrder==1 || @@title == 'Programming Windows'"
//dotnet run output.txt --filter="@@_sequenceNumber == 0"

