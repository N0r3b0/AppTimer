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
    public static string appTimer(string processName)
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
            if(seconds == 60)
            {
                seconds = 0;
                minutes++;
            }
            if(minutes == 60)
            {
                minutes = 0;
                hours++;
            }
        }
        return "Time " + hours + ":" + minutes + ":" + seconds;
    }

    static void Main(string[] args)
    {
        System.Console.WriteLine(appTimer("AppTimer"));
    }

}