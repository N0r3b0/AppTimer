using System.Diagnostics;
class Timer
{
    string processName = "";
    uint[] timeArr = new uint[2];
    /*public Timer(string processName) //contructor
    {
        this.processName = processName;
    }*/

    /* 
    Method isRunning checks if indicated process is running by using method GetProcessByName (memory expensive method). 
    GetProcessByName returns array of processes connected to 'processName'.
    If this array is longer then 0 then process is running. (GetProcessByName is memory expensive) might be replaced
    */

    public bool isRunning()
    {
        if (Process.GetProcessesByName(processName).Length > 0)
            return true;
        else return false;
    }

    /*
    Method appTimer increment 'seconds' variable by 1 each second passed.
    Thread.Sleep(1000) last exactly a second thus it;s known 1 sec's passed.
    */
    public uint[] timeCounter()
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
        return "Application: " + processName + "\nHours: " + timeArr[0] + "\nMinutes: " + timeArr[1] + "\nSeconds: " + timeArr[2];
    }

    public void sendToFile(uint[] timeArr)
    {
        File.WriteAllText("./time.txt", timeArrToString(timeArr));
    }

    static void Main(string[] args)
    {
        Timer first = new Timer();
        first.processName = "Calculator";
        first.timeArr = first.timeCounter();
        //first.sendToFile(first.timeArr);
    }

}