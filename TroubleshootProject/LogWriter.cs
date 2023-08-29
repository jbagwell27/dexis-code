using System;
using System.IO;

namespace TroubleshootProject
{
    class LogWriter
    {
        private readonly DateTime CurrentDate;
        private readonly string CurrentTime;
        private readonly DateTime ThirtyDaysAgo;
        private readonly string CurrentDaysFile;
        private readonly string ThirtyDayOldFile;


        public LogWriter()
        {
            CurrentDate = DateTime.Now.Date;
            CurrentTime = DateTime.Now.TimeOfDay.ToString();
            ThirtyDaysAgo = CurrentDate.AddDays(-30);

            CurrentDaysFile = "notehistory" + CurrentDate.ToString("yyyy.MM.dd") + ".log";
            ThirtyDayOldFile = "notehistory" + ThirtyDaysAgo.ToString("yyyy.MM.dd") + ".log";

            GenerateLogFile();


        }

        public void GenerateLogFile()
        {
            if (!File.Exists(CurrentDaysFile))
            {
                File.Create(CurrentDaysFile);
            }
            if (File.Exists(ThirtyDayOldFile))
            {
                File.Delete(ThirtyDayOldFile);
            }
        }

        public void AppendLog(string notes)
        {
            string result = CurrentTime + "\r\n" + notes + "\r\n" +
                "--------------------\r\n\r\n";

            File.AppendAllText(CurrentDaysFile, result);
        }
    }
}
