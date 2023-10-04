using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeAreaPanel : MonoBehaviour
{
    RectTransform myPanel;

    private void Awake() {
       Vector2 safeAreaMinPos = Screen.safeArea.position;
       Vector2 safeAreaMaxPos = safeAreaMinPos + Screen.safeArea.size;
       safeAreaMinPos.x /= Screen.width;
       safeAreaMinPos.y /= Screen.height;
       safeAreaMaxPos.x /= Screen.width;
       safeAreaMaxPos.y /= Screen.height;
       myPanel = GetComponent<RectTransform>();
       myPanel.anchorMin = safeAreaMinPos;
       myPanel.anchorMax = safeAreaMaxPos;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
