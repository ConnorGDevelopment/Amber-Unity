using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CharData : ScriptableObject
{
    public List<CharStat> Stats;

    void InitStats()
    {
        Stats = new List<CharStat>();

        foreach (CharStatName statName in Enum.GetValues(typeof(CharStatName)))
        {
            Stats.Add(new CharStat(statName, 10));
        }
    }

    public List<CharSkill> Skills;

    void InitSkills()
    {
        Skills = new List<CharSkill>();

        var jsonSkillList = Resources.Load<TextAsset>("Text/SkillList");

        var skillList = JsonUtility.FromJson<CharSkillDataDoc>(jsonSkillList.text);


        foreach (CharSkillData skillData in skillList.skills)
        {
            Debug.Log(skillData.skillName);
            Debug.Log(skillData.statName);
            Skills.Add(new CharSkill(skillData.skillName, skillData.statName, true));
        }
    }

    public List<CharSkill> SavingThrows;

    void InitSavingThrows()
    {
        SavingThrows = new List<CharSkill>();

        foreach (CharStatName statName in Enum.GetValues(typeof(CharStatName)))
        {
            SavingThrows.Add(new CharSkill(statName.ToString(), statName.ToString(), false));
        }
    }

    public int ProfBonus = 2;

    void InitAll()
    {
        InitStats();
        InitSkills();
        InitSavingThrows();
    }

    void Reset()
    {
        InitAll();
    }

    void Awake()
    {
        InitAll();
    }
}