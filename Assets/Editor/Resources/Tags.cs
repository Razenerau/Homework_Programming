using UnityEngine;

[System.Serializable]
public struct Tags
{
    const string ROCK = "Rock";
    const string PAPER = "Paper";
    const string SCISSORS = "Scissors";

    public static string GetROCK() { return ROCK; }
    public static string GetPAPER() { return PAPER; }
    public static string GetSCISSORS() { return SCISSORS; }
}
