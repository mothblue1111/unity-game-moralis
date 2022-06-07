using UnityEngine;

namespace ScoredProductions.ObjectFade {
#if UNITY_EDITOR

	using UnityEditor;

	[CustomEditor(typeof(UIFadeSpriteRenderer))]
	[CanEditMultipleObjects]
	public class UIFadeSpriteRendererEditor : Editor {
		public override void OnInspectorGUI() {
			DrawDefaultInspector();

			UIFadeSpriteRenderer myScript = (UIFadeSpriteRenderer)target;

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
	[RequireComponent(typeof(SpriteRenderer))]
	public class UIFadeSpriteRenderer : UIFadeBase {

		private SpriteRenderer SpriteRendererObject;

		public override void Awake() {
			SpriteRendererObject = GetComponent<SpriteRenderer>();
		}

		public override void OnEnable() {
			InititateOnEnable(SpriteRendererObject);
		}

		public override void FadeOut() {
			InitiateFadeOut(SpriteRendererObject);
		}

		public override void FadeIn() {
			InitiateFadeIn(SpriteRendererObject);
		}
	}
}