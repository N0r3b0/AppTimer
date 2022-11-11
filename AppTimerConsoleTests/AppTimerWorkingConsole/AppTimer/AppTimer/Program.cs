namespace AppTimer
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Threading;
    using System.Collections;
    using System.Collections.Generic;

    class Timer
    {
        string processName = "";
        uint[] timeArr = new uint[2]; //chosen process timer

        static ArrayList processesNameList = new ArrayList(); //arraylist for processess names
        static Process[] allProcesses = Process.GetProcesses(); //gets all the running processess


        /*
         listOfProcesses contain only taskbar processess names.
         */
        public static ArrayList listOfProcessess()
        {
            for (int i = 0; i < allProcesses.Length; i++)
            {
                Process workingProcess = allProcesses[i];
                if (workingProcess.MainWindowTitle.Length > 0)
                    processesNameList.Add(workingProcess.ProcessName);
            }
            return processesNameList;
        }
        public Timer(string processName) //contructor
        {
            this.processName = processName;
        }

        /* 
        Method isRunning checks if indicated process is running by using method GetProcessByName (memory expensive method). 
        GetProcessByName returns array of processes connected to 'processName'.
        If this array is longer then 0 then process is running.
        */

        public bool isRunning()
        {
            if (Process.GetProcessesByName(processName).Length > 0)
                return true;
            else return false;
        }
        public static bool isRunning(string test) //overload for static usage.
        {
            if (Process.GetProcessesByName(test).Length > 0)
                return true;
            else return false;
        }

        /*
        Method appTimer increment 'seconds' variable by 1 each second passed.
        Thread.Sleep(1000) last exactly a second thus it;s known 1 sec's passed.
        */
        public void timeCounter()
        {
            uint seconds = 0;
            uint minutes = 0;
            uint hours = 0;
            while (true)
            {
                if (isRunning() == false)
                    break;
                Thread.Sleep(1000);
                seconds++;
                if (seconds == 60)
                {
                    seconds = 0;
                    minutes++;
                }
                if (minutes == 60)
                {
                    minutes = 0;
                    hours++;
                }
            }
            uint[] timeCuntArr = { hours, minutes, seconds };
            timeArr = timeCuntArr;
        }
        public string timeArrToString()
        {
            return "Application: " + processName + "\nHours: " + timeArr[0] + "\nMinutes: " + timeArr[1] + "\nSeconds: " + timeArr[2] + "\n";
        }

        public void sendToFile()
        {
            DateTime localDate = DateTime.Now;
            string date = localDate.ToString();
            string path = @"D:\Programowanie\Programowanie_git\1_MojeProjekty\AppTimer\" + processName + ".txt";
            File.AppendAllText(path, date + "\n" + timeArrToString());
        }
        public static string chooseProcess(ArrayList procList)
        {
            Console.WriteLine("Choose program to follow");
            for (int i = 0; i < procList.Count; i++)
                Console.WriteLine(procList[i].ToString());
            string userInput = Console.ReadLine();
            return userInput;
        }
        static void Main(string[] args)
        {
            ArrayList list1 = listOfProcessess();
            string userInput = chooseProcess(list1);
            Timer timer1 = new Timer(userInput);
            timer1.timeCounter();
            timer1.sendToFile();

            //string userInput2 = chooseProcess(list1);
            //Timer timer2 = new Timer(userInput2);

            //string userInput3 = chooseProcess(list1);
            //Timer timer3 = new Timer(userInput3);

            //timer2.timeCounter();
            // timer3.timeCounter();

            //timer2.sendToFile();
            // timer3.sendToFile();
        }

    }
}

