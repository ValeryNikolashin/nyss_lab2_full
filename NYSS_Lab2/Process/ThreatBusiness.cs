using NYSS_Lab2.IContracts;
using NYSS_Lab2.IRepository;
using NYSS_Lab2.Models;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;

namespace NYSS_Lab2.Process
{
    /// <summary>
    /// Логика работы с угрозами безопасности
    /// </summary>
    public class ThreatBusiness : IThreatBusiness
    {
        //private const string sourceFileName = "thrlist.xlsx";
        //private const string sourceFileUrl = "https://bdu.fstec.ru/files/documents/thrlist.xlsx";
        //private const string localStorageFileName = "thrlist.dat";
        private readonly string sourceFileName;
        private readonly string sourceFileUrl;
        private readonly IThreatRepository threatRepository;

        public ThreatBusiness(string sourceFileName, string sourceFileUrl, IThreatRepository threatRepository)
        {
            this.sourceFileName = sourceFileName;
            this.sourceFileUrl = sourceFileUrl;
            this.threatRepository = threatRepository;
        }

        public UpdateReport AutomationUpdate()
        {
            //TODO: доделать успешность обновления
            var report = new UpdateReport() { IsSuccessed = true };

            using (var client = new WebClient())
            {
                try
                {
                    client.DownloadFile(sourceFileUrl, sourceFileName);
                }
                catch (ArgumentNullException)
                {
                    report.IsSuccessed = false;
                    report.ErrorMessage = "Отсутствует Url адреса для скачивания файла.";
                    return report;
                }
                catch (WebException)
                {
                    report.IsSuccessed = false;
                    report.ErrorMessage = "Возможные проблемы: отсутствие интернета, некорректный Url адрес для скачивания файла, файл для сохранения уже существует и используется другой программой.";
                    return report;
                }
                catch (Exception ex)
                {
                    report.IsSuccessed = false;
                    report.ErrorMessage = ex.Message;
                    return report;
                }
            }

            var oldThreatsAsEnumerable = threatRepository.GetAllThreats();

            List<Threat> oldThreats = oldThreatsAsEnumerable!= null? new List<Threat>(oldThreatsAsEnumerable):null;

            var newThreats = new List<Threat>();

            ExcelPackage package;
            try
            {
                package = new ExcelPackage(new FileInfo(sourceFileName));
            }
            catch (Exception)
            {
                report.IsSuccessed = false;
                report.ErrorMessage = "Загруженный файл повреждён или имеет неверный формат.";
                return report;
            }
            var sheet = package.Workbook.Worksheets.FirstOrDefault();
            int row = 3;
            while (true)
            {
                var cells = sheet.Cells;

                if (cells[row, 1].Value == null) break;

                Threat threat;

                try
                {
                    var id = int.Parse(cells[row, 1].Value.ToString());
                    var name = cells[row, 2].Value.ToString();
                    var description = cells[row, 3].Value.ToString();
                    var source = cells[row, 4].Value.ToString();
                    var influenceObject = cells[row, 5].Value.ToString();
                    var confidentialityViolation = int.Parse(cells[row, 6].Value.ToString()) == 1;
                    var integrityViolation = int.Parse(cells[row, 7].Value.ToString()) == 1;
                    var accessibilityViolation = int.Parse(cells[row, 8].Value.ToString()) == 1;

                    threat = new Threat()
                    {
                        Id = id,
                        Name = name,
                        Description = description,
                        Source = source,
                        InfluenceObject = influenceObject,
                        ConfidentialityViolation = confidentialityViolation,
                        IntegrityViolation = integrityViolation,
                        AccessibilityViolation = accessibilityViolation
                    };
                }
                catch (Exception)
                {
                    report.IsSuccessed = false;
                    report.ErrorMessage = "Загруженный файл повреждён или имеет неверный формат.";
                    return report;
                }

                if (oldThreats != null && row - 3 < oldThreats.Count)
                {
                    var index = row - 3;
                    if (index < oldThreats.Count && !oldThreats[index].Equals(threat)) report.Logs.Add(new ModifiedThreatLog() { NewThreat = threat.ToString(), OldThreat = oldThreats[index].ToString(), ThreatId = threat.Id });
                }
                else
                {
                    report.Logs.Add(new ModifiedThreatLog() { NewThreat = threat.ToString(), OldThreat = null, ThreatId = threat.Id });
                }

                newThreats.Add(threat);

                row++;
            }

            if(oldThreats != null && row-3 < oldThreats.Count)
            {
                for (int i = row-3; i < oldThreats.Count; i++)
                {
                    report.Logs.Add(new ModifiedThreatLog() { NewThreat = null, OldThreat = oldThreats[i].ToString(), ThreatId = oldThreats[i].Id });
                }
            }

            SaveThreats(newThreats);

            return report;
        }

        public IEnumerable<ShortThreat> GetAllShortThreats()
        {
            return threatRepository.GetAllShortThreats();
        }

        public Threat GetThreat(int threatId)
        {
            return threatRepository.GetThreat(threatId);
        }

        public void SaveThreats(IEnumerable<Threat> threats)
        {
            threatRepository.SaveThreats(threats);
        }
    }
}
