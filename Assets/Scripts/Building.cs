using UnityEngine;

public class Building : MonoBehaviour
{
    [SerializeField] private BuildingDefinition definition;

    public BuildingDefinition Definition {get { return definition;} }
    public int Level { get; private set; } = 1;
    public bool IsUpgrading { get; private set; }
    public float UpgradeSecondsRemaining { get; private set; }

    public int GoldPerTick
    {
        get
        {
            if (IsUpgrading)
            {
                return 0;
            }
            return definition.GoldPerTickAtLevel(Level);
        }
    }

    public bool TryStartUpgrade()
    {
        int cost = definition.UpgradeCostToReach(Level + 1);
        if (IsUpgrading || !ResourceManager.Instance.TrySpendGold(cost)) //ordering matters, 
        //reverse would spend first before check if upgrading
        {
            return false;
        }
        IsUpgrading = true;
        UpgradeSecondsRemaining = definition.UpgradeSecondsToReach(Level + 1);
        return true;
    }

    private void Update()
    {
        if(!IsUpgrading)
        {
            return;
        }
        if(UpgradeSecondsRemaining > 0)
        {
            UpgradeSecondsRemaining -= Time.deltaTime;
        }
        else
        {
            Level += 1;
            IsUpgrading = false;
        }
    }
}
