using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotService
{
    private List<SlotController> slotControllersList = new List<SlotController>();
    private SlotController unlockingSlot;
    public SlotService(SlotView slotPrefab, int noOfSlots, Transform slotTransform)
    {
        CreateSlot(slotPrefab,noOfSlots,slotTransform);
    }

    private void CreateSlot(SlotView slotPrefab , int noOfSlots , Transform slotTransform)
    {
        for (int i = 0; i < noOfSlots; i++)
        {
            SlotModel slotModel = new SlotModel();
            SlotController slotController = new SlotController(slotPrefab, slotModel, slotTransform);
            slotControllersList.Add(slotController);
        }
    }

    public SlotController GetSlotController(int slotIndex)
    {
        return slotControllersList[slotIndex];
    }  
    
    public int GetSlotIndex(SlotController SlotController)
    {
        return slotControllersList.IndexOf(SlotController); 
    }

    public void SetUnlockingSlot(SlotController slotController)
    {
        unlockingSlot = slotController;
    }
    public SlotController GetUnlockingSlot()
    {
        return unlockingSlot;
    }
}
