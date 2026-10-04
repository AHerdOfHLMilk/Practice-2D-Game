using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuildingPanelUI : MonoBehaviour
{
    public static BuildingPanelUI Instance { get; private set; }

    [SerializeField] private GameObject panelRoot;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text levelText;
    [SerializeField] private TMP_Text productionText;
    [SerializeField] private TMP_Text upgradeCostText;
    [SerializeField] private Button upgradeButton;
    [SerializeField] private Image upgradeProgressFill;

    private Building selectedBuilding;

    private void Awake()
    {
        Instance = this;
        upgradeButton.onClick.AddListener(HandleUpgradeClicked);
        Hide();
    }

    public void Show(Building building)
    {
        if (selectedBuilding)
        {
            selectedBuilding.OnUpgradeCompleted -= RefreshStaticLabels;
        }
        selectedBuilding = building;
        selectedBuilding.OnUpgradeCompleted += RefreshStaticLabels;
        panelRoot.SetActive(true);
        RefreshStaticLabels();
    }

    public void Hide()
    {
        if (selectedBuilding)
        {
            selectedBuilding.OnUpgradeCompleted -= RefreshStaticLabels;            
        }
        selectedBuilding = null;
        panelRoot.SetActive(false);
    }

    private void RefreshStaticLabels()
    {
        nameText.text = selectedBuilding.Definition.displayName;
        levelText.text = "Lv " + selectedBuilding.Level;
        productionText.text = "+" + selectedBuilding.GoldPerTick + "/s";
        upgradeCostText.text = selectedBuilding.Definition
            .UpgradeCostToReach(selectedBuilding.Level + 1).ToString();
    }

    private void HandleUpgradeClicked()
    {
        if (selectedBuilding != null && selectedBuilding.TryStartUpgrade())
        {
            RefreshStaticLabels();
        }
    }

    private void Update()
    {
        if (!selectedBuilding || !selectedBuilding.IsUpgrading)
        {
            upgradeProgressFill.fillAmount = 0;
            return;
        }
        float totalTime = selectedBuilding.Definition.UpgradeSecondsToReach(selectedBuilding.Level + 1);
        float leftoverTime = selectedBuilding.UpgradeSecondsRemaining;
        upgradeProgressFill.fillAmount = 1-(leftoverTime/totalTime);
    }
}