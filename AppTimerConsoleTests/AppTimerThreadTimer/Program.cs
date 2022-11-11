using System;
using System.Diagnostics;
class Timer
{
    /* 
    Method isRunning checks if indicated process is running by using method GetProcessByName (memory expensive method). 
    GetProcessByName returns array of processes connected to 'processName'.
    If this array is longer then 0 then process is running. (GetProcessByName is memory expensive) might be replaced
    */

    public static bool isRunning(string processName)
    {
        if (Process.GetProcessesByName(processName).Length > 0)
            return true;
        else return false;
    }

    /*
    Method appTimer increment 'seconds' variable by 1 each second passed.
    Thread.Sleep(1000) last exactly a second thus it;s known 1 sec's passed.
    */
    public static uint[] appTimer(string processName)
    {
        uint seconds = 0;
        uint minutes = 0;
        uint hours = 0;
        while (true)
        {
            if (isRunning(processName) == false)
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
        uint[] timeArr = { hours, minutes, seconds };
        return timeArr;
    }
    public static string timeArrToString(uint[] timeArr)
    {
        return "Application: ";
    }

    public void sendToFile(uint[] timeArr)
    {
        File.WriteAllText("./time.txt", timeArrToString(timeArr));
    }

    static void Main(string[] args)
    {
        /*uint[] time = new uint[2];
        time = appTimer("Calculator");
        for (int i = 0; i < time.Length; i++)
        {
            System.Console.WriteLine(time[i]);
        }*/
        Process[] processes = Process.GetProcesses();
        string[] procesList = new string[processes.Length];
        int i = 0;
        foreach (Process p in processes)
        {
            if (!procesList[i].Equals(p.ProcessName))
                procesList[i] = (p.ProcessName) + " ";
            i++;
        }
        for (int j = 0; j < procesList.Length; j++)
        System.Console.WriteLine(procesList[j]);
            
            //File.AppendText("./time.txt" + procesList[j]);
    }

}