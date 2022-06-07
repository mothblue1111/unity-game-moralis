using UnityEngine;

namespace ScoredProductions.ObjectFade {

#if UNITY_EDITOR
	using UnityEditor;

	[CustomEditor(typeof(UIFadeGetAllForFader))]
	public class UIFadeGetAllForFaderEditor : Editor {
		public override void OnInspectorGUI() {
			DrawDefaultInspector();

			UIFadeGetAllForFader myScript = (UIFadeGetAllForFader)target;
			if (GUILayout.Button("Get Children")) {
				myScript.GetAllChildren();
			}
		}
	}

#endif

	[ExecuteInEditMode]
	[RequireComponent(typeof(UIFadeGroupFader))]
	public class UIFadeGetAllForFader : MonoBehaviour {
		private UIFadeGroupFader GroupFader;

		void OnValidate() {
			UIFadeGroupFader groupfader = GetComponent<UIFadeGroupFader>();
			if (groupfader != null) {
				GroupFader = groupfader;
			}
		}

		public void GetAllChildren() {
			GroupFader.FadeScripts = GetComponentsInChildren<UIFadeBase>();
		}
	}
}