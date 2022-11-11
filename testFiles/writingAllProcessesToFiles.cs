namespace AppTimer
{
    using System;
    using System.Diagnostics;
    using System.IO;

    class Timer
    {
        public static bool isWithin(string[] s1, string s2)
        {
            for(int i = 0; i < s1.Length; i++)
            {
                if (Equals(s2, s1[i]) == true)
                    return true;
            }
            return false;
        }
        static void Main(string[] args)
        {
            Process[] processes = Process.GetProcesses();
            string[] procesList = new string[processes.Length];
            string test = "";

            for (int i = 0; i < processes.Length; i++)
            {
                Process p = processes[i];
                if (isWithin(procesList, p.ProcessName) == false)
                {
                    procesList[i] = (p.ProcessName);
                }
            }
            foreach (Process p in processes)
            {
                if (!String.IsNullOrEmpty(p.MainWindowTitle))
                {
                    test += p.MainWindowTitle + "\n";
                }
            }


            File.WriteAllText(@"D:\Programowanie\Programowanie_git\1_MojeProjekty\AppTimer\test.txt", test);

            for (int i = 0; i < procesList.Length; i++)
            {
                if (string.IsNullOrEmpty(procesList[i]) == false)
                    File.AppendAllText(@"D:\Programowanie\Programowanie_git\1_MojeProjekty\AppTimer\procesList.txt", procesList[i] + "\n");
            }
        }

    }
}

