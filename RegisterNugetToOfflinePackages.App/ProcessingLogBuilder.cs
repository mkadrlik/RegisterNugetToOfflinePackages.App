using System.Collections.Generic;
using System.IO;
using System.Text;

namespace RegisterNugetToOfflinePackages.App
{
    public static class ProcessingLogBuilder
    {
        public static string BuildProcessingLogFile(List<StreamReader> logData)
        {
            StringBuilder processingLog = new StringBuilder();

            foreach (StreamReader data in logData)
            {
                using (StreamReader reader = data)
                {
                    processingLog = processingLog.Append(reader.ReadToEnd()).AppendLine();
                }
            }

            return processingLog.ToString();
        }
    }
}
