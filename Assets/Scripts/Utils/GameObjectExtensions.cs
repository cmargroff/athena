using UnityEngine;

public static class GameObjectExtensions {
  public static GameObject FindObjectByName(this GameObject obj, string objectName){
    var t = obj.transform.Find(objectName);
    return t ? t.gameObject : null;
  }
}