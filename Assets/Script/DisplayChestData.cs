using UnityEngine;
using TMPro;

public class DisplayChestData : MonoBehaviour
{
    [SerializeField] private TMP_Text cointext;
    [SerializeField] private TMP_Text gemtext;

    public void SetChestData(ChestScriptable chestScriptable)
    {
        cointext.SetText($"{chestScriptable.minimumCoin} - {chestScriptable.maximumCoin}");
        gemtext.SetText($"{chestScriptable.minimumGem} - {chestScriptable.maximumgem}");
    }

}
