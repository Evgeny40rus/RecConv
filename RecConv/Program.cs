using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RecConv
{
    internal class Program
    {
        static void Main(string[] args)
        {
            for (int i = 0; i < args.Length; i++)
            {
                Console.WriteLine(args[i]); ;
            }
            var jsonString = File.ReadAllText("json.txt");
            var deserializedObject = JsonSerializer.Deserialize<Contacts>(jsonString); ;
            new JsonToCsvConverter(deserializedObject.AllContacts).Convert();
            Console.WriteLine("Конвертация завершена.\nНажмите любую клавишу.");

            Console.ReadKey();
        }
    }
    public class Contact
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("phone")]
        public string Phone { get; set; }
    }
    public class Contacts
    {
        [JsonPropertyName("contacts")]
        public List<Contact> AllContacts { get; set; }
    }
    public class JsonToCsvConverter
    {
        private readonly List<Contact> _allContacts;
        public JsonToCsvConverter(List<Contact> AllContacts)
        {
            _allContacts = AllContacts;
        }

       
        public void Convert(string path = "", string outPutFileName = "contacts", string delimiter = ";")
        {
            using (StreamWriter sw = File.CreateText($"{path}{outPutFileName}.csv"))
            {
                sw.WriteLine($"Name{delimiter}Phone");

                foreach (var contact in _allContacts)
                {
                    sw.WriteLine($"{contact.Name}{delimiter}{contact.Phone}");
                }
            }
        }
    }
}
