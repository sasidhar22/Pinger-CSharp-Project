using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PingerPractice
{
    internal class PingerClass
    {
        private string InputFilePath;
        private int TotalIps;
        private long IpCount;
        
        public PingerClass(string inputFilePath)
        {
            InputFilePath = inputFilePath;
        }


        public void IpsCount()
        {
            string firstLine = File.ReadLines(InputFilePath).First();
            TotalIps = Int16.Parse(firstLine);
        }


        public void CheckIpCount()
        {
            
            #region Count the total number of IPs present
            var lineCount = 0;
            string line = string.Empty;
            using (var readerlines = File.OpenText(InputFilePath))
            {
                while ((line = readerlines.ReadLine()) != null)
                {
                    if (!line.Equals(string.Empty))
                    {
                        lineCount++;
                    }
                }
            }

            //In the below line of code, we substract lineCount by 1.
            //because, we don't want to include the first line where it says
            //the total ips present in the addresses1.txt file
            IpCount = lineCount - 1;
            #endregion

            //Exception case 3 : Check the file format
            #region Check if the total count mentioned in the first line of the file is equal to total number of IPs present in the file.
            try
            {
                if (TotalIps != IpCount)
                {
                    throw new PingerFileFormatException("The file does not have as many IPs as mentioned in the first line of the file");
                }
            }
            #endregion
        
        }


        public void Process()
        {
            //Exception case 1 : Check if the file exists
            #region Tries to read the file if exists and throws an exception if the file doesn't exist
            try
            {
                using (StreamReader reader = new StreamReader(InputFilePath))
                {
                    reader.ReadToEnd();
                }
            }
            catch (FileNotFoundException ErrorMessage)
            {
                // Write error.
                Console.WriteLine(ErrorMessage);
                return;
            }
            #endregion


            //Exception case 2 : Check the file format
            #region Check if the first line of the file contains an integer and throw an exception if not.

            #endregion


            IpsCount();

            CheckIpCount();
        }

    }
}



