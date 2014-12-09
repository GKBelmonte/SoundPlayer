using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blaze.SoundPlayer;
using Blaze.SoundPlayer.Waves;
namespace Blaze.SoundPlayer.TestConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            ISoundPlayer player = new NAudioSoundPlayer(0, 16000);
            player.PlayOnceSync(1000);
            Console.ReadKey(false);

            ISoundPlayer player2 = new NAudioSoundPlayer(1, 16000);
            player2.PlayOnceSync(1000);
            Console.ReadKey(false);
        }
    }
}
