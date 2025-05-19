using System;
using System.Collections.Generic;
using UnityEngine;

public class ChestService
{
    private List<ChestController> chestControllersList = new List<ChestController>();
    private List<ChestSavedData> chestSavedDataList;

    public ChestService()
    {
        if (PlayerPrefs.HasKey("ChestSavedData"))
        {
            string dataString = PlayerPrefs.GetString("ChestSavedData");
            Wrapper wrapper = JsonUtility.FromJson<Wrapper>(dataString);
            chestSavedDataList = wrapper.chestSavedDataList;
            if (chestSavedDataList.Count > 0)
            {
                CreateExistingChest();
            }
        }
        else
        {
            chestSavedDataList = new List<ChestSavedData>();
        }
    }

    private void CreateExistingChest()
    {
        for (int i = 0; i < GameService.Instance.GetSlotCount(); i++)
        {
            SlotController slotController = GameService.Instance.SlotService.GetSlotController(i);
            ChestSavedData chestSavedData = chestSavedDataList.Find(x => x.slotIndex == i);
            if (chestSavedData != null)
            {
                ChestPrefabData chestPrefabData = GameService.Instance.GetChestPrefabDataList()[i];
                ChestModel chestModel = new ChestModel(chestPrefabData.chestScriptable, slotController, chestSavedData.chestState);
                ChestController chestControllers = new ChestController(chestPrefabData.chestPrefab, chestModel, slotController.GetSlotView().transform);
                chestControllersList.Add(chestControllers);
                slotController.GetSlotModel().SetChestInfo(chestControllers);
                chestControllers.CheckChestStateAndUpdateSlot(chestSavedData);
            }
        }
    }


    public void CreateNewChest()
    {
        bool isChestCreated = false;

        for (int i = 0; i < GameService.Instance.GetSlotCount(); i++)
        {
            SlotController slotController = GameService.Instance.SlotService.GetSlotController(i);

            if (slotController.GetSlotModel().IsSlotEmpty())
            {
                ChestPrefabData chestPrefabData = GameService.Instance.GetChestPrefabDataList()[i];

                ChestModel chestModel = new ChestModel(chestPrefabData.chestScriptable, slotController);
                ChestController chestControllers = new ChestController(chestPrefabData.chestPrefab, chestModel, slotController.GetSlotView().transform);
                chestControllersList.Add(chestControllers);
                slotController.GetSlotModel().SetChestInfo(chestControllers);

                chestSavedDataList.Add(new ChestSavedData()
                {
                    slotIndex = i,
                    chestState = ChestState.Locked,
                    chestType = chestPrefabData.chestType,

                    startTime = DateTime.Now.ToString(),
                });

                isChestCreated = true;
                break;
            }
        }

        if (!isChestCreated)
        {
            GameService.Instance.EventService.OnFailedString.InvokeEvent(FailedStringType.GenerateChestFailed);
        }

        string dataString = JsonUtility.ToJson(new Wrapper(chestSavedDataList));
        PlayerPrefs.SetString("ChestSavedData", dataString);
    }

    public void SetChestSavedData(int chestIndex, ChestState chestState)
    {
        ChestSavedData chestSavedData = chestSavedDataList.Find(x => x.slotIndex == chestIndex);

        if (chestSavedData != null)
        {
            chestSavedData.chestState = chestState;

            if (chestState == ChestState.Unlocking)
            {
                chestSavedData.startTime = DateTime.Now.ToString();
            }

            string dataString = JsonUtility.ToJson(new Wrapper(chestSavedDataList));
            PlayerPrefs.SetString("ChestSavedData", dataString);
        }
    }

    public void DeleteChestSavedData(int chestIndex)
    {
        ChestSavedData chestSavedData = chestSavedDataList.Find(x => x.slotIndex == chestIndex);
        if (chestSavedData != null)
        {
            chestSavedDataList.Remove(chestSavedData);
        }
    }
}

[System.Serializable]
public class ChestPrefabData
{
    public ChestType chestType;
    public ChestScriptable chestScriptable;
    public ChestView chestPrefab;
}

[System.Serializable]
public class Wrapper
{
    public List<ChestSavedData> chestSavedDataList;
    public Wrapper(List<ChestSavedData> ChestSavedDataList)
    {
        this.chestSavedDataList = ChestSavedDataList;
    }
}

[System.Serializable]
public class ChestSavedData
{
    public int slotIndex;
    public ChestState chestState;
    public ChestType chestType;
    public string startTime;
}

[System.Serializable]
public enum ChestState
{
    Locked,
    Unlocking,
    Opened
}