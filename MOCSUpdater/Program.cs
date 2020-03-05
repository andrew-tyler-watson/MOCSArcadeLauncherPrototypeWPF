using System;
using System.Diagnostics;


namespace MOCSUpdater
{
    public class Program
    {
        static void Main(string[] args)
        {
            Exec_PythonProcess();
        }

        static void Exec_PythonProcess()
        {
            var psi = new ProcessStartInfo();
            // File path of the python interpreter installation
            psi.FileName = "C:\\Users\\Work\\AppData\\Local\\Programs\\Python\\Python38\\python.exe";

            // File path of python script to be called
            var script = @"C:\\Users\\Work\\PycharmProjects\\TestProject\\TestProcess.py";

            psi.Arguments = $"\"{script}\"";

            psi.UseShellExecute = false;
            psi.CreateNoWindow = true;
            psi.RedirectStandardOutput = true;
            psi.RedirectStandardError = true;

            var errors = "";
            var results = "";

            // Get errors and output from the python script to be printed by the console
            using (var process = Process.Start(psi))
            {
                errors = process.StandardError.ReadToEnd();
                results = process.StandardOutput.ReadToEnd();
            }

            // Print out results and errors
            Console.WriteLine("ERRORS:");
            Console.WriteLine(errors);
            Console.WriteLine();
            Console.WriteLine("Results:");
            Console.WriteLine(results);
        }
    }
}
