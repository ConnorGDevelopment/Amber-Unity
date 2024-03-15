using UnityEngine;
using UnityEngine.UI;

public class SkillEle : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI SkillName;
    [SerializeField] TMPro.TextMeshProUGUI StatNameShort;
    [SerializeField] TMPro.TextMeshProUGUI SkillBonus;

    [SerializeField] GameObject SkillProf;


    internal void Set(CharSkill charSkill, CharData charData)
    {
        SkillName.text = charSkill.SkillName.ToString();
        StatNameShort.text = DisplayHelpers.StatShorthand(charSkill.StatName);
        SkillBonus.text = DisplayHelpers.ShowSign(charSkill.Bonus(charData));
        SkillProf.GetComponent<Image>().color = charSkill.IsProf ? Color.black : Color.white;
    }
}