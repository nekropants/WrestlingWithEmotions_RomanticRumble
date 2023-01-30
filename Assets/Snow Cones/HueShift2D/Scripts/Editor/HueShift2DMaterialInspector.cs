/**
 * HueShiftInspector.cs
 * Author: Thomas Hummes
 * **/


using UnityEngine;
using UnityEditor;

/// <summary>
/// Custom inspector for HueShift shader
/// </summary>
/// <remarks>Requires Unity 4.1</remarks>
public class HueShift2DMaterialInspector : MaterialEditor
{
	public override void OnInspectorGUI  ()
	{
		Material targetMat = target as Material;

		if (targetMat == null)
		{
			Debug.Log("Error creating HueShift Inspector");
			return;
		}
		// due to instruction limit, the shader values are clamped between 0..1
		// for easier use, they are enlarged to editing
		Texture2D mainTex = (Texture2D) targetMat.GetTexture("_MainTex");
		float LimitL = targetMat.GetFloat("_LimitL")*360f;
		float LimitU = targetMat.GetFloat("_LimitU")*360f;
		float Shift = targetMat.GetFloat("_Shift");
		bool Inverted = targetMat.GetFloat("_Inverted") > 0;
		
		float Alpha = targetMat.GetFloat("_Alpha");
		Color MultiplyColor = targetMat.GetColor("_Color");

		GUILayout.Label("Properties", EditorStyles.boldLabel);

		if (!targetMat.shader.name.Equals("Tastenhacker/HueShiftSprite2D"))
			mainTex = (Texture2D) EditorGUILayout.ObjectField("Texturea", mainTex, typeof (Texture2D), false);
		MultiplyColor = EditorGUILayout.ColorField("Color", MultiplyColor);

		float _limitL = LimitL;
		float _limitU = LimitU;
		EditorGUILayout.MinMaxSlider(new GUIContent("Color range"), ref _limitL, ref _limitU, 0f, 360f);

		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.Space();
		Color defaultColor = GUI.backgroundColor;
		DrawKnownColor("Red", Color.red, 15, 315, true, ref _limitL, ref _limitU, ref Inverted);
		DrawKnownColor("Magenta", Color.magenta, 255, 315, false, ref _limitL, ref _limitU, ref Inverted);
		DrawKnownColor("Blue", Color.blue, 195, 255, false, ref _limitL, ref _limitU, ref Inverted);
		DrawKnownColor("Cyan", Color.cyan, 135, 195, false, ref _limitL, ref _limitU, ref Inverted);
		DrawKnownColor("Green", Color.green, 75, 135, false, ref _limitL, ref _limitU, ref Inverted);
		DrawKnownColor("Yellow", Color.yellow, 15, 75, false, ref _limitL, ref _limitU, ref Inverted);
		EditorGUILayout.EndHorizontal();
		GUI.backgroundColor = defaultColor;

		LimitL = _limitL/360f;
		LimitU = _limitU/360f;
		Inverted = EditorGUILayout.Toggle("Inverted", Inverted);

		Shift = EditorGUILayout.IntSlider("Hue Shift Amount", Mathf.RoundToInt(Shift*180), -180, 180)/180f;


		targetMat.SetFloat("_LimitL", LimitL);
		targetMat.SetFloat("_LimitU", LimitU);
		targetMat.SetFloat("_Shift", Shift);
		targetMat.SetFloat("_Inverted", Inverted ? 1 : -1);
		targetMat.SetFloat("_Alpha", Alpha);
		targetMat.SetColor("_Color", MultiplyColor);
		targetMat.SetTexture("_MainTex", mainTex);
	}


	private void DrawKnownColor(string caption, Color color, float limitL, float limitU, bool invert, ref float newLimitL, ref float newLimitU, ref bool newInverted)
	{
		GUI.backgroundColor = color;
		if (GUILayout.Button("", GUILayout.Width(24), GUILayout.Height(24)))
		{
			newLimitL = limitL;
			newLimitU = limitU;
			newInverted = invert;
		}
	}
}
