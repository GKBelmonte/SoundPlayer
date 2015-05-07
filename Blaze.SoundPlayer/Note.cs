using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blaze.SoundPlayer
{
    public struct Note
    {
        public string mStep;
        public int mOctave;
        public int mStart;
        public int mEnd;
        public float mFreq;
        public float mVelocity;

        public Note(string step, int octave, int start, int end)
        {
            mStep = step; 
            mOctave = octave; 
            mStart = start; 
            mEnd = end; 
            mFreq = GetFrequency(mOctave,mStep);
            mVelocity = 1;
        }

        public Note(float freq, int start, float velocity, int end = -1)
        {
            var temp = FrequencyToNote(freq);
            mStart = start;
            mStep = temp.mStep;
            mOctave = temp.mOctave;
            mEnd = end;
            mFreq = freq;
            mVelocity = velocity;
        }

        public override string ToString()
        {
            return string.Format("{0}{1}", mStep, mOctave);
        }

        static public Note operator + (Note me, int what)
        {
            var number = ( NoteLetterToKeyNumber(me.mStep)+what)  ;
            var octaveIncrease = (int)Math.Floor(number / 12.0);
            var newStep = KeyNumberToNoteLetter((number+12) % 12);
            return new Note(newStep, me.mOctave+octaveIncrease, me.mStart, me.mEnd);
        }

        static double[] cees = 
            { 
                8.176,      16.352  ,   32.703     ,65.406     ,130.813,
                261.626,    523.521 ,   1046.502   ,2093.005,   4186.009,   
                8372.018,   16744.036 
            };
        public static double MagicMusicNumber = Math.Pow(2, (double)1 / (double)12);

        public static Note FrequencyToNote(double freq)
        {
            var ind = 0;
            while (freq > cees[ind])
                ind++;
            ind -= 1;//1 for the extra addition
            var CeeFam = ind - 1;// and one to account for the fact that the first index is actually C-1

            if (freq <= MagicMusicNumber)
                return new Note("L0", 0, 0, 0); //Too low to consider

            var approx = (ind == -1 ? MagicMusicNumber : cees[ind]);
            var next = 0.0;

            var noteLetterModifier = 0;
            do
            {
                next = approx * MagicMusicNumber;
                if (freq < next)
                    break;
                ++noteLetterModifier;
                approx = next;

            } while (true);

            //Chose to go up or down
            {
                var distanceUp = Math.Abs(freq - next);
                var distanceDown = Math.Abs(freq - approx);
                if (distanceUp < distanceDown)
                {
                    noteLetterModifier += 1;
                }
            }

            var letter = KeyNumberToNoteLetter(noteLetterModifier);
            if (letter == "++")
            {
                letter = "C";
                CeeFam++;
            }
            // Console.WriteLine(string.Format("The freq {0} is approx {1} < %{0}% < {2}. Me thinks this is {3}{4}", freq, approx, next, letter, CeeFam));
            //var ret = string.Format("Family of {0} freq. The C{1} family", ind == -1 ? 0 : cees[ind], ind-1);

            return new Note(letter, CeeFam, 0, 0);
        }

        public static string KeyNumberToNoteLetter(int letterModifier)
        {
            switch (letterModifier)
            {
                case 0:
                    return "C";
                case 1:
                    return "C#";
                case 2:
                    return "D";
                case 3:
                    return "D#";
                case 4:
                    return "E";
                case 5:
                    return "F";
                case 6:
                    return "F#";
                case 7:
                    return "G";
                case 8:
                    return "G#";
                case 9:
                    return "A";
                case 10:
                    return "A#";
                case 11:
                    return "B";
                default:
                    return "?";

            }
        }

        public static int NoteLetterToKeyNumber(string noteLetter)
        {
            //case ([0-9]+).*\n.*return (\"[a-zA-Z]#?\")
            switch (noteLetter)
            {
                case "C": return 0;
                case "C#": return 1;
                case "D": return 2;
                case "D#": return 3;
                case "E": return 4;
                case "F": return 5;
                case "F#": return 6;
                case "G": return 7;
                case "G#": return 8;
                case "A": return 9;
                case "A#": return 10;
                case "B": return 11;
                default: return -1;
            }
        }


        static readonly float A0Frequency = 27.5f;
        public float GetFrequency()
        {
            var freq = A0Frequency * MathExtensions.PowerOfTwo(mOctave);
            var modifier = NoteLetterToKeyNumber(mStep) - NoteLetterToKeyNumber("A");
            return (float)(freq * Math.Pow(MagicMusicNumber, modifier));
        }

        static public float GetFrequency(int octave, string step)
        {
            var freq = A0Frequency * MathExtensions.PowerOfTwo(octave);
            var modifier = NoteLetterToKeyNumber(step) - 9;//NoteLetterToKeyNumber("A");
            return (float)(freq * Math.Pow(MagicMusicNumber, modifier));
        }

        //A0   @     27.5
        //A#0  @ 29.13523
        //B0   @ 30.86771
        //C1   @  32.7032
        //C#1  @ 34.64783
        //D1   @  36.7081
        //D#1  @ 38.89087
        //E1   @ 41.20345
        //F1   @ 43.65353
        //F#1  @  46.2493
        //G1   @ 48.99943
        //G#1  @ 51.91309
        //A1   @       55
        //A#1  @ 58.27047
        //B1   @ 61.73541
        //C2   @ 65.40639
        //C#2  @ 69.29565
        //D2   @ 73.41619
        //D#2  @ 77.78175
        //E2   @ 82.40689
        //F2   @ 87.30706
        //F#2  @  92.4986
        //G2   @ 97.99886
        //G#2  @ 103.8262
        //A2   @      110
        //A#2  @ 116.5409
        //B2   @ 123.4708
        //C3   @ 130.8128
        //C#3  @ 138.5913
        //D3   @ 146.8324
        //D#3  @ 155.5635
        //E3   @ 164.8138
        //F3   @ 174.6141
        //F#3  @ 184.9972
        //G3   @ 195.9977
        //G#3  @ 207.6523
        //A3   @      220
        //A#3  @ 233.0819
        //B3   @ 246.9417
        //C4   @ 261.6256
        //C#4  @ 277.1826
        //D4   @ 293.6648
        //D#4  @  311.127
        //E4   @ 329.6276
        //F4   @ 349.2282
        //F#4  @ 369.9944
        //G4   @ 391.9954
        //G#4  @ 415.3047
        //A4   @      440
        //A#4  @ 466.1638
        //B4   @ 493.8833
        //C5   @ 523.2512
        //C#5  @ 554.3652
        //D5   @ 587.3295
        //D#5  @  622.254
        //E5   @ 659.2551
        //F5   @ 698.4565
        //F#5  @ 739.9888
        //G5   @ 783.9908
        //G#5  @ 830.6094
        //A5   @      880
        //A#5  @ 932.3275
        //B5   @ 987.7666
        //C6   @ 1046.502
        //C#6  @  1108.73
        //D6   @ 1174.659
        //D#6  @ 1244.508
        //E6   @  1318.51
        //F6   @ 1396.913
        //F#6  @ 1479.978
        //G6   @ 1567.982
        //G#6  @ 1661.219
        //A6   @     1760
        //A#6  @ 1864.655
        //B6   @ 1975.533
        //C7   @ 2093.005
        //C#7  @ 2217.461
        //D7   @ 2349.318
        //D#7  @ 2489.016
        //E7   @ 2637.021
        //F7   @ 2793.826
        //F#7  @ 2959.955
        //G7   @ 3135.963
        //G#7  @ 3322.438
        //A7   @     3520
        //A#7  @  3729.31
        //B7   @ 3951.066
        //C8   @ 4186.009
        //C#8  @ 4434.922
        //D8   @ 4698.636
        //D#8  @ 4978.032
        //E8   @ 5274.041
        //F8   @ 5587.652
        //F#8  @ 5919.911
        //G8   @ 6271.927
        //G#8  @ 6644.875
        //A8   @     7040
        //A#8  @  7458.62
        //B8   @ 7902.133
        //C9   @ 8372.019
        //C#9  @ 8869.844
        //D9   @ 9397.272
        //D#9  @ 9956.063
        //E9   @ 10548.08
        //F9   @  11175.3
        //F#9  @ 11839.82
        //G9   @ 12543.85
        //G#9  @ 13289.75
        //A9   @    14080
        //A#9  @ 14917.24
        //B9   @ 15804.27
        //C10  @ 16744.04
    }
}
