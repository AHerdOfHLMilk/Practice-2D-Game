using UnityEngine;

[CreateAssetMenu(menuName= "MiniWarBase/BuildingDefinition")]
public class BuildingDefinition : ScriptableObject
{
    public string displayName;
    public Sprite sprite;
    public int baseGoldPerTick;
    public int baseUpgradeCost;
    public float baseUpgradeSeconds;

    public int GoldPerTickAtLevel(int level)
    {
        return baseGoldPerTick * level;
    }

    public int UpgradeCostToReach(int nextLevel)
    {
        return baseUpgradeCost * nextLevel * nextLevel;
    }

    public float UpgradeSecondsToReach(int nextLevel)
    {
        return baseUpgradeSeconds * nextLevel;
    }
}
