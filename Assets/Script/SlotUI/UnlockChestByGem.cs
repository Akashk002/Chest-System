using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UnlockChestByGem : MonoBehaviour
{
    public TMP_Text GetCountText;

    public void UpdateGemCount(int val)
    {
        GetCountText.SetText(val.ToString());
    }
}
