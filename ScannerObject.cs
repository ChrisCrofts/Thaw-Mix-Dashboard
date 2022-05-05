using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Newtonsoft.Json;


namespace Thaw_Mix_Dashboard
{

    // tutorial for serializationhttps://youtu.be/jbwjbbc5PjI?t=584
    //[Serializable()]
    //public class ScannerObject : ISerializable
    //{
    //    public string scannerColor { get; set; }

    //    public ScannerObject() { }

    //    public ScannerObject (string color)
    //    {
    //        scannerColor = color;
    //    }

    //    public override string ToString()
    //    {
    //        return string.Format("Scanner color: {0}", scannerColor);
    //    }

    //    public void GetObjectData(SerializationInfo info, StreamingContext context)
    //    {
    //        info.AddValue("Scanner Color", scannerColor);
    //    }

    //    public ScannerObject(SerializationInfo info, StreamingContext context)
    //    {
    //        //scannerColor = (Color)info.GetValue("Scanner Color", typeof(Color));
    //        scannerColor = (string)info.GetValue("Scanner Color", typeof(string));
    //    }
    //}

    public class ScannerObject
    {
        public string Name { get; set; }
        public string Color { get; set; }
        public string Port { get; set; }

        public ScannerObject(string json)
        {
            var jsonConvert = JsonConvert.DeserializeObject<dynamic>(json);
            Name = jsonConvert.Name;
            Color = jsonConvert.Color;
            Port = jsonConvert.Port;
        }
    }
}
