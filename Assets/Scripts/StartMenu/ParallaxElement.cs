using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxElement : MonoBehaviour
{
    [SerializeField] private Vector2 sensitivity;

    private RectTransform rectTransform;
    private Vector3 originalPosition;

    void Awake() {
        rectTransform = GetComponent<RectTransform>();
        originalPosition = rectTransform.position;
    }

    void Update() {
        HandlePositioning();
    }

    private void HandlePositioning() {
        Vector3 mouseOffset = GetMouseOffsetFromCenter();
        Vector3 transformOffset = new Vector3(mouseOffset.x*sensitivity.x, mouseOffset.y*sensitivity.y, 0.0f);
        Vector3 newPosition = originalPosition - transformOffset;
        print(newPosition);
        rectTransform.position = newPosition;
    }

    private Vector3 GetMouseOffsetFromCenter() {
        Vector3 center = new Vector3(Screen.width/2f, Screen.height/2f, 0f);
        return Input.mousePosition - center;
    }
}
