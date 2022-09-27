using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PingerPractice
{

    #region PingerFileFormatException
    public class PingerFileFormatException : Exception
    {
        public PingerFileFormatException() { }

        public PingerFileFormatException(string message)
            : base(message) { }
    }
    #endregion

    #region FileNotFoundException
    public class FileNotFoundException : Exception
    {
        public FileNotFoundException() { }

        public FileNotFoundException(string message)
            : base(message) { }
    }
    #endregion

    #region PingerIPFormatException
    public class PingerIPFormatException : Exception
    {
        public PingerIPFormatException() { }

        public PingerIPFormatException(string message)
            : base(message) { }

    }
    #endregion

}
