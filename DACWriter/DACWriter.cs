using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DACWriter
{
    public partial class DACWriter : Form
    {
        public DACWriter()
        {
            InitializeComponent();
        }

        private void min1_Click(object sender, EventArgs e)
        {
            val1box.Value = 0;
        }

        private void max1_Click(object sender, EventArgs e)
        {
            val1box.Value = 4095;

        }

        private void min2_Click(object sender, EventArgs e)
        {
            val2box.Value = 0;
        }

        private void max2_Click(object sender, EventArgs e)
        {
            val2box.Value = 4095;
        }

        private void PlayButton_Click(object sender, EventArgs e)
        {
            MemoryStream MS = new MemoryStream();
            string tempfile = Path.GetTempFileName();
            HexToWaveLib.HexToWaveConverter.WriteDACCommand(tempfile, (uint)val1box.Value, (uint)val2box.Value);
            var soundPlayer = new System.Media.SoundPlayer(tempfile);
            soundPlayer.PlaySync();
            File.Delete(tempfile);
        }

        private void Mid_Click(object sender, EventArgs e)
        {
            val1box.Value = 2048;
        }

        private void mid2_Click(object sender, EventArgs e)
        {
            val2box.Value = 2048;
        }

        private void vminBox_ValueChanged(object sender, EventArgs e)
        {
            CalcCenter();
        }

        private void vmaxbox_ValueChanged(object sender, EventArgs e)
        {
            CalcCenter();

        }

        private void CalcCenter()
        {
            var Min = vminBox.Value; // at 0
            var Max = vmaxbox.Value; // at 4095

            //Min + ((Max - Min) / 4095.0) * DAC = 0;
            //((Max - Min) / 4095.0) * DAC = -Min;
            //DAC = -Min/((Max - Min) / 4095.0)
            daccenter.Value = -Min / ((Max - Min) / (decimal)4095.0);
        }
    }
}
