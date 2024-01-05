
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Text;
//using System.Text.Encodings.Web;
//using System.Text.Json;
//using System.Text.Json.Serialization;
//using System.Xml;
//using static System.Net.Mime.MediaTypeNames;

//namespace SolarConflict.XnaUtils
//{
//    public class SaveLoadManager 
//    {
//        public static JsonSerializerOptions MakeSerializerOptions()
//        {
//            var options = new JsonSerializerOptions();
//            options.PropertyNameCaseInsensitive = true;
//            options.Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
//            //options.ReferenceHandler = ReferenceHandler.Preserve;
//            //options.Encoder =             
//            options.WriteIndented = true;
//            options.IncludeFields = true;
//            options.IgnoreReadOnlyProperties = true;
//            options.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
//            options.Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
//            options.AllowTrailingCommas = true;            
//            options.Converters.Add(new JsonStringEnumConverter());
//            return options;
//        }

//        private JsonSerializerOptions jsonSerializerOptions;      
//        private SaveLoadManager()
//        {
//            jsonSerializerOptions = MakeSerializerOptions();
//        }

//        public string Serialize(object obj)
//        {
//            return JsonSerializer.Serialize(obj, jsonSerializerOptions);
//        }

//        public T Deserialize<T>(string json)
//        {
//            return JsonSerializer.Deserialize<T>(json, jsonSerializerOptions);
//        }

//        public void Save(string directory, string fileName, object o)
//        {
//            Directory.CreateDirectory(directory);
//            Save(Path.Combine(directory, fileName), o);       
//        }

//        public void Save(string path, object o)
//        {            
//            string jsonString = JsonSerializer.Serialize(o, jsonSerializerOptions);
//            System.IO.File.WriteAllText(path, jsonString);
//        }

//        public T Load<T>(string path)
//        {
//            var text = File.ReadAllText(path);            
//            return JsonSerializer.Deserialize<T>(text, jsonSerializerOptions);            
//        }

//        private static SaveLoadManager _instance;
//        public static SaveLoadManager Inst
//        {
//            get
//            {
//                if (_instance == null)
//                    _instance = new SaveLoadManager();
//                return _instance;
//            }
//        }
//    }
//}
