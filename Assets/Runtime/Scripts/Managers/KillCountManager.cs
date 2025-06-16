using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Runtime.CompilerServices;

public static class KillCountManager
{
    public static int RockKillCount { get; private set; } = 0;
    public static int PaperKillCount { get; private set; } = 0;
    public static int ScissorsKillCount { get; private set; } = 0;

    public static List<int> RockKillWaveList { get; private set; } = new List<int>() { 0 };   
    public static List<int> PaperKillWaveList { get; private set; } = new List<int>() { 0 };
    public static List<int> ScissorsKillWaveList { get; private set; } = new List<int>() { 0 };

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

                //Debug.Log($"Scissors count: {ScissorsKillCount}");
                break;
            case Structs.BulletType.PAPER:
                PaperKillCount++;
                TotalPaperKillCount++;

                //Debug.Log($"Paper count: {PaperKillCount}");
                break;
            default:
                RockKillCount++;
                TotalRockKillCount++;

                //Debug.Log($"Rock count: {RockKillCount}");
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

    public static void UpdateData()
    {
        int rockSum = RockKillWaveList.Sum();
        int thisWaveRockCount = RockKillCount - rockSum;
        RockKillWaveList.Add( thisWaveRockCount );


        int paperSum = PaperKillWaveList.Sum();
        int thisWavePaperCount = PaperKillCount - paperSum;
        PaperKillWaveList.Add(thisWavePaperCount);

        int scissorsSum = ScissorsKillWaveList.Sum();
        int thisWaveScissorsCount = ScissorsKillCount - scissorsSum;
        ScissorsKillWaveList.Add(thisWaveScissorsCount);
        //Debug.Log("RockKillWaveList: " + string.Join(", ", RockKillWaveList) + "\n" +
        //          "PaperKillWaveList: "+ string.Join(", ", PaperKillWaveList) + "\n" +
        //          "ScissrosKillWaveList: " + string.Join(", ", ScissorsKillWaveList));
    }

    public static void Reset()
    {
        RockKillCount = 0;
        PaperKillCount = 0;
        ScissorsKillCount = 0;
    }
}
