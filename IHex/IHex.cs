using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IHex
{
    public class HexSegment
    {
        public List<byte> Bytes = new List<byte>();
        public uint baseaddress;
        public uint lowestoffset = uint.MaxValue;

        // extend size to minimum flash block size
        public void PadToBlockSize(int blocksize)
        {
            int Length = Bytes.Count;
            int OrigLengh = Length;
            Length = ((Length / blocksize) + 2) * blocksize;
            while (Bytes.Count < Length) Bytes.Add(0xff);
            Console.WriteLine("Padding {0} to {1} bytes!", OrigLengh, Length);
        }

        // really braindead.. byt works *can't think*
        internal void Set(int address, byte value)
        {
            if (Bytes.Count == 0) lowestoffset = (uint)address;
            address -= (int)lowestoffset;
            while(address<0)
            {
                Bytes.Insert(0, 0);
                address++;
                lowestoffset--;
            }
            while (Bytes.Count <= address) Bytes.Add(0);
            Bytes[address] = value;            
        }

        // byte to int32 
        public UInt32 UintAt(int p)
        {
            return (UInt32)(Bytes[0 + p] + (Bytes[1 + p] << 8) + (Bytes[2 + p] << 16) + (Bytes[3 + p] << 24));            
        }
    }

    public class IHex
    {
        List<string> Lines = new List<string>();
        public List<byte> RawBytes;
        public Dictionary<long, HexSegment> DecodedSegment = new Dictionary<long, HexSegment>();
        public bool HasErrors = false;
        public uint BaseAddress = 0;

        public static void Merge(string file1, string file2, string outfile)
        {
            IHex A = new IHex(file1);
            IHex B = new IHex(file2);

           

            IHex I  = new IHex();

            I.AddSegmentsFrom(B);
            I.AddSegmentsFrom(A);
            I.Save(outfile);
        }

        public IHex()
        {

        }
        public void AddSegmentsFrom(IHex B)
        {
            foreach(var a in B.DecodedSegment)
            {
                var seg = this.FindOrCreateSegment(a.Value.lowestoffset);
                for (int i = 0; i < a.Value.Bytes.Count();i++ )
                {
                    seg.Set((int)a.Value.lowestoffset + i, a.Value.Bytes[(int)i]);
                }
            }
        }
        public void Save(string filename)
        {   
            List<string> lines = new List<string>();
            foreach(var a in DecodedSegment)
            {
                int bytesleft = a.Value.Bytes.Count();
                int c = 0;
                UInt32 address = a.Value.baseaddress;
                while(bytesleft > 0)
                {
                    int b = Math.Min(16, bytesleft);
                    ushort addr = (ushort)(address & 0xffff);
                    List<byte> bytes = new List<byte>();
                    bytes.Add((byte)b);
                    bytes.Add((byte)((addr >> 8) & 0xff));
                    bytes.Add((byte)((addr) & 0xff));
                    bytes.Add(0);

                    string L = ":";

                    for (int i = 0; i < b;i++ )
                    {
                        byte C = a.Value.Bytes[c++];
                        bytes.Add(C);
                        
                    }


                    UInt32 CheckSum = 0;
                    foreach(var theb in bytes)
                    {
                        CheckSum += theb;
                    }
                    byte checkbyte = (byte)(((byte)~(byte)(CheckSum & 0xff))+(byte)1);
                    bytes.Add(checkbyte);
                    foreach(var theb in bytes)
                    {
                        L += theb.ToString("X2");
                    }
                    address += (uint)b;
                    lines.Add(L);
                    bytesleft -= 16;
                }
            }
            lines.Add(":00000001FF");
            foreach (var a in lines) Console.WriteLine(a);
            File.WriteAllLines(filename, lines);
        
        }

        public IHex(string filename)
        {
            try
            {
                StreamReader SR = new StreamReader(File.Open(filename, FileMode.Open));
                while (SR.EndOfStream == false)
                {
                    Lines.Add(SR.ReadLine());
                }

                SR.Close();
                ParseLines();
            }
            catch (Exception)
            {
                HasErrors = true;
            }
        }
        
        byte hex8(string inp)
        {
            return (byte)Convert.ToInt32(inp, 16);
        }
        
        UInt16 hex16(string inp)
        {
            return (UInt16)Convert.ToInt32(inp, 16);
        }
        
        void ExtendToSize(int s)
        {
        }

        enum LineTypes
        {
            DATARECORD = 0x00,
            ENDOFFILE = 0x01,
            EXTENDEDSEGMENTADDRESS = 0x02,
            STARTSEGMENTADDRESS = 0x03,
            EXTENDEDLINEARADDRESS = 0x04,
            STARTLINEARADDRESS = 0x05,
            SAMSUNGROMCODE = 0x20,
            SAMSUNGEXTENSIONCODE = 0x22
        }

        void ParseLines()
        {
            BaseAddress = 0;
            int currentoffset = 0;
            int lastaddress = int.MaxValue;
            int lastbytecount = 0;
            foreach (var s in Lines) 
            {
                if (s[0] == ':')
                {
                    var bytecount = hex8(s.Substring(1, 2));
                    var address = hex16(s.Substring(3, 4));
                    int type = hex8(s.Substring(7, 2));
                    if(lastaddress + lastbytecount != address)
                    {
                        BaseAddress = address;
                        
                    }
                   
                    lastaddress = address;
                    lastbytecount = bytecount;

                    switch((LineTypes) type)
                    {
                        case LineTypes.DATARECORD:
                                 var seg = FindOrCreateSegment(BaseAddress);
                                for (int i = 0; i < bytecount; i++)
                                {
                                    byte T = hex8(s.Substring(9 + i * 2,2));
                                    seg.Set(i + address + currentoffset, T);
                                }
                            break;
                        case LineTypes.EXTENDEDSEGMENTADDRESS:
                            UInt16 D = hex16(s.Substring(9,4));                            
                            currentoffset = D << 4;
                            break;
                        case LineTypes.EXTENDEDLINEARADDRESS:
                            uint D2 = hex16(s.Substring(9, 4));

                            BaseAddress = (uint)(D2 << 16);
                            break;
                        default:
                           
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("cannot decode \"{0}\"", s);
                    HasErrors = true;
                }
            }
        }

        private HexSegment FindOrCreateSegment(uint baseaddress)
        {
            if (DecodedSegment.ContainsKey(baseaddress)) return DecodedSegment[baseaddress];
            DecodedSegment[baseaddress] = new HexSegment() { baseaddress = baseaddress };
            return DecodedSegment[baseaddress];            
        }

        
        public List<byte> FindSegment(long p)
        {
            if (DecodedSegment.ContainsKey(p)) return DecodedSegment[p].Bytes;
            return null;
        }

        public List<byte> Collapse()
        {
            uint len = 0; 
            foreach (var seg in DecodedSegment)
            {
                len = Math.Max(len, (uint)seg.Value.lowestoffset + (uint)seg.Value.Bytes.Count);
            }
            List<byte> res = new List<byte>();
            for (int i =0 ;i<len;i++) res.Add(0);
            foreach (var seg in DecodedSegment)
            {
                for(int i =0 ;i< seg.Value.Bytes.Count;i++)
                {
                    res[i + (int)seg.Value.lowestoffset] = seg.Value.Bytes[i];
                }
            }
            return res;            
        }
    }
}
