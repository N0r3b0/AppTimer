using System.Diagnostics;

class Timer
{

    public static bool isRunning(string processName)
    {
        if (Process.GetProcessesByName(processName).Length > 0)
            return true;
        else return false;
    }

    public static string appTimer(string processName)
    {
        Stopwatch stopWatch = new Stopwatch();
        stopWatch.Start();
        while (true)
            {
                if (isRunning(processName) == false)
                    break;
                Thread.Sleep(1000);
            }

        stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;

        string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}", ts.Hours, ts.Minutes, ts.Seconds);
        //Console.WriteLine("RunTime " + elapsedTime);
        return "RunTime " + elapsedTime;
    }



    static void Main(string[] args)
    {
        string processName = Console.ReadLine();
        if (isRunning(processName) == false) System.Console.WriteLine("Wrong process name or process are not runnig");
        string time = Timer.appTimer(processName);
        System.Console.WriteLine(time);
    }
}