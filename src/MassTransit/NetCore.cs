namespace System.Net.Mime
{
    public class ContentType
    {
        public ContentType(string mediaType)
        {
            MediaType = mediaType;
        }

        public string MediaType { get; private set; }
    }
}

namespace System
{
    public class SerializableAttribute : Attribute { }
}

namespace System.Runtime.Serialization
{
    public interface ISerializable { }
}

// "net452": {
//       "frameworkAssemblies": {
//         "System.Xml": "4.0.0.0",
//         "System.Xml.Linq": "4.0.0.0",
//         "System.Linq": "4.0.0.0",
//         "System.Transactions": "4.0.0.0",
//         "System.Runtime.Caching": "4.0.0.0"
//       }
//      },