using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace myapp.Models
{
    public class AnalysisResult
    {
        [XmlElement("slowBikeCount")]
        public int SlowBikeCount { get; set; }

        [XmlIgnore]
        public Dictionary<char, int> ConsonantCounts { get; set; } = new Dictionary<char, int>();

        [JsonIgnore]
        [XmlArray("consonantCounts")]
        [XmlArrayItem("consonant")]
        public ConsonantCount[] SerializableConsonantCounts
        {
            get
            {
                return ConsonantCounts.Select(kvp => new ConsonantCount { Consonant = kvp.Key.ToString(), Count = kvp.Value }).ToArray();
            }
            set
            {
                if (value == null)
                {
                    ConsonantCounts = new Dictionary<char, int>();
                }
                else
                {
                    ConsonantCounts = value.ToDictionary(cc => cc.Consonant[0], cc => cc.Count);
                }
            }
        }
    }

    public class ConsonantCount
    {
        [XmlElement("letter")]
        public string Consonant { get; set; } = string.Empty;

        [XmlElement("count")]
        public int Count { get; set; }
    }
}
