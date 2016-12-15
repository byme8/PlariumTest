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
        private const string FileName = "Records.txt";

        public UserRepository()
        {
            var serializer = new XmlSerializer(typeof(User[]));
            using (var stream = new FileStream(FileName, FileMode.OpenOrCreate))
            {
                if (stream.Length == 0)
                {
                    serializer.Serialize(stream, new User[0]);
                    stream.Seek(0, SeekOrigin.Begin);
                }
                
                var users = serializer.Deserialize(stream) as User[];
                this.Users = users.ToList();
            }
        }

        public List<User> Users
        {
            get;
            set;
        }

        public void Dispose()
        {
            var serializer = new XmlSerializer(typeof(User[]));
            using (var stream = new FileStream(FileName, FileMode.Create))
            {
                serializer.Serialize(stream, this.Users.ToArray());
            }
        }
    }
}
