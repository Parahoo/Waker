using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Waker
{
    [Serializable]
    public class UserSetting
    {
        public List<AlarmJob> AlarmJobs { get; set; } = new List<AlarmJob>();

        private const string settingFilename = "usersetting.xml";
        public void Save()
        {
            using (StreamWriter fs = new StreamWriter(settingFilename, false, Encoding.UTF8))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(UserSetting));
                xmlSerializer.Serialize(fs, this);
            }
        }

        public static bool Load(out UserSetting config)
        {
            try
            {
                using (StreamReader fs = new StreamReader(settingFilename, Encoding.UTF8))
                {
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(UserSetting));
                    config = xmlSerializer.Deserialize(fs) as UserSetting;
                    return true;
                }
            }
            catch
            {
                config = new UserSetting();
                return false;
            }

        }
    }
}
