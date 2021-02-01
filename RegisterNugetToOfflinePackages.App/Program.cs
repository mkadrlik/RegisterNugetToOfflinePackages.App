using RegisterNugetToOfflinePackages.App.Helpers;
using System;
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
            StringBuilder fullLog = new StringBuilder();

            foreach (string nugetDirectory in nugetDirectories)
            {
                List<string> nugetFile = Directory.GetFiles(nugetDirectory, "*.nupkg", SearchOption.AllDirectories).ToList();
                StringBuilder sb = new StringBuilder();

                try
                {
                    string nugetStore = Path.Combine(Environment.GetEnvironmentVariable("ProgramFiles(x86)"), ConfigurationManager.AppSettings["NuGetStore"]).AddQuotesAroundString();
                    string nugetExePath = string.Concat(Path.Combine(Environment.GetEnvironmentVariable("ProgramFiles(x86)"), ConfigurationManager.AppSettings["NuGetExePath"])).AddQuotesAroundString();
                    string arguments = string.Concat(" add ", nugetFile[0], " -Source ", nugetStore);

                    Process process = new Process
                    {
                        StartInfo = new ProcessStartInfo
                        {
                            Arguments = arguments,
                            CreateNoWindow = false,
                            FileName = nugetExePath,
                            WindowStyle = ProcessWindowStyle.Hidden,
                            RedirectStandardOutput = true,
                            RedirectStandardError = true,
                            RedirectStandardInput = true,
                            UseShellExecute = false,
                            Verb = "open"
                        }
                    };

                    using (process)
                    {
                        Console.WriteLine(process.StartInfo.FileName);

                        bool isRunning = process.Start();

                        if (isRunning)
                        {
                            process.WaitForExit();

                            _ = sb.Append(nugetFile[0].ToString()).AppendLine();
                            if (process.StandardInput.BaseStream.CanRead) _ = sb.Append(process.ExitCode).AppendLine();
                            if (process.StandardOutput.BaseStream.CanRead) _ = sb.Append(process.StandardOutput.ReadToEnd()).AppendLine();
                            if (process.StandardError.BaseStream.CanRead) _ = sb.Append(process.StandardError.ReadToEnd()).AppendLine();
                        }
                    }
                }
                catch (Exception ex)
                {
                    _ = sb.Append(ex.Message).AppendLine();
                    _ = sb.Append(ex.StackTrace).AppendLine();
                    while (ex.InnerException != null && !string.IsNullOrWhiteSpace(ex.InnerException.Message))
                    {
                        _ = sb.Append(ex.InnerException.Message).AppendLine();
                    }
                }

                _ = sb.AppendLine();
                _ = fullLog.Append(sb);
                Console.WriteLine(sb.ToString());
            }

            File.WriteAllText(string.Concat(Environment.CurrentDirectory, "ProcessingLog", "_", DateTime.Now.Ticks, ".txt"), fullLog.ToString());
        }
    }
}
