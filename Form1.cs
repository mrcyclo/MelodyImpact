using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MelodyImpact
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            tbFile.Text = @"C:\Users\mrcyclo\Downloads\I_miss_you_piano.mid";
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "Select MIDI file";
            dialog.Filter = "MIDI File|*.mid";
            if (dialog.ShowDialog() != DialogResult.OK) return;

            tbFile.Text = dialog.FileName;
        }

        private void btnParse_Click(object sender, EventArgs e)
        {

        }
    }
}
