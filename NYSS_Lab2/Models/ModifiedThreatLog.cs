namespace NYSS_Lab2.Models
{
    /// <summary>
    /// Лог обновления записи в локальной базе
    /// </summary>
    public class ModifiedThreatLog
    {
        /// <summary>
        /// Id записи угрозы, которая изменилась
        /// </summary>
        public int ThreatId { get; set; }

        /// <summary>
        /// Старое значение записи
        /// </summary>
        public string OldThreat { get; set; }

        /// <summary>
        /// Новое значение записи
        /// </summary>
        public string NewThreat { get; set; }
    }
}
