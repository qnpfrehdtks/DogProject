using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml;
using System.Xml.Serialization; // XmlSerializer

public class XMLLoadManger : Singleton<XMLLoadManger>
{

    protected override bool Initialize()
    {
        return true;
    }

    // class -> XML 형식으로 변환해주는 함수
    public string XmlSerialize<T>( T obj) where T : class
    {
        if (obj == null) throw new System.ArgumentNullException("obj");
        var serializer = new XmlSerializer(typeof(T));
        using (var writer = new StringWriter())
        {
            serializer.Serialize(writer, obj);
            return writer.ToString();
        }
    }

    // xml string -> Class로 변환하는 함ㅁ수
    public T XmlDeserialize<T>( string xml) where T : class
    {
        if (xml == null) throw new System.ArgumentNullException("xml");
        var serializer = new XmlSerializer(typeof(T));
        using (var reader = new StringReader(xml))
        {
            try { return (T)serializer.Deserialize(reader); }
            catch { return null; } // Could not be deserialized to this type.
        }

    }

}
