using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppTimer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            processesList();
        }
        public ArrayList listOfProcessess() //function originaly from AppTimer used to get all the processes and mark the important ones
        {
            Process[] allProcesses = Process.GetProcesses();
            ArrayList processesNameList = new ArrayList();
            for (int i = 0; i < allProcesses.Length; i++)
            {
                Process workingProcess = allProcesses[i];
                if (workingProcess.MainWindowTitle.Length > 0)
                    processesNameList.Add(workingProcess.ProcessName);
            }
            return processesNameList;
        }
        public void processesList()
        {
            boxOfProcesses.DataSource = listOfProcessess(); //connecting listbox with array of important processes
        }
        LinkedList<string> selectedProcesses = new LinkedList<string>();  //list for currently monitored processes
        private async void boxOfProcesses_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            string selectedProcess = boxOfProcesses.SelectedItem.ToString();
            if (selectedProcesses.Contains(selectedProcess)) //checking if process was already selected
            {
                MessageBox.Show("Ten proces jest już wybrany");
                return;
            }
            selectedProcesses.AddLast(selectedProcess);         //adding to list of monitored processes 

            MessageBox.Show("Timer for " + selectedProcess + " started");
            AppTimer timer1 = new AppTimer(selectedProcess);

            Task task = timer1.timeCounterAsync();      //creating task for timer
            await task;                                //awaiting for counter to finish
            if (task.IsCompleted == true)   //if counter finished, then show message
                timer1.sendToFile();
            selectedProcesses.Remove(selectedProcess);  //removing process that we ended
            MessageBox.Show("Timer for " + selectedProcess + " stopped");
        }

        private void button2_Click(object sender, EventArgs e) //refresh processes list in boxOfProcessess
        {
            processesList();
        }
    }

    class AppTimer
    {
        string processName = "";
        uint[] timeArr = new uint[2]; //chosen process timer
        DateTime startTime;
        DateTime endTime;
        public AppTimer(string processName) //contructor
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
            startTime = DateTime.Now;
            while (true)
            {
                if (isRunning() == false)
                    break;
                Thread.Sleep(1000);
            }
            endTime = DateTime.Now;
            TimeSpan timeSpan = endTime - startTime;
            uint[] timeCuntArr = {uint.Parse(timeSpan.Hours.ToString()),
                                  uint.Parse(timeSpan.Minutes.ToString()),
                                  uint.Parse(timeSpan.Seconds.ToString())};
            timeArr = timeCuntArr;
        }
        public async Task timeCounterAsync()
        {
            startTime = DateTime.Now;
            while (true)
            {
                if (isRunning() == false)
                    break;
                //Thread.Sleep(1000);
                await Task.Delay(1000);
            }
            endTime = DateTime.Now;
            TimeSpan timeSpan = endTime - startTime;
            uint[] timeCuntArr = {uint.Parse(timeSpan.Hours.ToString()),
                                  uint.Parse(timeSpan.Minutes.ToString()),
                                  uint.Parse(timeSpan.Seconds.ToString())};
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

        //async functions need to return Task, if they need to return other datatype u need to put it in <> --> Task<String>
        //c# does an exception for events
        //if function is async then put an async at the end of its name --> doSomethingAsync
        //static void Main(string[] args)
        //{
        //    ArrayList list1 = listOfProcessess();
        //    string userInput;

        //    userInput = chooseProcess(list1);
        //    AppTimer timer1 = new AppTimer(userInput);
        //    timer1.timeCounter();

        //    userInput = chooseProcess(list1);
        //    AppTimer timer2 = new AppTimer(userInput);
        //    timer2.timeCounter();

        //    timer1.sendToFile();

        //    timer1.sendToFile();
        //    timer2.sendToFile();
        //}

    }
}
