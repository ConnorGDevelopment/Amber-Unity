using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CStatBlock : MonoBehaviour
{
    public List<(string, int)> BaseStats;

    public List<CStatMod> StatMods;

    void Init()
    {
        BaseStats = new List<(string, int)>();

        List<string> statNames = new() { "Strength", "Dexterity", "Constitution", "Intelligence", "Wisdom", "Charisma" };

        foreach (string statName in statNames)
        {
            BaseStats.Add((statName, 10));
        }
    }

    public List<(string, int)> Stats
    {
        get
        {
            List<(string, int)> stats = new(BaseStats);

            StatMods.ForEach((statMod) =>
            {
                if (BaseStats.Exists((stat) => stat.Item1 == statMod.StatName))
                {
                    (string, int) match = BaseStats.Find((stat) => stat.Item1 == statMod.StatName);

                    match.Item2 += statMod.Val;
                }
            });

            return stats;
        }
    }

    void Awake()
    {
        Init();
    }
}