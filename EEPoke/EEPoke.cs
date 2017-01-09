using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HexToWaveLib;
using System.IO;

namespace EEPoke
{
    public partial class EEPoke : Form
    {
        public EEPoke()
        {
            InitializeComponent();
        }

        private void PokeButton_Click(object sender, EventArgs e)
        {
            MemoryStream MS = new MemoryStream();
            string tempfile = Path.GetTempFileName();
            HexToWaveLib.HexToWaveConverter.WriteEepromCommand(tempfile, address, value);
            var soundPlayer = new System.Media.SoundPlayer(tempfile);
            soundPlayer.PlaySync();
            File.Delete(tempfile);            
        }

        UInt32 address = 0;
        byte value = 0;

        private void AddressBox_TextChanged(object sender, EventArgs e)
        {
            address = ParseNumber(AddressBox.Text);
            AddressLabel.Text = String.Format("0x{0:X}", address);
        }

        private uint ParseNumber(string S)
        {
            try
            {
                S = S.Trim();

                if (S.Length > 0)
                {
                    if (S.ToLower().StartsWith("0x"))
                    {
                        return Convert.ToUInt32(S.Substring(2), 16);
                    }
                    return uint.Parse(S);
                }
            }
            catch (Exception)
            {

            }
            return 0;
        }

        private void ValueBox_TextChanged(object sender, EventArgs e)
        {
            value = (byte)ParseNumber(ValueBox.Text);
            ValueLabel.Text = String.Format("0x{0:X}", value);
        }
    }
}
