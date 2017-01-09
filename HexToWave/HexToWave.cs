using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using IHex;

namespace HexToWave
{
    class HexToWave
    {
        
        
        static void Main(string[] args)
        {

            if (args.Length < 2)
            {
                Console.WriteLine("Usage: HexToWave.exe {intel hex infile} {wav outfile}");
                return;
            }
            if (File.Exists(args[0]) == false)
            {
                Console.WriteLine("{0} not found!", args[0]);
                return;
            }

            HexToWaveLib.HexToWaveConverter.ConvertHexToWav(args[0], args[1]);
        }

       
    }
}
