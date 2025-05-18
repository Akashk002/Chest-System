using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "ChestScriptableObject", menuName = "ScriptableObjects/ChestScriptableObject")]
public class ChestScriptable : ScriptableObject
{
    public ChestType chestType;
    public int minimumCoin;
    public int maximumCoin;
    public int minimumGem;
    public int maximumgem;
    public int timeInMin;
}

[CreateAssetMenu(fileName = "ChestScriptableObject", menuName = "ScriptableObjects/ChestScriptableList")]
public class ChestScriptableList : ScriptableObject
{
    public List<ChestScriptable> chestDataList;
}

public enum ChestType
{
    Common,
    Rare,
    Epic,
    Legendary
}
