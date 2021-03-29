using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
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

            tbFile.Text = @"C:\Users\mrcyclo\Downloads\Westlife_-_Beautiful_in_White.mid";
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        static extern int SetForegroundWindow(IntPtr point);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        private static extern IntPtr GetForegroundWindow();

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
                        string note = new MidiNote(midiEvent.Note + int.Parse(tbOffset.Text)).NoteName;
                        currentNotes.Add(note);
                    }
                }

                codeLines.Add("_Press('" + string.Join(" ", currentNotes.Distinct()) + "', 10)");

                currentTick += stepTick;
            }

            rtbParse.Text = string.Join("\n", codeLines);
        }

        private async void btnPlay_Click(object sender, EventArgs e)
        {
            rtbParse.Clear();

            Process process = Process.GetProcessesByName("GenshinImpact").First();
            if (process == null) return;

            IntPtr handle = process.MainWindowHandle;

            int stepTick = 0;
            int.TryParse(tbSpeed.Text, out stepTick);
            if (stepTick == 0) return;

            int noteOffset = 0;
            int.TryParse(tbOffset.Text, out noteOffset);

            int trackIndex = 0;
            int.TryParse(tbTrack.Text, out trackIndex);
            if (trackIndex < 0) return;

            MidiFile midi = new MidiFile(tbFile.Text);
            if (trackIndex >= midi.TracksCount) return;

            MidiTrack track = midi.Tracks[trackIndex];

            btnPlay.Enabled = false;
            await Task.Run(async () =>
            {
                SetForegroundWindow(handle);
                await Task.Delay(100);

                int currentTick = 0;
                int currentEventIndex = 0;
                while (currentEventIndex < track.MidiEvents.Count)
                {
                    if (handle != GetForegroundWindow()) return;

                    List<MidiEvent> currentTickEvents = new List<MidiEvent>();
                    while (true)
                    {
                        if (currentEventIndex >= track.MidiEvents.Count) break;

                        MidiEvent evt = track.MidiEvents[currentEventIndex];
                        if (evt.Time == currentTick && evt.MidiEventType == MidiEventType.NoteOn)
                        {
                            currentTickEvents.Add(evt);
                        }
                        else if (evt.Time > currentTick)
                        {
                            break;
                        }
                        currentEventIndex++;
                    }

                    foreach (MidiEvent evt in currentTickEvents)
                    {
                        MidiNote note = new MidiNote(evt.Note);
                        SendNote(note);
                    }

                    await Task.Delay(10);
                    currentTick += stepTick;
                }
            });
            btnPlay.Enabled = true;
        }

        private void SendNote(MidiNote note)
        {
            string[] keys = new string[] {
                "z", "z", "x", "x", "c", "v", "v", "b", "b", "n", "n", "m",
                "a", "a", "s", "s", "d", "f", "f", "g", "g", "h", "h", "j",
                "q", "q", "w", "w", "e", "r", "r", "t", "t", "y", "y", "u"
            };

            int keyIndex = note.NoteNumber - 48;
            if (keyIndex < 0 || keyIndex >= keys.Length) return;

            Invoke(new Action(() =>
            {
                rtbParse.AppendText(note.NoteName + " - " + keys[keyIndex] + "\n");
                rtbParse.ScrollToCaret();
            }));

            SendKeys.SendWait(keys[keyIndex]);
        }

        private void cbAllTrack_CheckedChanged(object sender, EventArgs e)
        {
            tbTrack.Enabled = !cbAllTrack.Checked;
        }
    }
}
