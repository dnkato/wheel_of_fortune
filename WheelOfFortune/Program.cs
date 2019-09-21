using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WOFClassLib;

namespace WheelOfFortune
{
    class Program
    {
        static void Main(string[] args)
        {

            //Game game = new Game();

            //game.Start(); 

            //Console.ReadKey();

            // Replace the Game class with my refactored class...so EVIL :)
            var game = new GameWorkflow();
            
            game.Start();

        }
    }
}
