using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameService : GenericMonoSingleton<GameService>
{
    [SerializeField] private TMP_Text coinText;
    [SerializeField] private TMP_Text gemText;
    [SerializeField] private int noOfSlots = 4;
    [SerializeField] private Transform slotTransform;
    [SerializeField] private SlotView slotPrefab;
    [SerializeField] private List<ChestPrefabData> ChestPrefabDataList;
    [SerializeField] private UnlockedChest unlockedChest;
    [SerializeField] private DisplayOverlayTextHandler displayOverlayTextHandler;

    private SlotService slotService;
    private ChestService chestService;
    private EventService eventService;
    private CurrencyHandler currencyHandler;


    public SlotService SlotService { get { return slotService; } }
    public ChestService ChestService { get { return chestService; } }
    public CurrencyHandler CurrencyHandler { get { return currencyHandler; } }
    public EventService EventService { get { return eventService; } }
    private void Start()
    {
        slotService = new SlotService(slotPrefab, noOfSlots, slotTransform);
        chestService = new ChestService();
        currencyHandler = new CurrencyHandler(coinText, gemText);
        eventService = new EventService();
        displayOverlayTextHandler.SubscribeEvent();
    }

    public int GetSlotCount()
    {
        return noOfSlots;
    }

    public List<ChestPrefabData> GetChestPrefabDataList()
    {
        return ChestPrefabDataList;
    }

    public void CreateChest()
    {
        chestService.CreateNewChest();
    }

    public void OpenUnlockChestPopup()
    {
        unlockedChest.gameObject.SetActive(true);
    }
}



