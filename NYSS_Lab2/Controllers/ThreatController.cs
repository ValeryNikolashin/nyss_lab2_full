using NYSS_Lab2.IContracts;
using NYSS_Lab2.Models;
using System.Collections.Generic;

namespace NYSS_Lab2.Controllers
{
    /// <summary>
    /// Контроллер угроз информационной безопасности
    /// </summary>
    public class ThreatController
    {
        private readonly IThreatBusiness threadBusiness;

        public ThreatController(IThreatBusiness threadBusiness)
        {
            this.threadBusiness = threadBusiness;
        }

        /// <summary>
        /// Обновляет локальную базу
        /// </summary>
        /// <returns>Отчёт об обновлении</returns>
        public UpdateReport AutomationUpdate()
        {
            return threadBusiness.AutomationUpdate();
        }

        /// <summary>
        /// Возвращает список записей угроз из локальной базы
        /// </summary>
        /// <returns>Список локальных угроз с сокращённой информацией</returns>
        public IEnumerable<ShortThreat> GetAllShortThreats()
        {
            return threadBusiness.GetAllShortThreats();
        }

        /// <summary>
        /// Возвращает угрозу
        /// </summary>
        /// <param name="threatId">Идентификатор угрозы</param>
        /// <returns>Угроза безопасности информации</returns>
        public Threat GetThreat(int threatId)
        {
            return threadBusiness.GetThreat(threatId);
        }

        /// <summary>
        /// Сохраняет список угроз в локальную базу
        /// </summary>
        /// <param name="threats">Список угроз безопасности</param>
        public void SaveThreats(IEnumerable<Threat> threats)
        {
            threadBusiness.SaveThreats(threats);
        }
    }
}
