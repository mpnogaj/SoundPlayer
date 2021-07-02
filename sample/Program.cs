using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using SoundPlayer;

namespace ConsoleAlertPlayer
{
    static class Program
    {
        static IEnumerable<string> GetAllFiles()
        {
            var directory = Directory.GetCurrentDirectory() + "/Media";
            var files = Directory.GetFiles(directory);
            foreach (var file in files)
            {
                if (Path.GetExtension(file) == ".wav") yield return file;
            }
        }
        
        static void Main()
        {
            Console.Clear();
            var files = GetAllFiles().ToList();
            // Init
            Player.Instance();
            Player.Instance().Volume = 0.8f;
            while (true)
            {
                Console.Clear();
                var i = 1;
                foreach (var file in files)
                {
                    Console.WriteLine($"{i}: {Path.GetFileName(file)}");
                    i++;
                }
                try
                {
                    var number = Convert.ToInt32(Console.ReadLine());
                    Player.Instance().Play(files[number - 1]);
                }
                catch (FormatException e)
                {
                    Console.WriteLine(e);
                    return;
                }
            }
        }
    }
}