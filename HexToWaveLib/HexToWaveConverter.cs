using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexToWaveLib
{
    public static class HexToWaveConverter
    {
        public class WaveHeader
        {
            public string sGroupID;
            public uint dwFileLength;
            public string sRiffType;

            public WaveHeader()
            {
                dwFileLength = 0;
                sGroupID = "RIFF";
                sRiffType = "WAVE";
            }
        }

        public static void ConvertHexToWav(string sourcehexfile, string targetwavfile)
        {

            IHex.IHex H = new IHex.IHex(sourcehexfile);
            var allbytes = H.Collapse();
            Console.WriteLine("0x{0:X} bytes read from {1}", allbytes.Count, sourcehexfile);

            int BootLoaderOffset = 0;

            BootLoaderOffset = FindOffset(allbytes);
            int count = Math.Max(0, allbytes.Count - BootLoaderOffset);
            List<byte> bytes = new List<byte>();

            for (int i = 0; i < count; i++)
            {
                bytes.Add(allbytes[i + BootLoaderOffset]);
            }

            int bcount = (bytes.Count() % 1024);
            if (bcount > 0)
            {
                for (int i = 0; i < 1024 - bcount; i++) bytes.Add(0);
            }          

            List<short> Data2 = new List<short>();
            byte idx = 0;
            UInt32 blockwrite = 0;
            List<byte> thebytes = new List<byte>();
            int blockchunk = 0;

            WriteLeadIn(Data2);
            Write4Byte(Data2, (byte)'D', (byte)'O', (byte)'I', (byte)'T');
            WriteInt(Data2, (uint)Math.Ceiling(bytes.Count() / 1024.0f));// blocks
            WriteLeadOut(Data2, true);
            WriteSecond(Data2);
            
            while ((bytes.Count() / 1024) * 1024 < bytes.Count()) bytes.Add(0);

            for (int i = 0; i < bytes.Count(); i += MAXCHUNK)
            {
                int bytecount = Math.Min(MAXCHUNK, bytes.Count() - i);
                if (Verbose) Console.WriteLine("writing chunk {0}", blockchunk);

                WriteLeadIn(Data2);
                Write4Byte(Data2, (byte)'B', (byte)'L', (byte)'O', idx);
                blockchunk++;
                List<byte> chunkbytes = new List<byte>();

                for (int j = 0; j < bytecount; j++)
                {
                    chunkbytes.Add(bytes[i + j]);
                    thebytes.Add(bytes[i + j]);
                }

                for (int j = bytecount; j < MAXCHUNK; j++)
                {
                    chunkbytes.Add(0);

                    thebytes.Add(0);

                }

                UInt32 CRC = crc32c(0, chunkbytes, MAXCHUNK);
                if (Verbose) Console.Write("CRC: ");
                WriteInt(Data2, CRC);
                if (Verbose) Console.WriteLine("");
                writecount = 0;

                for (int ii = 0; ii < MAXCHUNK; ii++) WriteByte(Data2, chunkbytes[ii]);

                WriteLeadOut(Data2, true);

                idx++;
                if (idx == 1024 / MAXCHUNK)
                {
                    WriteFlashCommand(blockwrite, thebytes, Data2);

                    blockwrite += 1024;
                    thebytes.Clear();
                    idx = 0;
                }
            }

            if (idx != 0)
            {
                while (thebytes.Count < 1024) thebytes.Add(0);
                WriteFlashCommand(blockwrite, thebytes, Data2);
            }

            for (int i = 0; i < 11; i++)
            {
                WriteEmptyLongPulse(Data2);
                WriteEmptyLongPulse(Data2);
            }

            WriteReboot(Data2);

            WaveGenerator W2 = new WaveGenerator(Data2);
            W2.Save(targetwavfile);


        }

        public static void WriteDACCommand(string outfile, uint value1, uint value2)
        {
            List<short> DACData = new List<short>();
            WriteDAC(DACData, value1, value2);
            WaveGenerator W3 = new WaveGenerator(DACData);
            W3.Save(outfile);
        }

        private static void WriteDAC(List<short> D, uint value1, uint value2)
        {
            WriteLeadIn(D);
            Write4Byte(D, (byte)'D', (byte)'A', (byte)'C', (byte)'S');
            WriteInt(D, (value1 << 16) + value2);
            WriteInt(D, (uint)(((uint)value2 << 16) + value1));
            WriteLeadOut(D, true);
        }

        public static void WriteEepromCommand(string outfile, UInt32 address, byte value)
        {
            List<short> EepromData = new List<short>();
            WriteEeprom(EepromData, address, value);
            WaveGenerator W3 = new WaveGenerator(EepromData);
            W3.Save(outfile);
        }

        private static void WriteEeprom(List<short> D, uint address, byte value)
        {
            WriteLeadIn(D);
            Write4Byte(D, (byte)'E', (byte)'E', (byte)'P', (byte)'R');
            WriteInt(D, (address << 16) + value);
            WriteInt(D, (uint)(((uint)value << 16) + address));
            WriteLeadOut(D, true);

           
        }

        public static void WriteRebootWav(string outputfilename)
        {
            List<short> RebootData = new List<short>();
            WriteReboot(RebootData);
            WaveGenerator W3 = new WaveGenerator(RebootData);
            W3.Save(outputfilename);
        }

        public class WaveFormatChunk
        {
            public string sChunkID;
            public uint dwChunkSize;
            public ushort wFormatTag;
            public ushort wChannels;
            public uint dwSamplesPerSec;
            public uint dwAvgBytesPerSec;
            public ushort wBlockAlign;
            public ushort wBitsPerSample;

            public WaveFormatChunk()
            {
                sChunkID = "fmt ";
                dwChunkSize = 16;
                wFormatTag = 1;
                wChannels = 1;
                dwSamplesPerSec = 44100;
                wBitsPerSample = 16;
                wBlockAlign = (ushort)(wChannels * (wBitsPerSample / 8));
                dwAvgBytesPerSec = dwSamplesPerSec * wBlockAlign;
            }
        }

        public class WaveDataChunk
        {
            public string sChunkID;
            public uint dwChunkSize;
            public short[] shortArray;

            public WaveDataChunk()
            {
                shortArray = new short[0];
                dwChunkSize = 0;
                sChunkID = "data";
            }
        }

        public class WaveGenerator
        {
            WaveHeader header;
            WaveFormatChunk format;
            WaveDataChunk data;

            public WaveGenerator(List<short> Data)
            {
                header = new WaveHeader();
                format = new WaveFormatChunk();
                data = new WaveDataChunk();

                int numSamples = Data.Count;
                data.shortArray = new short[numSamples];

                for (int i = 0; i < numSamples; i++)
                {
                    data.shortArray[i] = Data[i];
                }

                data.dwChunkSize = (uint)(data.shortArray.Length * (format.wBitsPerSample / 8));
                TimeSpan TS = new TimeSpan(0, 0, (int)((float)numSamples / (float)format.dwSamplesPerSec));
                if (Verbose) Console.WriteLine("");
                Console.WriteLine("wave generated: {0} seconds: {1}", (float)numSamples / (float)format.dwSamplesPerSec, TS.ToString());
            }

            public void Save(string filePath)
            {
                FileStream fileStream = new FileStream(filePath, FileMode.Create);
                BinaryWriter writer = new BinaryWriter(fileStream);

                writer.Write(header.sGroupID.ToCharArray());
                writer.Write(header.dwFileLength);
                writer.Write(header.sRiffType.ToCharArray());
                writer.Write(format.sChunkID.ToCharArray());
                writer.Write(format.dwChunkSize);
                writer.Write(format.wFormatTag);
                writer.Write(format.wChannels);
                writer.Write(format.dwSamplesPerSec);
                writer.Write(format.dwAvgBytesPerSec);
                writer.Write(format.wBlockAlign);
                writer.Write(format.wBitsPerSample);

                writer.Write(data.sChunkID.ToCharArray());
                writer.Write(data.dwChunkSize);
                foreach (short dataPoint in data.shortArray)
                {
                    writer.Write(dataPoint);
                }

                writer.Seek(4, SeekOrigin.Begin);
                uint filesize = (uint)writer.BaseStream.Length;
                writer.Write(filesize - 8);

                writer.Close();
                fileStream.Close();
            }

            internal void Save(MemoryStream outstream)
            {
                outstream.Seek(0, SeekOrigin.Begin);
                StreamWriter writer = new StreamWriter(outstream);
                writer.Write(header.sGroupID.ToCharArray());
                writer.Write(header.dwFileLength);
                writer.Write(header.sRiffType.ToCharArray());
                writer.Write(format.sChunkID.ToCharArray());
                writer.Write(format.dwChunkSize);
                writer.Write(format.wFormatTag);
                writer.Write(format.wChannels);
                writer.Write(format.dwSamplesPerSec);
                writer.Write(format.dwAvgBytesPerSec);
                writer.Write(format.wBlockAlign);
                writer.Write(format.wBitsPerSample);

                writer.Write(data.sChunkID.ToCharArray());
                writer.Write(data.dwChunkSize);
                foreach (short dataPoint in data.shortArray)
                {
                    writer.Write(dataPoint);
                }

                outstream.Seek(4, SeekOrigin.Begin);
                uint filesize = (uint)outstream.Length;
                writer.Write(filesize - 8);
            }
        }

        const UInt32 CRCPOLYNOMIAL = 0x82f63b78;

        static UInt32 crc32c(UInt32 crc, List<byte> buf, int len)
        {

            crc = ~crc;
            int B = 0;
            while (len-- > 0)
            {
                crc ^= buf[B];
                B++;
                for (int k = 0; k < 8; k++)
                {
                    crc = ((crc & 1) > 0) ? (crc >> 1) ^ CRCPOLYNOMIAL : crc >> 1;
                }
            }
            return ~crc;
        }

        static UInt32 ReadIntFromBytes(List<Byte> bytes, int offset)
        {

            UInt32 R = 0;
            R += bytes[offset + 0];
            R += (uint)(bytes[offset + 1]) << 8;
            R += (uint)(bytes[offset + 2]) << 16;
            R += (uint)(bytes[offset + 3]) << 24;
            return R;
        }


        private static int FindOffset(List<byte> allbytes)
        {
            for (int i = 0; i < allbytes.Count; i += 1024)
            {
                UInt32 Off = ReadIntFromBytes(allbytes, i + 4);
                if (Off > i && Off < allbytes.Count)
                {

                    if (Verbose) Console.WriteLine("Boot offset found at {0:X}", i);
                    return i;
                }
            }

            return 0;
        }

        private static void WriteReboot(List<short> D)
        {
            WriteLeadIn(D);
            Write4Byte(D, (byte)'B', (byte)'O', (byte)'O', (byte)'T');
            Write4Byte(D, (byte)'B', (byte)'O', (byte)'O', (byte)'T');
            WriteLeadOut(D, true);

        }

        private static void Write4Byte(List<short> Data2, byte v1, byte v2, byte v3, byte v4)
        {
            if (Verbose) Console.WriteLine("");
            if (Verbose) Console.Write("{0}", (char)v1);
            if (Verbose) Console.Write("{0}", (char)v2);
            if (Verbose) Console.Write("{0}", (char)v3);
            if (Verbose) Console.WriteLine("{0}", v4 > 'a' ? ((char)v4).ToString() : GetChar(v4));
            writecount = 0;
            WriteByte(Data2, v1, false);
            WriteByte(Data2, v2, false);
            WriteByte(Data2, v3, false);
            WriteByte(Data2, v4, false);

        }

        private static string GetChar(byte v4)
        {
            // uggly leftover caseswitch
            if (v4 < 34) return v4.ToString("X2");

            switch (v4)
            {
                case 0: return "0x00";
                case 1: return "0x01";
                case 2: return "0x02";
                case 3: return "0x03";
                case 4: return "0x04";
                case 5: return "0x05";
                case 6: return "0x06";
                case 7: return "0x07";
                case 8: return "0x08";
                case 9: return "0x09";
                case 10: return "0x0a";
                case 11: return "0x0b";
                case 12: return "0x0c";
                case 13: return "0x0d";
                case 14: return "0x0e";
                case 15: return "0x0f";
                case 16: return "0x10";
                case 17: return "0x11";
                case 18: return "0x12";
                case 19: return "0x13";
                case 20: return "0x14";
                case 21: return "0x15";
                case 22: return "0x16";
                case 23: return "0x17";
                case 24: return "0x18";
                case 25: return "0x19";
                case 26: return "0x1a";
                case 27: return "0x1b";
                case 28: return "0x1c";
                case 29: return "0x1d";
                case 30: return "0x1e";
                case 31: return "0x1f";
                case 32: return "0x20";
                case 33: return "0x21";
            }
            return ((char)v4).ToString();
        }

        private static void WriteFlashCommand(UInt32 blockwrite, List<byte> thebytes, List<short> Data2)
        {
            if (Verbose) Console.WriteLine("");
            UInt32 CRC = crc32c(0, thebytes, 1024);
            if (Verbose) Console.WriteLine("Block {0:X}, CRC: 0x{1:X}", blockwrite, CRC);

            writecount = 0;
            WriteLeadIn(Data2);

            Write4Byte(Data2, (byte)'F', (byte)'L', (byte)'A', (byte)'S');


            WriteInt(Data2, blockwrite);
            WriteInt(Data2, CRC);


            WriteLeadOut(Data2, false);
            // WriteSecond(Data2);

        }

        private static void WriteInt(List<short> data2, uint inputint)
        {
            byte B1 = (byte)((inputint >> 24) & 0xff);
            byte B2 = (byte)((inputint >> 16) & 0xff);
            byte B3 = (byte)((inputint >> 8) & 0xff);
            byte B4 = (byte)((inputint) & 0xff);
            WriteByte(data2, B1);
            WriteByte(data2, B2);
            WriteByte(data2, B3);
            WriteByte(data2, B4);
        }

        private static void WriteSecond(List<short> data2)
        {
            for (int i = 0; i < INTERRUPTRATE; i++)
            {
                data2.Add(0);
            }
        }
        const int MAXCHUNK = 128;


        const int INTERRUPTRATE = 44100;
        const int LONGFREQ = 500;
        const int SHORTFREQ = LONGFREQ * 2;
        const int LONGPULSE = (INTERRUPTRATE / LONGFREQ);

        const int SHORTPULSE = (INTERRUPTRATE / SHORTFREQ);

        private static void WriteLeadIn(List<short> Data)
        {
            for (int j = 0; j < 1; j++) WriteByte(Data, 0, false);

            //WriteByte(Data, 0x02, false);
            WriteByte(Data, 0x02, false);
            //WriteByte(Data, 0x09, false);
            // WriteByte(Data, 0x08, false);
            // WriteByte(Data, 0x07, false);
            // WriteByte(Data, 0x06, false);
            // WriteByte(Data, 0x05, false);
            // WriteByte(Data, 0x04, false);
            WriteByte(Data, 0x03, false);
            WriteByte(Data, 0x02, false);
            WriteByte(Data, 0x01, false);

            writecount = 0;

        }

        private static void WriteLeadOut(List<short> Data, bool shortleadout = false)
        {
            WriteSin(Data, SHORTPULSE);
            if (Verbose) Console.WriteLine("");
            if (shortleadout)
            {
                WriteEmptyLongPulse(Data);
                WriteEmptyLongPulse(Data);
                WriteEmptyLongPulse(Data);
            }
            else
            {
                for (int i = 0; i < 11; i++)
                {
                    WriteEmptyLongPulse(Data);
                }
            }

            if (Verbose) Console.WriteLine("");
            writecount = 0;

        }
        public static bool Verbose = false;
        private static void WriteEmptyLongPulse(List<short> data)
        {
            for (int i = 0; i < LONGPULSE; i++) data.Add(0);
            if (Verbose) Console.Write("-");
        }

        static int writecount = 0;

        private static void WriteByte(List<short> Data, byte A, bool show = true)
        {
            if (show)
            {
                if (Verbose) Console.Write("{0:X2} ", A);
                writecount++;
            }
            if (writecount > 15)
            {
                writecount = 0;
                if (Verbose) Console.WriteLine();
            }
            for (int j = 0; j < 8; j++)
            {
                if ((A & (1 << (7 - j))) > 0)
                {
                    WriteSin(Data, LONGPULSE);
                }
                else
                {
                    WriteSin(Data, SHORTPULSE);
                }
            }

        }

        private static void WriteSin(List<short> Data, int v)
        {
            for (int j = 0; j < v / 2; j++)
            {
                // Data.Add(32767);
            }
            for (int j = v / 2; j < v; j++)
            {
                //  Data.Add(-32767);
            }

            //return;

            float P = (float)(Math.PI * 2.0) / (float)v;
            for (int j = 0; j < v; j++)
            {
                Data.Add((short)(Math.Sin(j * P) * 32767.0f));
            }
        }

    }
}
