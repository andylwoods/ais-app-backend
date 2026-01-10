using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace myapp.Models
{
    public class AnalysisResult
    {
        [XmlElement("slowBikeCount")]
        public int SlowBikeCount { get; set; }

        [XmlIgnore]
        public Dictionary<char, int>? ConsonantCounts { get; set; }

        [JsonIgnore]
        [XmlArray("consonantCounts")]
        [XmlArrayItem("consonant")]
        public ConsonantCount[]? SerializableConsonantCounts
        {
            get
            {
                if (ConsonantCounts == null)
                {
                    return null;
                }

                return ConsonantCounts.Select(kvp => new ConsonantCount { Consonant = kvp.Key.ToString(), Count = kvp.Value }).ToArray();
            }
            set
            {
                if (value == null)
                {
                    ConsonantCounts = null;
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
        public string Consonant { get; set; }

        [XmlElement("count")]
        public int Count { get; set; }
    }
}
