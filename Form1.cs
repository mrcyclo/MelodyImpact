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
using WindowsInput;
using WindowsInput.Native;

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

            int delay = 0;
            int.TryParse(tbDelay.Text, out delay);
            if (delay <= 0) return;

            int noteOffset = 0;
            int.TryParse(tbOffset.Text, out noteOffset);

            int trackIndex = 0;
            bool allTracks = cbAllTrack.Checked;
            if (!allTracks)
            {
                int.TryParse(tbTrack.Text, out trackIndex);
                if (trackIndex < 0) return;
            }

            if (string.IsNullOrEmpty(tbFile.Text)) return;

            MidiFile midi = new MidiFile(tbFile.Text);
            if (trackIndex >= midi.TracksCount && !allTracks) return;

            int startAt = 0;
            int.TryParse(tbStartAt.Text, out startAt);

            if (startAt > 0)
            {
                foreach (MidiTrack track in midi.Tracks)
                {
                    track.MidiEvents.RemoveAll(x => x.Time < startAt);
                }
            }

            btnPlay.Enabled = false;
            await Task.Run(async () =>
            {
                SetForegroundWindow(handle);
                await Task.Delay(100);

                int currentTick = startAt;
                int stepTick = midi.TicksPerQuarterNote / 4;
                int[] eventIdxs = new int[midi.TracksCount];

                InputSimulator sim = new InputSimulator();

                while (eventIdxs.Max() != -1)
                {
                    if (handle != GetForegroundWindow()) return;

                    Stopwatch watch = Stopwatch.StartNew();

                    List<MidiNote> notes = new List<MidiNote>();
                    foreach (MidiTrack track in midi.Tracks)
                    {
                        if (!allTracks && track.Index != trackIndex)
                        {
                            eventIdxs[track.Index] = -1;
                            continue;
                        }

                        while (true)
                        {
                            if (eventIdxs[track.Index] == -1 || eventIdxs[track.Index] >= track.MidiEvents.Count)
                            {
                                eventIdxs[track.Index] = -1;
                                break;
                            }

                            MidiEvent evt = track.MidiEvents[eventIdxs[track.Index]];
                            if (evt.Time >= currentTick && evt.Time < currentTick + stepTick && evt.MidiEventType == MidiEventType.NoteOn)
                            {
                                notes.Add(new MidiNote(evt.Note + noteOffset));
                            }
                            else if (evt.Time >= currentTick + stepTick)
                            {
                                break;
                            }
                            eventIdxs[track.Index]++;
                        }
                    }

                    List<VirtualKeyCode> keyCodes = new List<VirtualKeyCode>();
                    foreach (MidiNote note in notes)
                    {
                        if (note.GenshinKey == null) continue;
                        Enum.TryParse("VK_" + note.GenshinKey.ToUpper(), out VirtualKeyCode keyCode);
                        if (keyCode <= 0) continue;
                        keyCodes.Add(keyCode);
                    }
                    if (keyCodes.Count > 0)
                    {
                        sim.Keyboard.KeyPress(keyCodes.ToArray());
                    }

                    watch.Stop();

                    if (notes.Count > 0)
                    {
                        Invoke(new Action(() =>
                        {
                            rtbParse.AppendText(
                                string.Join(" ", notes.Select(x => x.NoteName)).PadLeft(20) +
                                " | " +
                                string.Join("", notes.Select(x => x.GenshinKey)).PadLeft(8) +
                                " | " +
                                (watch.ElapsedMilliseconds + "ms").PadLeft(5) +
                                "\n"
                            );
                            rtbParse.ScrollToCaret();
                        }));
                    }

                    await Task.Delay(Math.Max(delay - (int)watch.ElapsedMilliseconds, 0));
                    currentTick += stepTick;
                }
            });
            btnPlay.Enabled = true;
        }

        private void cbAllTrack_CheckedChanged(object sender, EventArgs e)
        {
            tbTrack.Enabled = !cbAllTrack.Checked;
        }
    }
}
