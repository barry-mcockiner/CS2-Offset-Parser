using System;
using System.IO;
using Newtonsoft.Json;

namespace JsonOffsetParser
{
    internal sealed class EPoint
    {
        static void Main(string[] args)
        {
            Parser p = new Parser();
            Console.WriteLine($"[~] dwViewMatrix -> {p.get_offset_by_classname(1, null, "dwViewMatrix")}");
            Console.WriteLine($"[~] CBaseAnimGraphController.m_bIsUsingAG2 -> {p.get_offset_by_classname(0, "CBaseAnimGraphController", "m_bIsUsingAG2")}"); //random
            Console.WriteLine("[~] done");
            Console.ReadKey();

        }
    }
}
