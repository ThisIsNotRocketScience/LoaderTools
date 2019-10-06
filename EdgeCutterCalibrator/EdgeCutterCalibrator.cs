using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WobblerCalibrator
{
    public partial class EdgeCutterCalibrator : Form
    {
        public EdgeCutterCalibrator()
        {
            InitializeComponent();

            normalBox.Text = levelNormal.ToString();
            phasedBox.Text = levelPhased.ToString();

            Explanation.Text = @"

1) boot Edgecutter in bootloader mode (press the mode button while starting for about 5 seconds - it will not start like it usually does)
2) put all the knobs in center position
3) connect a patchcable from your headphone out to the A input on wobbler
4) put your headphone volume to maximum
5) press the Write Neutral button
6) connect a multimeter to normal output
7) use the left up/down buttons to find the value that is closest to 0 volt
8) connect a multimeter to phased output
9) use the right up/down buttons to find the value that is closest to 0 volt
10) press the Save to EEPROM button
11) wait until it is done
12) just to make sure, press the Save to EEPROM button again
13) wait until it is done
14) press the REBOOT button
15) done!
";

        }
        int R = 0;
        int G = 0;
        int B = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (R > 0) R -= 8;
            if (G > 0) G -= 8;
            if (B > 0) B -= 8;
            if (R < 0) R = 0;
            if (G < 0) G = 0;
            if (B < 0) B = 0;
            this.BackColor = Color.FromArgb(255, R, G, B);
            if (Staged>0) Staged--;
            if (Staged == 1) WriteDACValues();
        }

        int Staged = 0;
        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
           
        }

        private void EdgecutterCalibrator_Keydown(object sender, KeyEventArgs e)
        {
       
        }

        int levelNormal = 120;
        int levelPhased = 120;

        private void NeutralButton_Click(object sender, EventArgs e)
        {
            levelNormal = 120;
            levelPhased = 120;
            R = 128;G = 128;B = 128;
            UpdateStuff();
            
        }

        private void UpdateStuff()
        {
            if (levelNormal > 4095) levelNormal = 4095;
            if (levelNormal < 0) levelNormal = 0;

            if (levelPhased > 4095) levelPhased = 4095;
            if (levelPhased < 0) levelPhased = 0;

            normalBox.Text = levelNormal.ToString();
            phasedBox.Text = levelPhased.ToString();

            Staged = 40;
        }

        private void WriteDACValues()
        {
            R = 255; G = 128; B = 0;
            MemoryStream MS = new MemoryStream();
            string tempfile = Path.GetTempFileName();
            HexToWaveLib.HexToWaveConverter.WriteDACCommand(tempfile, (uint)levelPhased, (uint)levelNormal);
            var soundPlayer = new System.Media.SoundPlayer(tempfile);
            soundPlayer.PlaySync();
            File.Delete(tempfile);

            R = 0; G = 255; B = 0;
        }

        private void UpNormal_Click(object sender, EventArgs e)
        {
            levelNormal++;
            UpdateStuff();
        }

        private void UpPhased_Click(object sender, EventArgs e)
        {
            levelPhased++;
            UpdateStuff();
        }

        private void NormalDown_Click(object sender, EventArgs e)
        {
            levelNormal--;
            UpdateStuff();
        }

        private void DownPhased_Click(object sender, EventArgs e)
        {
            levelPhased--;
            UpdateStuff();
        }

        void WriteEepromByte(ushort address, byte value)
        {
            MemoryStream MS = new MemoryStream();
            string tempfile = Path.GetTempFileName();
            HexToWaveLib.HexToWaveConverter.WriteEepromCommand(tempfile, address, value);
            var soundPlayer = new System.Media.SoundPlayer(tempfile);
            soundPlayer.PlaySync();
            File.Delete(tempfile);

        }

        struct Edgecutter2Calibration
        {
            public int CalibNormal ;
            public int CalibCurved;
            
        }

        byte[] getBytes(Edgecutter2Calibration wob)
        {
            int size = Marshal.SizeOf(wob);
            byte[] arr = new byte[size];

            IntPtr ptr = Marshal.AllocHGlobal(size);
            Marshal.StructureToPtr(wob, ptr, true);
            Marshal.Copy(ptr, arr, 0, size);
            Marshal.FreeHGlobal(ptr);
            return arr;
        }

        private void EepromSave_Click(object sender, EventArgs e)
        {
            Edgecutter2Calibration P = new Edgecutter2Calibration();
            P.CalibNormal = levelNormal;
            P.CalibCurved= levelPhased;

            //#define VERSIONBYTE 0x10
            WriteEepromByte(0x20, 0x10);
            System.Threading.Thread.Sleep(400);
            var B = getBytes(P);
            for (int i = 0; i < B.Length; i++)
            {
                WriteEepromByte((ushort)(0x20 + i + 1), B[i]);
                System.Threading.Thread.Sleep(100);
                WriteEepromByte((ushort)(0x20 + i + 1), B[i]);
                System.Threading.Thread.Sleep(100);
            }
            for (int i = 0; i < B.Length; i++)
            {
                byte b = B[i];
                Console.WriteLine("writing {0:X}:{1:X}", i, b);
            }
        }

        private void RebootButton_Click(object sender, EventArgs e)
        {
            MemoryStream MS = new MemoryStream();
            string tempfile = Path.GetTempFileName();
            HexToWaveLib.HexToWaveConverter.WriteRebootWav(tempfile);
            var soundPlayer = new System.Media.SoundPlayer(tempfile);
            soundPlayer.PlaySync();
            File.Delete(tempfile);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var soundPlayer = new System.Media.SoundPlayer("Edgecutter.wav");
            soundPlayer.PlaySync();

        }

        private void EdgeCutterCalibrator_Load(object sender, EventArgs e)
        {

        }
    }
}
