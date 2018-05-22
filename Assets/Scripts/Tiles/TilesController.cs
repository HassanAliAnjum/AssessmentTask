using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AT {
	public class TilesController : MonoBehaviour {
		
		private Image[] tiles;
		private bool isInTransition = false;
		private Color tileColor = new Color (0.9294118f, 0.4509804f, 0.2862745f);

		void Awake () {
			tiles = gameObject.GetComponentsInChildren<Image> ();
			AddTileListeners ();
		}

		void HandleTileClick () {
			if (isInTransition)
				return;

			ChangeTilesColor ();
		}

		void AddTileListeners () {
			foreach (Image tile in tiles) {
				tile.GetComponent<Button> ().onClick.AddListener (HandleTileClick);
			}
		}

		void ChangeTilesColor () {
			isInTransition = true;
			StartCoroutine (ChangeTilesColorInternal ());
		}

		IEnumerator ChangeTilesColorInternal () {
			foreach (Image tile in tiles) {
				tile.color = tileColor;
				yield return new WaitForSeconds (1);
			}
			isInTransition = false;
			EventManager.TriggerEvent (Event.AllTilesColorChangeEvent);
		}
	}
}