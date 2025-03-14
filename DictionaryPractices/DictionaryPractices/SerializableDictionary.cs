using System.Xml.Serialization;

namespace DictionaryPractices
{
    [XmlRoot]
    public class SerializableDictionary<TKey, TValue> 
    {
        [XmlArray("Items")]
        [XmlArrayItem("Item")]
        public List<KeyValuePair<TKey, TValue>> Items { get; set; } = new();
        public SerializableDictionary()
        {
        }

        public SerializableDictionary(Dictionary<TKey, TValue> dictionary)
        {
            Items = new List<KeyValuePair<TKey, TValue>>(dictionary);
        }

        public Dictionary<TKey, TValue> ToDictionary()
        {
            return new Dictionary<TKey, TValue>(Items);
        }
    }
}
