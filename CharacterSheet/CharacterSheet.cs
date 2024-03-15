using System.Collections.Generic;
using UnityEngine;

public class CharacterSheet : MonoBehaviour
{

    [SerializeField] CharData CharData;
    [SerializeField] List<StatEle> StatEles;

    [SerializeField] List<SkillEle> SkillEles;

    public void Update()
    {
        for (int i = 0; i < StatEles.Count; i++)
        {
            StatEles[i].Set(CharData.Stats[i]);
        }

        for (int i = 0; i < SkillEles.Count; i++)
        {
            SkillEles[i].Set(CharData.Skills[i], CharData);
        }
    }


}