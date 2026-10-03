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
