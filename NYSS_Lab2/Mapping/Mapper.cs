using NYSS_Lab2.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace NYSS_Lab2.Mapping
{
    /// <summary>
    /// Класс для маппинга
    /// </summary>
    public static class Mapper
    {
        /// <summary>
        /// Маппит Threat в ShortThreat
        /// </summary>
        /// <param name="threat">Сущность угроза информационной безопасности</param>
        /// <returns>ShortThreat</returns>
        public static ShortThreat ThreatToShortThreat(Threat threat)
        {
            return new ShortThreat { Id = threat.Id, IdAsString= $"УБИ.{threat.Id}", Name = threat.Name };
        }

        /// <summary>
        /// Маппит список Threat в список ShortThreat
        /// </summary>
        /// <param name="threats">Список сущностей угроза информационной безопасности</param>
        /// <returns>Список ShortThreat</returns>
        public static IEnumerable<ShortThreat> ThreatsToShortThreats(IEnumerable<Threat> threats)
        {
            if (threats == null) return null;

            var shorThreats = new List<ShortThreat>();
            foreach (var threat in threats)
            {
                shorThreats.Add(ThreatToShortThreat(threat));
            }

            return shorThreats;
        }
    }
}
