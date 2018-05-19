using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAChallenge
{
    class FileWriter
    {
        public void Write(List<string> fileContents, string filename)
        {
            try
            {
                // if file exists - overwrite it
                using (StreamWriter sw = new StreamWriter(filename, false))
                {
                    foreach (string row in fileContents)
                    {
                        sw.WriteLine(row);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occured while trying to open a file: {0}", ex.Message);
            }
        }
    }
}
