using UnityEngine;
using UnityEngine.UI;

namespace ScoredProductions.ObjectFade {
#if UNITY_EDITOR

	using UnityEditor;

	[CustomEditor(typeof(UIFadeGraphic))]
	[CanEditMultipleObjects]
	public class UIFadeGraphicEditor : Editor {
		public override void OnInspectorGUI() {
			DrawDefaultInspector();

			UIFadeGraphic myScript = (UIFadeGraphic)target;

			switch (myScript.CurrentState) {
				case UIFadeBase.FadeStates.FadeIn:
					GUILayout.Label("Current State: Faded In");
					break;
				case UIFadeBase.FadeStates.FadeOut:
					GUILayout.Label("Current State: Faded Out");
					break;
			}
			if (myScript.Processing) {
				GUILayout.Label("Currently Processing");
			}
		}
	}

#endif

	[RequireComponent(typeof(Graphic))]
	public class UIFadeGraphic : UIFadeBase {

		private Graphic GraphicObject;

		public override void Awake() {
			GraphicObject = GetComponent<Graphic>();
		}

		public override void OnEnable() {
			InititateOnEnable(GraphicObject);
		}

		public override void FadeOut() {
			InitiateFadeOut(GraphicObject);
		}

		public override void FadeIn() {
			InitiateFadeIn(GraphicObject);
		}
	}
}