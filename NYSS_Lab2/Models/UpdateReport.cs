using System.Collections.Generic;

namespace NYSS_Lab2.Models
{
    /// <summary>
    /// Отчёт об обновлении локальной базы угроз
    /// </summary>
    public class UpdateReport
    {
        /// <summary>
        /// Успешности обновления
        /// </summary>
        public bool IsSuccessed { get; set; }

        /// <summary>
        /// Список изменений
        /// </summary>
        public List<ModifiedThreatLog> Logs { get; set; } = new List<ModifiedThreatLog>();

        /// <summary>
        /// Сообщение об ошибке в процессе обновления
        /// </summary>
        public string ErrorMessage { get; set; }
    }
}
