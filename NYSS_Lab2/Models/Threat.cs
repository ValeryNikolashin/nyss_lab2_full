using System;

namespace NYSS_Lab2.Models
{
    /// <summary>
    /// Угроза информационной безопасности
    /// </summary>
    [Serializable]
    public class Threat
    {
        /// <summary>
        /// Идентификатор угрозы
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Наименование угрозы
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Описание угрозы
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Источник угрозы
        /// </summary>
        public string Source { get; set; }

        /// <summary>
        /// Объект воздействия угрозы
        /// </summary>
        public string InfluenceObject { get; set; }

        /// <summary>
        /// Нарушение конфиденциальности
        /// </summary>
        public bool ConfidentialityViolation { get; set; }

        /// <summary>
        /// Нарушение целостности
        /// </summary>
        public bool IntegrityViolation { get; set; }

        /// <summary>
        /// Нарушение доступности
        /// </summary>
        public bool AccessibilityViolation { get; set; }

        private bool Equals(Threat threat) => Id == threat.Id && Name == threat.Name 
            && Description == threat.Description && Source == threat.Source && InfluenceObject == threat.InfluenceObject 
            && ConfidentialityViolation == threat.ConfidentialityViolation && IntegrityViolation == threat.IntegrityViolation 
            && AccessibilityViolation == threat.AccessibilityViolation;

        public override bool Equals(object obj)
        {
            return obj is Threat threat && Equals(threat);
        }

        public override int GetHashCode() => (Id, Name, Description, Source, InfluenceObject, ConfidentialityViolation, IntegrityViolation, AccessibilityViolation).GetHashCode();

        public override string ToString()
        {
            return $"Id = {Id}{Environment.NewLine}" +
                    $"Name = {Name}{Environment.NewLine}" +
                    $"Description = {Description}{Environment.NewLine}" +
                    $"Source = {Source}{Environment.NewLine}" +
                    $"InfluenceObject = {InfluenceObject}{Environment.NewLine}" +
                    $"ConfidentialityViolation = {ConfidentialityViolation}{Environment.NewLine}" +
                    $"IntegrityViolation = {IntegrityViolation}{Environment.NewLine}" +
                    $"AccessibilityViolation = {AccessibilityViolation}{Environment.NewLine}";


        }
    }
}
