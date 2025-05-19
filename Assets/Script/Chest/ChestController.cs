using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestController 
{
    private ChestView chestView;
    private ChestModel chestModel;

    public ChestController(ChestView chestPrefab , ChestModel chestModel, Transform transform)
    {
        chestView = GameObject.Instantiate(chestPrefab, transform);
        chestView.transform.SetAsFirstSibling();
        chestView.SetChestController(this);
        this.chestModel = chestModel;
        chestModel.SetChestController(this);
    }

    public void CheckChestStateAndUpdateSlot(ChestSavedData chestSavedData)
    {
        if (chestModel.chestState == ChestState.Unlocking)
        {
            TimeSpan timeDifference = DateTime.Now - DateTime.Parse(chestSavedData.startTime);
            int totalTimeInSeconds = chestModel.chestScriptable.timeInMin * 60;
            var remainingTimeInSeconds = (totalTimeInSeconds) - (float)timeDifference.TotalSeconds;
            chestModel.slotController.GetSlotModel().SetRemainingTime(remainingTimeInSeconds);
            chestModel.slotController.StartTimerForUnlockChest();
        }
        else
        if (chestModel.chestState == ChestState.Opened)
        {
            chestModel.slotController.UnlockChest();
        }
        else
        {

        }
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
}
