using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MelodyImpact
{
    class MidiNote
    {
        public int NoteNumber { get; private set; }
        public string NoteName { get; private set; }
        public int Octave { get; private set; }

        public string GenshinKey { get; private set; }

        public MidiNote(int noteNum)
        {
            NoteNumber = noteNum;

            string[] notes = new string[] { "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#", "A", "A#", "B" };
            Octave = NoteNumber / 12 - 1;

            NoteName = notes[NoteNumber % 12] + Octave;

            string[] keys = new string[] {
                "z", "z", "x", "x", "c", "v", "v", "b", "b", "n", "n", "m",
                "a", "a", "s", "s", "d", "f", "f", "g", "g", "h", "h", "j",
                "q", "q", "w", "w", "e", "r", "r", "t", "t", "y", "y", "u"
            };

            int keyIndex = NoteNumber - 48;
            GenshinKey = (keyIndex < 0 || keyIndex >= keys.Length) ? null : keys[keyIndex];
        }
    }
}
