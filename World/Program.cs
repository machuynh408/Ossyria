using OssyriaDEV;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace World
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Ossyria : v0." + OssyriaDEV.Settings.VERSION % 100 + " (World)";

            if (!Directory.Exists(Settings.LIB_PATH + @"\Character\"))
                OssyriaDEV.Library.dumpCharacterWz();
            if (!Directory.Exists(Settings.LIB_PATH + @"\Item\"))
                OssyriaDEV.Library.dumpItemWz();
