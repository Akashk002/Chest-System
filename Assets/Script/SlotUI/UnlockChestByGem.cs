using UnityEngine;
using TMPro;

public class UnlockChestByGem : MonoBehaviour
{
    [SerializeField]private TMP_Text GetCountText;

    public void UpdateGemCount(int val)
    {
        GetCountText.SetText(val.ToString());
    }
}
