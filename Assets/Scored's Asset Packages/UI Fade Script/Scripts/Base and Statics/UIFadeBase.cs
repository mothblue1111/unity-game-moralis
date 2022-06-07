using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace ScoredProductions.ObjectFade {

	[DisallowMultipleComponent]
	public abstract class UIFadeBase : MonoBehaviour {

		[Tooltip("Speed the item fades.")]
		[Header("Item Fade Speed.")]
		public float Speed = 1;

		[Tooltip("True if you want to have it fade in when enabled or when the scene is loaded.")]
		[Header("Starting Fade Operation")]
		public FadeStates FadeOnStart = FadeStates.No_Action;

		[Header("Fade Complete Event")]
		public UnityEvent OnProcessComplete;

		public enum FadeStates {
			FadeOut,
			FadeIn,
			No_Action
		}

		/// <summary>
		/// Value to identify when the script is currently working
		/// </summary>
		public bool Processing { get; protected set; }

		/// <summary>
		/// Current fade state
		/// </summary>
		public FadeStates CurrentState { get; protected set; } = FadeStates.No_Action;

		protected IEnumerator<bool> task;

		protected Color OriginalColour;

		// Start is called before the first frame update
		private void Start() {
			UIFadeScript.RegisterObject(this);

			OnEnable();
		}

		public abstract void Awake();

		public abstract void OnEnable();

		public abstract void FadeIn();

		public abstract void FadeOut();

		public void ToggleFade() {
			switch (CurrentState) {
				case FadeStates.FadeOut:
					FadeIn();
					break;
				case FadeStates.FadeIn:
					FadeOut();
					break;
			}
		}

		protected IEnumerator CheckProcessing() {
			while (task != null && !task.Current) {
				Processing = true;
				yield return null;
			}

			Processing = false;
			task = null;
			OnProcessComplete.Invoke();
		}

		#region ### OnEnabled ###

		protected void InititateOnEnable(SpriteRenderer SpriteRendererObject) {
			if (SpriteRendererObject != null) {
				SpriteRendererObject.color = ProcessOnEnable(SpriteRendererObject.color);
			}
		}

		protected void InititateOnEnable(Graphic GraphicObject) {
			if (GraphicObject != null) {
				GraphicObject.color = ProcessOnEnable(GraphicObject.color);
			}
		}

		private Color ProcessOnEnable(Color color) {
			if (OriginalColour == Color.clear) {
				if (color != null) {
					OriginalColour = color;
				} else {
					return Color.clear;
				}
			}

			switch (FadeOnStart) {
				case FadeStates.FadeIn:
					FadeIn();
					CurrentState = FadeStates.FadeIn;
					break;
				case FadeStates.FadeOut:
					if (color != null) {
						color = Color.clear;
						CurrentState = FadeStates.FadeOut;
					}
					break;
			}

			return color;
		}

		#endregion

		#region ### Fade Out ###

		protected void InitiateFadeOut(SpriteRenderer SpriteRendererObject) {
			if (UIFadeScript.GlobalLock || Processing) {
				return;
			}

			CurrentState = FadeStates.FadeOut;

			if (SpriteRendererObject != null && SpriteRendererObject.enabled) {
				OriginalColour = SpriteRendererObject.color;
				StartCoroutine(task = UIFadeScript.FadeImage(SpriteRendererObject, OriginalColour, true, Speed));
				StartCoroutine(CheckProcessing());
			}
		}

		protected void InitiateFadeOut(Graphic GraphicObject) {
			if (UIFadeScript.GlobalLock || Processing) {
				return;
			}

			CurrentState = FadeStates.FadeOut;

			if (GraphicObject != null && GraphicObject.enabled) {
				OriginalColour = GraphicObject.color;
				StartCoroutine(task = UIFadeScript.FadeImage(GraphicObject, OriginalColour, true, Speed));
				StartCoroutine(CheckProcessing());
			}
		}

		#endregion

		#region ### Fade In ###

		protected void InitiateFadeIn(SpriteRenderer SpriteRendererObject) {
			if (UIFadeScript.GlobalLock || Processing) {
				return;
			}

			CurrentState = FadeStates.FadeIn;

			if (SpriteRendererObject != null && SpriteRendererObject.enabled) {
				StartCoroutine(task = UIFadeScript.FadeImage(SpriteRendererObject, OriginalColour, false, Speed));
				StartCoroutine(CheckProcessing());
			}
		}

		protected void InitiateFadeIn(Graphic GraphicObject) {
			if (UIFadeScript.GlobalLock || Processing) {
				return;
			}

			CurrentState = FadeStates.FadeIn;

			if (GraphicObject != null && GraphicObject.enabled) {
				StartCoroutine(task = UIFadeScript.FadeImage(GraphicObject, OriginalColour, false, Speed));
				StartCoroutine(CheckProcessing());
			}
		}

		#endregion
	}
}