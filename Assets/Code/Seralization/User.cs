using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Assets.Code.Seralization
{
    [Serializable]
    public class User
    {
        [XmlAttribute("Name")]
        public string Name
        {
            get;
            set;
        }
    }
}
