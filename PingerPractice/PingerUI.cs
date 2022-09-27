using System;

namespace PingerPractice
{
    internal class PingerUI
    {
        static void Main(string[] args)
        {
            //storing the address of the file
            string filePath = @"C:\folder\addresses1.txt";

            // creating an instance of the PingerClass
            PingerClass pingerClass = new PingerClass(filePath);

            // starting the process to read the file in the given path
            pingerClass.Process();
        }
    }
}



