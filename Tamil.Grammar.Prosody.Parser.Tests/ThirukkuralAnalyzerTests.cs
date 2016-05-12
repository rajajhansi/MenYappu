using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Xunit;

namespace RjamSoft.Tamil.Grammar.Parser.Tests
{
    public class Athikaaram
    {
        public string Id { get; set; }
        public string AthikaaramNumber { get; set; }
        public string Name { get; set; }
    }

    public class Thirukkural
    {
        public string Id { get; set; }
        public string KuralNumber { get; set; }
        public string[] Kural { get; set; }
    }

    public class ThirukkuralStatistics
    {
        public List<Thirukkural> Nerpu { get; set; }
        public List<Thirukkural> Niraipu { get; set; }
        public List<Thirukkural> Ner { get; set; }
        public List<Thirukkural> Nirai { get; set; }
        public List<Thirukkural> Thanichol { get; set; }

        public ThirukkuralStatistics()
        {
            Ner = new List<Thirukkural>();
            Nirai = new List<Thirukkural>();
            Nerpu = new List<Thirukkural>();
            Niraipu = new List<Thirukkural>();
        }
    }

    public class ThirukkuralAnalyzerTests
    {
        [Fact]
        public void TestThirukkuralStatistics()
        {
            Thirukkural[] thirukkurals = new Thirukkural[0];
            using (StreamReader file = File.OpenText(@"InputFiles\thirukkural.json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                thirukkurals = (Thirukkural[]) serializer.Deserialize(file, typeof(Thirukkural[]));
                Assert.Equal(1330, thirukkurals.Length);
            }
            var prosodyParser = new ProsodyParser("", true, false);
            var thirukkuralStatistics = new ThirukkuralStatistics();
            var wrongThirukkurals = new List<Thirukkural>();

            foreach (var thirukkural in thirukkurals)
            {
                try
                {
                    var kural = string.Join("\r\n", thirukkural.Kural);
                    var prosodyPart = prosodyParser.Parse(kural);
                    if (!prosodyPart.MetreType.Contains("வெண்பா"))
                    {
                        wrongThirukkurals.Add(thirukkural);
                    }
                    switch (prosodyPart.VenLastSyllable)
                    {
                        case "நேர்":
                            thirukkuralStatistics.Ner.Add(thirukkural);
                            break;
                        case "நிரை":
                            thirukkuralStatistics.Nirai.Add(thirukkural);
                            break;
                        case "நேர்பு":
                            thirukkuralStatistics.Nerpu.Add(thirukkural);
                            break;
                        case "பு":
                            thirukkuralStatistics.Niraipu.Add(thirukkural);
                            break;
                    }
                    if (prosodyPart.IsLastSyllableSingleLetter)
                    {
                        thirukkuralStatistics.Thanichol.Add(thirukkural);
                    }
                }
                catch (Exception e)
                {
                    wrongThirukkurals.Add(thirukkural);
                }
            }
            var wrongThirukkuralJsonFile = @"InputFiles\wrongThirukkural.json";
            var thirukkuralStatisticsJsonFile = @"InputFiles\thirukkuralStatistics.json";
            var jsonSerializer = new JsonSerializer();
            jsonSerializer.Formatting = Formatting.Indented;
            jsonSerializer.ContractResolver = new CamelCasePropertyNamesContractResolver();
            using (var jsonWriter = new JsonTextWriter(new StreamWriter(wrongThirukkuralJsonFile)))
            {
                jsonSerializer.Serialize(jsonWriter, wrongThirukkurals);
            }
            using (var jsonWriter = new JsonTextWriter(new StreamWriter(thirukkuralStatisticsJsonFile)))
            {
                jsonSerializer.Serialize(jsonWriter, thirukkuralStatistics);
            }
            Console.WriteLine($"# Thirukkurals with Nerpu {thirukkuralStatistics.Nerpu.Count}");
            Console.WriteLine($"# Thirukkurals with Niraipu {thirukkuralStatistics.Niraipu.Count}");
        }
    }
}
