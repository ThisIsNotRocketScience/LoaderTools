using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HexToWaveLib;

namespace DacTestFileWriter
{
    class Program
    {
        static void Main(string[] args)
        {
            HexToWaveConverter.WriteDACCommandFile("output.wav");
        }
    }
}
