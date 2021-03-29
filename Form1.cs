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
            rtbParse.Clear();
        }

        // Read: https://github.com/davidluzgouveia/midi-parser
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
            bool allTracks = cbAllTrack.Checked;
            if (!allTracks)
            {
                int.TryParse(tbTrack.Text, out trackIndex);
                if (trackIndex < 0) return;
            }

            MidiFile midi = new MidiFile(tbFile.Text);
            if (trackIndex >= midi.TracksCount && !allTracks) return;

            btnPlay.Enabled = false;
            await PlayMelody(midi, handle, stepTick, allTracks, trackIndex, noteOffset);
            btnPlay.Enabled = true;
        }

        private void cbAllTrack_CheckedChanged(object sender, EventArgs e)
        {
            tbTrack.Enabled = !cbAllTrack.Checked;
        }

        private async Task PlayMelody(MidiFile midi, IntPtr handle, int stepTick, bool allTracks, int trackIndex, int noteOffset)
        {
            SetForegroundWindow(handle);
            await Task.Delay(100);

            int currentTick = 0;
            int[] eventIdxs = new int[midi.TracksCount];

            while (true)
            {
                if (handle != GetForegroundWindow()) return;

                Stopwatch watch = Stopwatch.StartNew();

                List<MidiNote> notes = new List<MidiNote>();
                foreach (MidiTrack track in midi.Tracks)
                {
                    if (!allTracks && track.Index != trackIndex) continue;

                    while (true)
                    {
                        if (eventIdxs[track.Index] >= track.MidiEvents.Count) break;

                        MidiEvent evt = track.MidiEvents[eventIdxs[track.Index]];
                        if (evt.Time == currentTick && evt.MidiEventType == MidiEventType.NoteOn)
                        {
                            notes.Add(new MidiNote(evt.Note + noteOffset));
                        }
                        else if (evt.Time > currentTick)
                        {
                            break;
                        }
                        eventIdxs[track.Index]++;
                    }
                }

                string keys = string.Join("", notes.Select(x => x.GenshinKey));
                if (!string.IsNullOrEmpty(keys))
                {
                    Task.Run(() => SendKeys.SendWait(keys));
                }

                watch.Stop();

                if (!string.IsNullOrEmpty(keys))
                {
                    Invoke(new Action(() =>
                    {
                        rtbParse.AppendText(
                            string.Join(" ", notes.Select(x => x.NoteName)).PadLeft(20) +
                            " | " +
                            keys.PadLeft(8) +
                            " | " +
                            (watch.ElapsedMilliseconds + "ms").PadLeft(5) +
                            "\n"
                        );
                        rtbParse.ScrollToCaret();
                    }));
                }

                await Task.Delay(Math.Max(10 - (int)watch.ElapsedMilliseconds, 0));
                currentTick += stepTick;
            }
        }
    }
}
