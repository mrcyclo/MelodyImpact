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

            //tbFile.Text = @"C:\Users\mrcyclo\Downloads\Canon_in_C.mid";
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "Select MIDI file";
            dialog.Filter = "MIDI File|*.mid";
            if (dialog.ShowDialog() != DialogResult.OK) return;

            tbFile.Text = dialog.FileName;
        }

        // Read: https://github.com/davidluzgouveia/midi-parser
        private void btnParse_Click(object sender, EventArgs e)
        {
            MidiFile midiFile = new MidiFile(tbFile.Text);

            int stepTick = int.Parse(tbSpeed.Text);

            int maxTick = 0;
            foreach (MidiTrack track in midiFile.Tracks)
            {
                foreach (MidiEvent midiEvent in track.MidiEvents)
                {
                    if (maxTick < midiEvent.Time)
                    {
                        maxTick = midiEvent.Time;
                    }
                }
            }

            List<string> codeLines = new List<string>();
            int currentTick = 0;
            while (currentTick < maxTick)
            {
                List<string> currentNotes = new List<string>();
                foreach (MidiTrack track in midiFile.Tracks)
                {
                    List<MidiEvent> filteredEvents = track.MidiEvents.FindAll(x => x.Time >= currentTick && x.Time < currentTick + stepTick && x.MidiEventType == MidiEventType.NoteOn);
                    foreach (MidiEvent midiEvent in filteredEvents)
                    {
                        string note = num2noteName(midiEvent.Note - 4, track.Index == 0 ? 0 : 0);
                        currentNotes.Add(note);
                    }
                }

                codeLines.Add("_Press('" + string.Join(" ", currentNotes) + "', 10)");

                currentTick += stepTick;
            }

            rtbParse.Text = string.Join("\n", codeLines);
        }

        private string num2noteName(int notenum, int octaveOffset = 0)
        {
            string[] notes = new string[] { "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#", "A", "A#", "B" };
            int octave = notenum / 12 - 1 + octaveOffset;
            return notes[notenum % 12] + octave;
        }
    }
}
