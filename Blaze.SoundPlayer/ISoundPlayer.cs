using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blaze.SoundPlayer
{
    public interface ISoundPlayer
    {
        void SetSampleRate();
        void PlayOnce(int duration);
        void PlayOnceSync(int duration);
        int[] Data { get; set; }
    }
}
