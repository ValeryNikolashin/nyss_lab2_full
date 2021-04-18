using NYSS_Lab2.IRepository;
using NYSS_Lab2.Mapping;
using NYSS_Lab2.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

namespace NYSS_Lab2.Repository
{
    /// <summary>
    /// Репозиторий по работе с угрозами безопасности
    /// </summary>
    public class ThreatRepository : IThreatRepository
    {
        private string fileName;
        public ThreatRepository(string fileName)
        {
            this.fileName = fileName;
        }

        public IEnumerable<ShortThreat> GetAllShortThreats()
        {
            return Mapper.ThreatsToShortThreats(GetAllThreats());
        }

        public IEnumerable<Threat> GetAllThreats()
        {
            var formatter = new BinaryFormatter();

            using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                if (fs.Length == 0) return null;

                try
                {
                    return (IEnumerable<Threat>)formatter.Deserialize(fs);
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public Threat GetThreat(int threatId)
        {
            return GetAllThreats().FirstOrDefault(x => x.Id == threatId);
        }

        public void SaveThreats(IEnumerable<Threat> threats)
        {
            if (threats == null) return;

            var formatter = new BinaryFormatter();

            using (var fs = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, threats);
            }
        }
    }
}
