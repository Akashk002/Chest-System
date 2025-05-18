using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotModel 
{
    private SlotController slotController;
    public ChestController chestController;
    public float timeNeededToUnlock;
    public void SetSlotCountroller(SlotController slotController)
    {
        this.slotController = slotController;

        if (chestController != null)
        {
            if (GetChestState() == ChestState.Unlocking)
            {
                slotController.StartTimerForUnlockChest();
            }
            else
            if (GetChestState() == ChestState.Opened)
            {
                slotController.UnlockChest();
            }
            else
            {

            }
        }
    }

    public bool IsSlotEmpty()
    {
        return chestController == null;
    }
    public void SetChestInfo(ChestController chestController)
    {
        this.chestController = chestController;
        slotController.GetSlotView().emptyText.transform.SetAsFirstSibling();
        slotController.GetSlotView().lockedChestText.enabled = true;
        timeNeededToUnlock = chestController.GetChestModel().GetUnlockingTimeInMin();
        UpdateSlotTimeText();
    }

    public void UpdateSlotTimeText()
    {
        slotController.GetSlotView().timeText.enabled = true;
        float timeInHours = timeNeededToUnlock / 60f;
        slotController.GetSlotView().timeText.SetText(timeInHours + "H");
    }

    public int GetGemCountByTime()
    {
        if (slotController.GetChestState() == ChestState.Unlocking)
        {
            float time = slotController.GetSlotView().timerController.GetTime();
            return Mathf.CeilToInt(time / 10);
        }

        return Mathf.CeilToInt(timeNeededToUnlock / 10); ;
    }

    public ChestState GetChestState()
    {
        return chestController.GetChestModel().GetChestState();
    }
}
