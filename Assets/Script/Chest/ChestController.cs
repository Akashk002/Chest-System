using System;
using UnityEngine;

public class ChestController
{
    private ChestView chestView;
    private ChestModel chestModel;
    private SlotController slotController;

    public ChestController(ChestView chestPrefab, ChestModel chestModel, SlotController slotController)
    {
        Transform transform = slotController.GetSlotView().transform;

        chestView = GameObject.Instantiate(chestPrefab, transform);
        chestView.transform.SetAsFirstSibling();
        chestView.SetChestController(this);
        this.chestModel = chestModel;
        this.slotController = slotController;
    }

    public void CheckChestStateAndUpdateSlot(ChestSavedData chestSavedData)
    {
        if (chestModel.chestState == ChestState.Unlocking)
        {
            ResumeChestTimer(chestSavedData);
        }
        else
        if (chestModel.chestState == ChestState.Unlocked)
        {
            slotController.UnlockChest();
        }
    }

    private void ResumeChestTimer(ChestSavedData chestSavedData)
    {
        TimeSpan timeDifference = DateTime.Now - DateTime.Parse(chestSavedData.startTime);
        int totalTimeInSeconds = chestModel.chestScriptable.timeInMin * 60;
        var remainingTimeInSeconds = (totalTimeInSeconds) - (float)timeDifference.TotalSeconds;
        slotController.SetRemainingTime(remainingTimeInSeconds);
        slotController.StartTimerForUnlockChest();
    }

    public ChestView GetChestView()
    {
        return chestView;
    }
    public ChestModel GetChestModel()
    {
        return chestModel;
    }

    public void OpenChest()
    {
        chestView.GetChestAnimator().enabled = true;
        chestModel.GetChestReward();
    }

    internal SlotController GetSlotController()
    {
        return slotController;
    }
}
