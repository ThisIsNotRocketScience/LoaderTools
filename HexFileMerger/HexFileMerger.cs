using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexFileMerger
{
    class HexFileMerger
    {
        static void Main(string[] args)
        {
            if (args.Count() < 3)
            {
                Console.WriteLine("Usage: HexFileMerger.exe <file1> <file2> <outfile>");
                Console.WriteLine("Bytes from file1 will be overwritten using bytes from file2");

                return;
            }

            IHex.IHex.Merge(args[0], args[1], args[2]);

            Console.ReadKey();
        }
    }
}
