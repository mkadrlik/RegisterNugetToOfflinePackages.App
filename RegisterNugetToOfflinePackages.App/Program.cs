using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace RegisterNugetToOfflinePackages.App
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            string folderPath = args[0];

            List<string> nugetDirectories = Directory.GetDirectories(folderPath).ToList();

            foreach (string nugetDirectory in nugetDirectories)
            {
                List<string> nugetFile = Directory.GetFiles(nugetDirectory, "*.nupkg").ToList();
                StringBuilder processingLog = new StringBuilder();
                char[] buffer = null;

                try
                {
                    Process process = Process.Start(ConfigurationManager.AppSettings["NugetExePath"]);

                    StreamReader inputReader = new StreamReader(process.StandardInput.BaseStream);
                    int bufferCount = inputReader.ReadAsync(buffer, 0, 0).Result;
                    if (bufferCount == 0)
                    {
                        inputReader.Close();
                        _ = processingLog.Append(buffer).AppendLine();
                    }

                    StreamReader outputReader = new StreamReader(process.StandardOutput.BaseStream);
                    processingLog = outputReader.ReadToEnd();
                    reader.Close();

                    StreamReader errorReader = new StreamReader(process.StandardError.BaseStream);
                    processingLog = errorReader.ReadToEnd();
                    reader.Close();
                }
                catch (Exception)
                {
                }
            }
        }
    }
}
