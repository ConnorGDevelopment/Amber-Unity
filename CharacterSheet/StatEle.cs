using UnityEngine;

public class StatEle : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI StatName;
    [SerializeField] TMPro.TextMeshProUGUI StatVal;
    [SerializeField] TMPro.TextMeshProUGUI StatBonus;

    internal void Set(CharStat charStat)
    {
        StatName.text = charStat.StatName.ToString();
        StatVal.text = charStat.Val.ToString();
        StatBonus.text = DisplayHelpers.ShowSign(charStat.Bonus);
    }
}