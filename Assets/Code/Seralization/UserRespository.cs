using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Assets.Code.Seralization
{
    public class UserRepository : IDisposable
    {
        private const string RecordsFileName = "Records.txt";
        private const string SettingsFileName = "Settings.txt";

        public UserRepository()
        {
            var serializer = new XmlSerializer(typeof(Record[]));
            using (var stream = new FileStream(RecordsFileName, FileMode.OpenOrCreate))
            {
                if (stream.Length == 0)
                {
                    serializer.Serialize(stream, new Record[0]);
                    stream.Seek(0, SeekOrigin.Begin);
                }
                
                var users = serializer.Deserialize(stream) as Record[];
                this.Records = users.ToList();
            }

            serializer = new XmlSerializer(typeof(User));
            using (var stream = new FileStream(SettingsFileName, FileMode.OpenOrCreate))
            {
                if (stream.Length == 0)
                {
                    serializer.Serialize(stream, new User { Name = "UserName"});
                    stream.Seek(0, SeekOrigin.Begin);
                }

                this.User = serializer.Deserialize(stream) as User; 
            }
        }

        public List<Record> Records
        {
            get;
            set;
        }

        public User User;

        public void Dispose()
        {
            var serializer = new XmlSerializer(typeof(Record[]));
            using (var stream = new FileStream(RecordsFileName, FileMode.Create))
            {
                serializer.Serialize(stream, this.Records.ToArray());
            }

            serializer = new XmlSerializer(typeof(User));
            using (var stream = new FileStream(SettingsFileName, FileMode.Create))
            {
                serializer.Serialize(stream, this.User);
            }
        }
    }
}
