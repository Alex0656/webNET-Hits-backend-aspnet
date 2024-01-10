using System.Runtime.Serialization;
using System.Xml.Linq;

namespace lab1_backend.Models
{
    //[Serializable]
    public class MyPair //: ISerializable
    {
        public MyPair(string key, string value)
        {
            Key = key;
            Value = value;
        }
        public string Key;
        public string Value;

        public string ToString()
        {
            return Key + " : " + Value;
        }

        public static string ToJson(string header, List<MyPair> list)
        {
            string ret = header + "\n{\n";
            
            foreach (MyPair pair in list)
            {
                if(pair.Key.StartsWith("http:"))
                    continue;
                ret += "\t" + pair.Key + " : " + pair.Value + ",\n";
            }
            ret += "\n}";
            return ret;
        }
        /*
            public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
            {
                info.AddValue("Key", Key);
                info.AddValue("Value", Value);
            }
        */

    }
}
