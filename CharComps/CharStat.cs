using System;
using System.Collections.Generic;

public enum CharStatName
{
    Strength,
    Dexterity,
    Constitution,
    Intelligence,
    Wisdom,
    Charisma
}

[Serializable]
public class CharStatMod
{
    public string SourceName;
    public CharStatName StatName;
    public int Val;

    public CharStatMod(string sourceName, CharStatName statName, int val)
    {
        SourceName = sourceName;
        StatName = statName;
        Val = val;
    }
}

[Serializable]
public class CharStat
{

    public string InspectorName;
    public CharStatName StatName;

    public int BaseVal;

    public List<CharStatMod> StatMods;

    public CharStat(CharStatName statName, int baseVal)
    {
        StatName = statName;
        InspectorName = statName.ToString();
        BaseVal = baseVal;
        StatMods = new List<CharStatMod>();
    }


    public void AddStatMod(CharStatMod statMod)
    {
        StatMods.Add(statMod);
    }

    public void RemoveStatMod(CharStatMod statMod)
    {
        StatMods.Remove(statMod);
    }

    public int Val
    {
        get
        {
            int bonus = BaseVal;

            StatMods.ForEach((statMod) =>
            {
                bonus += statMod.Val;
            });

            return bonus;
        }
    }

    public int Bonus
    {
        get
        {
            return (Val - 10) / 2;
        }

    }
}