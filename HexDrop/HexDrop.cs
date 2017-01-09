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

namespace HexDrop
{
    public partial class HexDrop : Form
    {
        public HexDrop()
        {
            InitializeComponent();
        }

        private void label1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void label1_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] D = e.Data.GetData(DataFormats.FileDrop) as string[];                
                foreach (string S in D)
                {

                    if (File.Exists(S))
                    {
                        try
                        {
                            HexToWaveLib.HexToWaveConverter.ConvertHexToWav(S, S + ".wav");
                        }
                        catch(Exception)
                        {

                        }
                    }
                   
                }
            }
        }

        private void HexDrop_DragDrop(object sender, DragEventArgs e)
        {
            label1_DragDrop(sender, e);
        }

        private void HexDrop_DragEnter(object sender, DragEventArgs e)
        {
            label1_DragEnter(sender, e);
        }
    }
}
