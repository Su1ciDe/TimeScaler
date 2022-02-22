using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace TimeScaler
{
#if UNITY_EDITOR
	[InitializeOnLoad]
#endif
	public static class TimeScaleEditor
	{
#if UNITY_EDITOR
		private struct TimeScaleType
		{
			public string timeScaleName;
			public float timeScaleAmount;

			public TimeScaleType(string name, float amount)
			{
				timeScaleName = name;
				timeScaleAmount = amount;
			}
		}

		static readonly TimeScaleType[] types =
		{
			new TimeScaleType("X 0.10", .1f),
			new TimeScaleType("X 0.25", .25f),
			new TimeScaleType("X 0.50", .5f),
			new TimeScaleType("X 1.00", 1f),
			new TimeScaleType("X 1.25", 1.25f),
			new TimeScaleType("X 1.50", 1.50f),
			new TimeScaleType("X 2.00", 2f),
		};

		public static class TimeScaleDropdown
		{
			[InitializeOnLoadMethod]
			private static void ShowTimeScaleDropDown()
			{
				UnityToolbarExtender.ToolbarExtender.LeftToolbarGUI.Add(() =>
				{
					EditorGUI.BeginChangeCheck();

					string[] dropdown = new string[types.Length + 1];
					dropdown[0] = "Time Scale X" + Time.timeScale;
					for (int i = 1; i <= types.Length; i++)
						dropdown[i] = types[i - 1].timeScaleName;

					EditorGUI.BeginDisabledGroup(!EditorApplication.isPlaying);
					int value = EditorGUILayout.Popup(0, dropdown, "Dropdown", GUILayout.Width(125));
					EditorGUI.EndDisabledGroup();

					if (EditorGUI.EndChangeCheck())
					{
						if (value > 0)
						{
							// EditorWindow.GetWindow(typeof(SceneView).Assembly.GetType("UnityEditor.GameView")).ShowNotification(new GUIContent("Slow Motion " + types[value].slowMotionAmount));
							foreach (SceneView scene in SceneView.sceneViews)
								scene.ShowNotification(new GUIContent("Time Scale: " + types[value - 1].timeScaleAmount));
							Time.timeScale = types[value - 1].timeScaleAmount;
						}
					}
				});
			}
#endif
		}
	}
}