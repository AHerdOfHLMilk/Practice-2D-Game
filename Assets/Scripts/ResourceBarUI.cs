using TMPro;
using UnityEngine;

public class ResourceBarUI : MonoBehaviour
{
    [SerializeField] private TMP_Text goldText;

    private void OnEnable()
    {
        ResourceManager.OnGoldChanged += HandleGoldChanged;
    }

    private void Start()
    {
        HandleGoldChanged(ResourceManager.Instance.Gold);
    }

    private void HandleGoldChanged(int newGold)
    {
        goldText.text = newGold.ToString();
    }

    private void OnDisable()
    {
        ResourceManager.OnGoldChanged -= HandleGoldChanged;
    }

}