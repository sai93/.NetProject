/*

This is a test harness for a logger. You should point it at your own class when it's ready.. Adjust the Customization area w/ your own settings.

*/

namespace LoggerTest
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using DLLLibrary;

    class Program
    {
        #region Customization

        /// <summary>
        /// How many messages should each thread send before shutting down?
        /// </summary>
        private static int NUMBER_OF_MESSAGES_PER_THREAD = 10;
        //300
        /// <summary>
        /// How many threads should be sending messages concurrently?
        /// </summary>
        private static int NUMBER_OF_THREADS_SENDING_MESSAGES = 1;
        //8
        /// <summary>
        /// Minimum log level supported by your logger
        /// </summary>
        private static int MIN_LOG_LEVEL = 1;

        /// <summary>
        /// Maximum log level supported by your logger
        /// </summary>
        private static int MAX_LOG_LEVEL = 4;

        /// <summary>
        /// How often should an exception be sent into the logger - every X number of messages
        /// </summary>
        private static int HOW_OFTEN_AN_EXCEPTION = 1;
        //3
        #endregion

        static void Main(string[] args)
        {
            try
            {
               // Thread.Sleep(15000);
                DLL logger = new DLL(6);
                Thread.Sleep(100);
                List<Thread> threads = new List<Thread>();

                // Setup threads
                for (int i = 1; i <= NUMBER_OF_THREADS_SENDING_MESSAGES; i++)
                {
                    int threadNumber = i;

                    Thread newThread = new Thread(x =>
                    {
                        Random rand = new Random();

                        // string errorMessage = "Error Message, Thread number: {0}, Message Number: {1}, Log Level: {2}";
                        Exception exception = new Exception();
                        int logLevel;

                        for (int messageNumber = 1; messageNumber <= NUMBER_OF_MESSAGES_PER_THREAD; messageNumber++)
                        {
                            logLevel = rand.Next(MIN_LOG_LEVEL, MAX_LOG_LEVEL);
                            if (logLevel == 1)
                                 logger.debug(
                                (messageNumber % HOW_OFTEN_AN_EXCEPTION == 0) ? exception : null);
                            if (logLevel == 2)
                                logger.error(
                                    (messageNumber % HOW_OFTEN_AN_EXCEPTION == 0) ? exception : null);
                            if (logLevel == 3)
                                logger.warning(
                                    (messageNumber % HOW_OFTEN_AN_EXCEPTION == 0) ? exception : null);
                        }
                    });

                    threads.Add(newThread);
                }

                // Start threads
                foreach (Thread aThread in threads)
                {
                    aThread.Start();

                }

                // Join threads
                foreach (Thread aThread in threads)
                {
                    aThread.Join();
                }


                Console.WriteLine("All Threads Finished");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine("****************:Exception Occured in Logger Test:**********");
            }
        }
    }
}
