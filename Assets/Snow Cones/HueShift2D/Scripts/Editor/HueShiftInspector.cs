using UnityEditor;
using UnityEngine;

namespace HueShift2D
{
    [CustomEditor(typeof (HueShift))]
    public class HueShiftInspector : Editor
    {
        public override void OnInspectorGUI()
        {
            HueShift hs = target as HueShift;

            // HueShift properties
            float _limitL = hs.LowerLimit*360f;
            float _limitU = hs.UpperLimit*360f;
            float _inverted = hs.Inverted ? 1 : -1;
            EditorGUILayout.MinMaxSlider(new GUIContent("Color range"), ref _limitL, ref _limitU, 0f, 360f);

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.Space();
            Color defaultColor = GUI.backgroundColor;
            DrawKnownColor("Red", Color.red, 15, 315, true, ref _limitL, ref _limitU, ref _inverted);
            DrawKnownColor("Magenta", Color.magenta, 255, 315, false, ref _limitL, ref _limitU, ref _inverted);
            DrawKnownColor("Blue", Color.blue, 195, 255, false, ref _limitL, ref _limitU, ref _inverted);
            DrawKnownColor("Cyan", Color.cyan, 135, 195, false, ref _limitL, ref _limitU, ref _inverted);
            DrawKnownColor("Green", Color.green, 75, 135, false, ref _limitL, ref _limitU, ref _inverted);
            DrawKnownColor("Yellow", Color.yellow, 15, 75, false, ref _limitL, ref _limitU, ref _inverted);
            EditorGUILayout.EndHorizontal();
            GUI.backgroundColor = defaultColor;

            hs.LowerLimit = _limitL/360f;
            hs.UpperLimit = _limitU/360f;
            hs.Inverted = EditorGUILayout.Toggle("Inverted", _inverted > 0);
            hs.Shift = EditorGUILayout.Slider("Hue Shift Amount", hs.Shift*180, -180f, 180f)/180f;
            if (hs.GetComponent<Renderer>().sharedMaterial.HasProperty("_Saturation"))
                hs.Saturation = EditorGUILayout.Slider("Saturation", hs.Saturation, -1, 1f);
            if (GUI.changed)
            {
                EditorUtility.SetDirty(this);
                EditorUtility.SetDirty(hs.GetComponent<Renderer>());
            }
        }

        private void DrawKnownColor(string caption, Color color, float limitL, float limitU, bool invert,
            ref float newLimitL, ref float newLimitU, ref float newInverted)
        {
            GUI.backgroundColor = color;
            if (GUILayout.Button("", GUILayout.Width(24), GUILayout.Height(24)))
            {
                newLimitL = limitL;
                newLimitU = limitU;
                newInverted = invert ? 1 : -1;
            }
        }
    }
}