using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class UnlockedChest : MonoBehaviour
{
    [SerializeField] private TMP_Text gemCount;
    private SlotController slotController;

    private void OnEnable()
    {
       GameService.Instance.EventService.OnSlotSelect.AddListener(SetGetCount);
    }

    private void OnDisable()
    {
        GameService.Instance.EventService.OnSlotSelect.RemoveListener(SetGetCount);
    }

    private void SetGetCount(SlotController slotController)
    {
        this.slotController = slotController;
        gemCount.text = slotController.GetSlotModel().GetGemCountByTime().ToString();
    }

    public void StartTimer()
    { 
        if (slotController != null)
        {
            slotController.StartTimerForUnlockChest();
        }
    }

    public void UnlockedChestByGem()
    {
        if (slotController != null)
        {
            slotController.UnlockChestByGem();
        }
    }

}
