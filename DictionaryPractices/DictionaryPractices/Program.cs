using DictionaryPractices;

#region Create a Dictionary
//1. Create an empty Dictionary:
using System.Xml.Serialization;

Dictionary<int, string> students1 = new Dictionary<int, string>();

//2. Create a Dictionary with initial values:
var students2 = new Dictionary<int, string>()
{
    {1, "Leyla"},
    {2, "Hasan"},
    {3, "Aysel"}
};

//3. Create a Dictionary with using KeyValuePair
//var students3 = new Dictionary<int, string>()
//{
//    new KeyValuePair(1, "Leyla"),
//    new KeyValuePair(2, "Hasan")
//};

#endregion

#region Add an Element
//1.with Add method
students1.Add(1, "Karim");
students1.Add(2, "Natiq");
//students1.Add(2, "Rafiqa"); ArgumentException because of the same key 2.

//2. with [] operand
students2[3] = "Nigar";
students2[4] = "Orxan";
students2[4] = "Valida"; //if key exists, it changes the value.
#endregion

#region Get an Element
//1. with [] operand
//Console.WriteLine(students1[1]);

//2. with checking key
if (students2.ContainsKey(2))
{
    Console.WriteLine("Key exists!");
}
else
{
    Console.WriteLine("Key doesn’t exist!");
}

//3. with checking value
if (students2.ContainsValue("Nigar"))
{
    Console.WriteLine("Value exists!");
}
else
{
    Console.WriteLine("Value doesn’t exist!");
}

//4. with TryGetValue (most safest)
if (students2.TryGetValue(5, out string name))
{
    Console.WriteLine($"{name} Found!");
}
else
{
    Console.WriteLine("Student doesn’t exist!");
}
Console.WriteLine("\n");
#endregion

#region Remove an Element
//1. with Remove
//bool isRemoved = students1.Remove(1);
//if (isRemoved) Console.WriteLine("Student was removed successfully!");
//else Console.WriteLine("Student doesn't exist");

//2. Clear all dictionary
students1.Clear();
Console.WriteLine("Dictionary was cleared!");

//3. with LINQ queries
var keysToRemove = students2.Where(s => s.Value == "Orxan").Select(s => s.Key).ToList();
if (keysToRemove.Count() == 0) Console.WriteLine("Students don't exist");
foreach (var key in keysToRemove)
{
    students2.Remove(key);
    Console.WriteLine("Orxan was removed successfully!");
};
Console.WriteLine("\n");
#endregion

#region Alternatives 
//lock
Dictionary<int, string> dict = new Dictionary<int, string>();
object _lock = new Object();
Parallel.For(0, 1000, i =>
{
    lock (_lock)
    {
        dict[i] = i.ToString();
    }
});
Console.WriteLine($"Element count: {dict.Count}");

//ReaderWriterLockSlim
ReaderWriterLockSlim rwLock = new ReaderWriterLockSlim();
Parallel.For(0, 1000, i =>
{
    rwLock.EnterWriteLock();
    try
    {
        dict[i] = i.ToString();
    }
    finally
    {
        rwLock.ExitWriteLock();
    }
});

Console.WriteLine("\n");
#endregion

#region Serialization/Deserialization
//1. Json - serialization
//string json = Json.Serializer.Serialize(students2);
//deserialization
//Dictionary<int, string> fruits = Json.Serializer.Deserialize<Dictionary<int, string>>(json);


//2. XML - serialization
Dictionary<int, string> fruits = new Dictionary<int, string>
{
    {1, "Alma"},
    {2, "Armud"},
    {3, "Heyva"}
};
XmlSerializer serializer = new XmlSerializer(typeof(SerializableDictionary<int, string>));
using (StringWriter writer = new StringWriter())
{
    serializer.Serialize(writer, new SerializableDictionary<int, string>(fruits));
}

//deserialization
var xmlData = "<Dictionary>\r\n    <Items>\r\n        <Item>\r\n            <Key> 1 </Key>\r\n            <Value> Alma </Value>\r\n        </Item>\r\n        <Item>\r\n            <Key> 2 </Key>\r\n            <Value> Armud </Value>\r\n        </Item>\r\n        <Item>\r\n            <Key> 3 </Key>\r\n            <Value> Heyva </Value>\r\n        </Item>\r\n    </Items>\r\n</Dictionary>\r\n";
using (StringReader reader = new StringReader(xmlData))
{
    SerializableDictionary<int, string> deserializedFruits = (SerializableDictionary<int, string>)serializer.Deserialize(reader);
    fruits = deserializedFruits.ToDictionary();
}
#endregion
