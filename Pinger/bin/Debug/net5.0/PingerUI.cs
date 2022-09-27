using System;
using System.IO;
using PingerFile;
using ExceptionClasses;
using log4net;

namespace Pinger
{
    internal class PingerUI
    {
        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        /// <summary>This is the entry point of the Pinger class program.
        /// <para>
        /// This Program runs all the methods that check 
        /// 1. File existance, 
        /// 2. File Format, and
        /// 3. IP format and IP Type in the file
        /// </para>
        /// </summary>
        static void Main(string[] args)
        {
            _log.Error("project start");
            string pathOfTheFile = @"C:\Pinger\addresse1.txt";

            PingerFileReader pingerFileReader = new PingerFileReader(pathOfTheFile);

            OpenPingerFile(pingerFileReader, pathOfTheFile);
            CheckPingerFormat(pingerFileReader);
            CheckIPFormatAndType(pingerFileReader);

        }



        /// <summary>
        /// Checks if the file is present and Opens if yes.
        /// </summary>
        /// <param name="pingerFileReader"></param>
        /// <param name="path"></param>
        static void OpenPingerFile(PingerFileReader pingerFileReader, string path)
        {
            try
            {
                pingerFileReader.OpenFile(path);
            }
            catch (FileNotFoundException ex)
            {
                //Console.WriteLine(ex.Message);
                _log.Error(ex);
            }
        }



        /// <summary>
        /// Checks if the Format of the file
        /// </summary>
        /// <param name="pingerFileReader"></param>
        static void CheckPingerFormat(PingerFileReader pingerFileReader)
        {
            try
            {
                pingerFileReader.CheckFileFormat();
            }
            catch (UnexpectedException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (PingerFileFormatException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }



        /// <summary>
        /// Checks the IP Format and Type in the file
        /// </summary>
        /// <param name="pingerFileReader"></param>
        static void CheckIPFormatAndType(PingerFileReader pingerFileReader)
        {
            try
            {
                pingerFileReader.CheckIPFormatAndType();
            }
            catch (UnexpectedException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (PingerIPFormatException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (PingerIPTypeException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}

