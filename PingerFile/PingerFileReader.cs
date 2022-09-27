using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ExceptionClasses;

namespace PingerFile
{
    /// <summary>
    /// Class <c>PingerFileReader</c> implements all the methods that reads/checks the file data.
    /// </summary>
    public class PingerFileReader
    {
        private string _inputFilePath;
        private FileStream _fileStream = null;

        public PingerFileReader()
        {
        }


        public PingerFileReader(string inputFilePath)
        {
            _inputFilePath = inputFilePath;
        }



        /// <exception cref="FileNotFoundException"
        /// Thrown when the file is not found.
        /// </exception>
        
        public void OpenFile(string path)
        {
            _inputFilePath = path;
            if (File.Exists(path))
            {
                _inputFilePath = path;
            }
            else
            {
                throw new FileNotFoundException();
            }

            FileInfo fileInfo = new FileInfo(_inputFilePath);
            _fileStream = fileInfo.Open(FileMode.Open, FileAccess.Read, FileShare.Read);
        }


        /// <exception cref="UnexpectedException">
        /// Thrown when the file is not found (OR) If the path is not specified
        /// </exception>
        /// <exception cref="PingerFileFormatException">
        /// Thrown when there is no number in the first line of the file (OR) If there are not equal no.of IPs
        /// </exception>
        
        public void CheckFileFormat()
        {
            if (_fileStream == null)
            {
                throw new UnexpectedException();
            }

            StreamReader streamReader = new StreamReader(_fileStream);

            #region 1. Checking If The First Line Of The File Contains An Integer Or Not

            string firstLineOfTheFile = streamReader.ReadLine();
            int givenNumberOfIps = 0;
            int ipCount = 0;

            try
            {
                if (int.TryParse(firstLineOfTheFile, out int value))
                {
                    givenNumberOfIps = value;
                }
                else
                {
                    throw new PingerFileFormatException("The first line of the file does not contain an integer.");
                }
            }
            catch (PingerFileFormatException ex)
            {
                Console.WriteLine(ex.Message);
            }

            #endregion

            #region 2. Checking If The Given IP Count Is Equal To The Actual Number Of IPs Present In The File
            
            while (!streamReader.EndOfStream)
            {
                string sentence = streamReader.ReadLine();
                if (sentence.Length != 0)
                {
                    ipCount++;
                }
            }
            streamReader.Close();

            if (ipCount != givenNumberOfIps)
            {
                throw new PingerFileFormatException("The number of IPs present in the file is not equal to the number given in the first line of the file.");
            }
            #endregion
        }



        /// <exception cref="UnexpectedException">
        /// Thrown when the file is not found or If the path is not specified
        /// </exception>
        /// <exception cref="PingerIPFormatException">
        /// Thrown when a number in an IP address is not lying between [0-254]
        /// </exception>
        /// <exception cref="PingerIPTypeException"
        /// Thrown when there is 100 in the first part of any of the IP addresses in the file
        /// </exception>
        
        public void CheckIPFormatAndType()
        {
            if (_fileStream == null)
            {
                throw new UnexpectedException();
            }

            using StreamReader streamReader = new StreamReader(File.OpenRead(_inputFilePath));

            streamReader.ReadLine();
            int line = 0;

            while (!streamReader.EndOfStream)
            {
                string ipAddress = streamReader.ReadLine();
                if (ipAddress == "")
                {
                    continue;
                }
                line += 1;
                var octets = ipAddress.Split('.').ToList();
                foreach (string octet in octets)
                {
                    int number = int.Parse(octet);
                    if (number < 0 || number > 254)
                    {
                        throw new PingerIPFormatException($"A number in an IP found out of bound at line: {line}");
                    }
                }

                int firstOctet = int.Parse(octets[0]);

                if (firstOctet == 100)
                {
                    throw new PingerIPTypeException($"An IP found with 100 in it's first part at line: {line}");
                }
            }
            streamReader.Close();
        }

        public void CloseFile()
        {
            _fileStream.Close();
            _fileStream = null;
        }

    }
}

