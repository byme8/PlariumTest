using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Assets.Code.Seralization
{
    public enum GameEndReason
    {
        MummyDeath,
        ZombieDeath,
        Quit
    }

    public class Record
    {
        [XmlAttribute("Name")]
        public string Name
        {
            get;
            set;
        }

        [XmlAttribute("Coins")]
        public int Coins
        {
            get;
            set;
        }

        [XmlAttribute("Time")]
        public float Time
        {
            get;
            set;
        }

        [XmlAttribute("LaunchTime")]
        public DateTime LaunchTime
        {
            get;
            set;
        }

        [XmlAttribute("Reason")]
        public GameEndReason Reason
        {
            get;
            set;
        }
    }
}
