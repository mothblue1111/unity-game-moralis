using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.Events;

namespace ScoredProductions.ObjectFade {

	public class UIFadeGroupFader : MonoBehaviour {

		[Tooltip("The fader will not initiate any fade while any of the listed faders are processing.")]
		public bool WaitForProcessing = false;

		public UIFadeBase[] FadeScripts;

		[Header("Fade Complete Event")]
		public UnityEvent OnProcessComplete;

		public bool AllFadedOut => FadeScripts.All(e => !e.isActiveAndEnabled || (!e.Processing && e.CurrentState == UIFadeBase.FadeStates.FadeOut));

		public bool AllFadedIn => FadeScripts.All(e => !e.isActiveAndEnabled || (!e.Processing && e.CurrentState == UIFadeBase.FadeStates.FadeIn));

		public bool AnyProcessing => FadeScripts.Any(e => e.Processing);

		protected IEnumerator<bool> task;

		#region ### Fade Toggle ###

		public void FadeAllToggle() {
			if (WaitForProcessing && AnyProcessing) {
				return;
			}

			if (task == null || task.Current) {
				StartCoroutine(task = RoutineFadeAllToggle());
			}
		}

		private IEnumerator<bool> RoutineFadeAllToggle() {
			foreach (UIFadeBase x in FadeScripts) {
				if (x.isActiveAndEnabled) {
					x.ToggleFade();
				}
			}

			while (FadeScripts.Any(e => e.Processing)) {
				yield return false;
			}

			yield return true;
		}

		#endregion

		#region ### Fade Out ###

		public void FadeAllOut() {
			if (WaitForProcessing && AnyProcessing) {
				return;
			}

			if (task == null || task.Current) {
				StartCoroutine(task = RoutineFadeAllOut());
			}
		}

		private IEnumerator<bool> RoutineFadeAllOut() {
			foreach (UIFadeBase x in FadeScripts) {
				if (x.isActiveAndEnabled && x.CurrentState == UIFadeBase.FadeStates.FadeIn) {
					x.FadeOut();
				}
			}

			while (FadeScripts.Any(e => e.Processing)) {
				yield return false;
			}

			yield return true;
		}

		#endregion

		#region ### Fade In ###

		public void FadeAllIn() {
			if (WaitForProcessing && AnyProcessing) {
				return;
			}

			if (task == null || task.Current) {
				StartCoroutine(task = RoutineFadeAllIn());
			}
		}

		private IEnumerator<bool> RoutineFadeAllIn() {
			foreach (UIFadeBase x in FadeScripts) {
				if (x.isActiveAndEnabled && x.CurrentState == UIFadeBase.FadeStates.FadeOut) {
					x.FadeIn();
				}
			}

			while (FadeScripts.Any(e => e.Processing)) {
				yield return false;
			}

			yield return true;
		}

		#endregion

	}
}