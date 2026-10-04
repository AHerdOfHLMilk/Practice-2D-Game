using UnityEngine;
using Screen = UnityEngine.Device.Screen;

public class SafeAreaFitter : MonoBehaviour
{
    private RectTransform safeAreaRectTransform;
    private void Awake()
    {
        safeAreaRectTransform = GetComponent<RectTransform>();
    }
    private void Start()
    {
        Rect safeArea = Screen.safeArea;
        Vector2 anchorMin = new Vector2(safeArea.x/Screen.width, safeArea.y/Screen.height);
        Vector2 anchorMax = new Vector2((safeArea.x + safeArea.width)/Screen.width, 
            (safeArea.y + safeArea.height)/Screen.height);
        safeAreaRectTransform.anchorMin = anchorMin;
        safeAreaRectTransform.anchorMax = anchorMax;
        safeAreaRectTransform.offsetMax = Vector2.zero;
        safeAreaRectTransform.offsetMin = Vector2.zero;
    }

}
