using NYSS_Lab2.Models;
using System.Collections.Generic;

namespace NYSS_Lab2.IRepository
{
    /// <summary>
    /// Интерфейс репозитория угроз информационной безопасности
    /// </summary>
    public interface IThreatRepository
    {
        /// <summary>
        /// Возвращает список записей угроз из локальной базы
        /// </summary>
        /// <returns>Список локальных угроз</returns>
        IEnumerable<Threat> GetAllThreats();

        /// <summary>
        /// Возвращает список записей угроз из локальной базы
        /// </summary>
        /// <returns>Список локальных угроз с сокращённой информацией</returns>
        IEnumerable<ShortThreat> GetAllShortThreats();

        /// <summary>
        /// Возвращает угрозу
        /// </summary>
        /// <param name="threatId">Идентификатор угрозы</param>
        /// <returns>Угроза безопасности информации</returns>
        Threat GetThreat(int threatId);

        /// <summary>
        /// Сохраняет список угроз в локальную базу
        /// </summary>
        /// <param name="threats">Список угроз безопасности</param>
        void SaveThreats(IEnumerable<Threat> threats);
    }
}
