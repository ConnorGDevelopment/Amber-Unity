using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CharSkillData
{
    public string skillName;
    public string statName;
}


[Serializable]
public class CharSkillDataDoc
{
    public List<CharSkillData> skills;
}

[Serializable]
public class CharSkill
{
    public string SkillName;
    public CharStatName StatName;

    public bool IsProf;

    public CharSkill(string skillName, string statName, bool isProf)
    {
        SkillName = skillName;
        StatName = Enum.Parse<CharStatName>(statName);
        IsProf = isProf;
    }

    public int Bonus(CharData charData)
    {
        CharStat matchStat = charData.Stats.Find((stat) => stat.StatName == StatName);

        return matchStat.Bonus + (IsProf ? charData.ProfBonus : 0);
    }
}