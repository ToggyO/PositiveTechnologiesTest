using System;
using System.Xml.Serialization;

namespace FileSearchEngine.Models
{
    [XmlRoot("soft")]
    public class Soft
    {
        [XmlElement("Name")]
        public string Name { get; set; }

        [XmlElement("ReleaseDate")]
        public DateTime ReleaseDate { get; set; }

        [XmlElement("Vendor")]
        public string Vendor { get; set; }

        [XmlElement("Version")]
        public string Version { get; set; }
        
        public override string ToString() => $"{Vendor}: {Name} ({Version})";
    }
}