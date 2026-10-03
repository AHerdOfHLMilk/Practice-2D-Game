using System;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
  public static ResourceManager Instance { get; private set; }

    public static event Action<int> OnGoldChanged;

    [SerializeField] private float secondsPerTick = 1f;

    public int Gold { get; private set; } = 100;

    private float tickTimer;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Update()
    {
        tickTimer += Time.deltaTime;
        while (tickTimer >= secondsPerTick)
        {
            RunProductionTick();
            tickTimer -= secondsPerTick;
        }
    }

    private void RunProductionTick()
    {
        int producedGold = 0;
        foreach (Building building in FindObjectsByType<Building>())
        {
            producedGold += building.GoldPerTick;
        }
        AddGold(producedGold);
    }

    public void AddGold(int amount)
    {
        Gold += amount;
        OnGoldChanged?.Invoke(Gold);
    }

    public bool TrySpendGold(int amount)
    {
        if (amount > Gold)
        {
            return false;
        }
        Gold -= amount;
        OnGoldChanged?.Invoke(Gold);
        return true;
    }
}
