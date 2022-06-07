using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.UI;

namespace ScoredProductions.ObjectFade {

	public class UIFadeSingleton : MonoBehaviour {

		public void StartManageAllFadeOut() => StartCoroutine(ManageAllFadeOut());
		private IEnumerator ManageAllFadeOut() {
			UIFadeScript.GlobalLock = true;
			while (!UIFadeScript.AllFadedOut) {
				yield return null;
			}
			UIFadeScript.GlobalLock = false;
		}

		public void StartManageAllFadeIn() => StartCoroutine(ManageAllFadeIn());
		private IEnumerator ManageAllFadeIn() {
			UIFadeScript.GlobalLock = true;
			while (!UIFadeScript.AllFadedIn) {
				yield return null;
			}
			UIFadeScript.GlobalLock = false;
		}
	}

	public static class UIFadeScript {
		private static GameObject _object;
		private static UIFadeSingleton MonoInstance;

		private static void CheckInstance() {
			if (_object == null) {
				_object = new GameObject("Fade Instance");
				MonoInstance = _object.AddComponent<UIFadeSingleton>();
			} else {
				UIFadeSingleton fadeManager = _object.GetComponent<UIFadeSingleton>();
				if (fadeManager == null) {
					MonoInstance = _object.AddComponent<UIFadeSingleton>();
				} else {
					MonoInstance = fadeManager;
				}
			}
		}

		public static List<UIFadeBase> AllRegisteredScripts = new List<UIFadeBase>();

		public static void RegisterObject(params UIFadeBase[] obj) {
			CheckInstance();
			foreach (UIFadeBase x in obj) {
				if (!AllRegisteredScripts.Contains(x)) {
					AllRegisteredScripts.Add(x);
				}
			}
		}

		public static void FadeAllIn() {
			AllRegisteredScripts.RemoveAll(e => e == null);
			AllRegisteredScripts.ForEach(e => {
				if (e.CurrentState == UIFadeBase.FadeStates.FadeOut && e.gameObject.activeInHierarchy) {
					e.FadeIn();
				}
			});
			if (MonoInstance == null) {
				CheckInstance();
			}
			MonoInstance.StartManageAllFadeIn();
		}

		public static void FadeAllOut() {
			AllRegisteredScripts.RemoveAll(e => e == null);
			AllRegisteredScripts.ForEach(e => {
				if (e.CurrentState == UIFadeBase.FadeStates.FadeIn && e.gameObject.activeInHierarchy) {
					e.FadeOut();
				}
			});
			if (MonoInstance == null) {
				CheckInstance();
			}
			MonoInstance.StartManageAllFadeOut();
		}

		public static bool AllFadedOut => AllRegisteredScripts.Where(e => e.gameObject.activeInHierarchy).All(e => !e.Processing && e.CurrentState == UIFadeBase.FadeStates.FadeOut);
		public static bool AllFadedIn => AllRegisteredScripts.Where(e => e.gameObject.activeInHierarchy).All(e => !e.Processing && e.CurrentState == UIFadeBase.FadeStates.FadeIn);

		public static bool GlobalLock = false;

		// I would use dynamic here and other places but dynamic isnt supported in older Unity and is very selective when it wants to work...

		public static IEnumerator<bool> FadeImage(Graphic img, Color basecolour, bool FadeOut, float speed = 1) {
			// fade from opaque to transparent
			if (FadeOut) {
				// loop over 1 second backwards
				for (float i = basecolour.a; i >= 0; i -= Time.deltaTime * speed) {
					// set color with i as alpha
					img.color = new Color(basecolour.r, basecolour.g, basecolour.b, i);
					yield return false;
				}
				img.color = new Color(basecolour.r, basecolour.g, basecolour.b, 0);
			}
			// fade from transparent to opaque
			else {
				// loop over 1 second
				for (float i = 0; i <= basecolour.a; i += Time.deltaTime * speed) {
					// set color with i as alpha
					img.color = new Color(basecolour.r, basecolour.g, basecolour.b, i);
					yield return false;
				}
				img.color = new Color(basecolour.r, basecolour.g, basecolour.b, basecolour.a);
			}
			yield return true;
		}


		public static IEnumerator<bool> FadeImage(SpriteRenderer img, Color basecolour, bool FadeOut, float speed = 1) {
			// fade from opaque to transparent
			if (FadeOut) {
				// loop over 1 second backwards
				for (float i = basecolour.a; i >= 0; i -= Time.deltaTime * speed) {
					// set color with i as alpha
					img.color = new Color(basecolour.r, basecolour.g, basecolour.b, i);
					yield return false;
				}
				img.color = new Color(basecolour.r, basecolour.g, basecolour.b, 0);
			}
			// fade from transparent to opaque
			else {
				// loop over 1 second
				for (float i = 0; i <= basecolour.a; i += Time.deltaTime * speed) {
					// set color with i as alpha
					img.color = new Color(basecolour.r, basecolour.g, basecolour.b, i);
					yield return false;
				}
				img.color = new Color(basecolour.r, basecolour.g, basecolour.b, basecolour.a);
			}
			yield return true;
		}
	}
}