using System;
using System.Threading;

class Program
{
    // Define a simple object to hold status
    class Status
    {
        public bool ExitRequested { get; set; } = false;
    }

    static void Main(string[] args)
    {
        Status status = new Status();

        // Start the data processing thread
        Thread processingThread = new Thread(() => ProcessData(status));
        processingThread.Start();

        // Start the keyboard listening thread
        Thread inputThread = new Thread(() => ListenForKeyPress(status));
        inputThread.Start();

        // Wait for both threads to finish
        processingThread.Join();
        inputThread.Join();
    }

    static void ProcessData(Status status)
    {
        // Simulate ongoing data processing
        while (!status.ExitRequested)
        {
            Console.WriteLine("Processing data...");
            Thread.Sleep(1000); // Simulate work by sleeping for 1 second
        }
        Console.WriteLine("Exiting data processing.");
    }

    static void ListenForKeyPress(Status status)
    {
        Console.WriteLine("Press 'E' to exit processing...");
        while (!status.ExitRequested)
        {
            if (Console.KeyAvailable)
            {
                var key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.E)
                {
                    Console.WriteLine("'E' key pressed. Updating status to request exit.");
                    status.ExitRequested = true;
                }
            }
        }
    }
}
