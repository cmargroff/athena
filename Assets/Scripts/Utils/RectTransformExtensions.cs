using UnityEngine;

public static class RectTransformExtensions
{
  public static void SetAnchor(this RectTransform rectTransform, Vector2 anchor)
  {
    rectTransform.anchorMin = anchor;
    rectTransform.anchorMax = anchor;
    rectTransform.anchoredPosition = Vector3.zero;
  }
  public static void ArrangeChildrenAnchorsEvenly(this RectTransform rectTransform, bool vertical = false)
  {
    var increment = 1f / (rectTransform.childCount - 1);
    for (var i = 0; i < rectTransform.childCount; i++)
    {
      var child = rectTransform.GetChild(i);
      if (vertical)
      {
        child.GetComponent<RectTransform>().SetAnchor(new Vector2(0.5f, increment * i));
      }
      child.GetComponent<RectTransform>().SetAnchor(new Vector2(increment * i, 0.5f));
    }
  }
}