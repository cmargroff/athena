using UnityEngine;

public static class RectTransformExtensions
{
  public static void SetAnchor(this RectTransform rectTransform, Vector2 anchor)
  {
    rectTransform.anchorMin = anchor;
    rectTransform.anchorMax = anchor;
    rectTransform.anchoredPosition = Vector3.zero;
  }
}