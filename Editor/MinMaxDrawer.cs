using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(MinMax))]
public class MinMaxDrawer : PropertyDrawer{
  public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
    EditorGUI.BeginProperty(position, label, property);

    position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
    float labelWidth = 30;
    float half = (position.width - ((labelWidth+5) * 2)) / 2;

    float offset = 0;
    var l1 = new Rect(position.x, position.y, labelWidth, position.height);
    EditorGUI.LabelField(l1, "Min");

    offset += labelWidth +5;
    var min = new Rect(position.x + offset, position.y, half, position.height);
    EditorGUI.PropertyField(min, property.FindPropertyRelative("min"), GUIContent.none);

    offset += half + 5;
    var l2 = new Rect(position.x + offset, position.y, labelWidth, position.height);
    EditorGUI.LabelField(l2, "Max");

    offset += labelWidth+5;
    var max = new Rect(position.x + offset, position.y, half, position.height);
    EditorGUI.PropertyField(max, property.FindPropertyRelative("max"), GUIContent.none);

    EditorGUI.EndProperty();
  }
}
#endif