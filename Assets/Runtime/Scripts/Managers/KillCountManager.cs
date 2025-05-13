using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class KillCountManager
{
    public static int RockKillCount { get; private set; } = 0;
    public static int PaperKillCount { get; private set; } = 0;
    public static int ScissorsKillCount { get; private set; } = 0;

    public static int TotalRockKillCount { get; private set; } = 0;
    public static int TotalPaperKillCount { get; private set; } = 0;
    public static int TotalScissorsKillCount { get; private set; } = 0;

    public static int MaxRockKillCount { get; private set; } = 0;
    public static int MaxPaperKillCount { get; private set; } = 0;
    public static int MaxScissorsKillCount { get; private set; } = 0;

    public static void Incerement(string type)
    {
        switch (type)
        {
            case Structs.BulletType.SCISSORS:
                ScissorsKillCount++;
                TotalScissorsKillCount++;
                break;
            case Structs.BulletType.PAPER:
                PaperKillCount++;
                TotalPaperKillCount++;
                break;
            default:
                RockKillCount++;
                TotalRockKillCount++;
                break;
        }
    }

    public static void CompareCounts()
    {
        if (RockKillCount > MaxRockKillCount)
        {
            MaxRockKillCount = RockKillCount;
        }
        if (PaperKillCount > MaxPaperKillCount)
        {
            MaxPaperKillCount = PaperKillCount;
        }
        if (ScissorsKillCount > MaxScissorsKillCount)
        {
            MaxScissorsKillCount = ScissorsKillCount;
        }
    }
}
