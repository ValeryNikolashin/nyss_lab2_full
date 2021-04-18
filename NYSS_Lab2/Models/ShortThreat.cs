namespace NYSS_Lab2.Models
{
    /// <summary>
    /// Угроза информационной безопасности (сокращённое представление)
    /// </summary>
    public class ShortThreat
    {
        /// <summary>
        /// Идентификатор угрозы
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Строковое представление идентификатора угрозы
        /// </summary>
        public string IdAsString { get; set; }

        /// <summary>
        /// Наименование угрозы
        /// </summary>
        public string Name { get; set; }
    }
}
