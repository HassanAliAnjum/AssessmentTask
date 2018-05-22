using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace AT {
	public class StartSceneHandler : MonoBehaviour {

		[SerializeField]
		Button characterButton;

		void OnEnable () {
			characterButton.onClick.AddListener (HandleCharacterClick);

			EventManager.StartListening (Event.AllTilesColorChangeEvent, HandleAllTilesColorChangeEvent);
		}

		void OnDisable () {
			characterButton.onClick.RemoveListener (HandleCharacterClick);

			EventManager.StopListening (Event.AllTilesColorChangeEvent, HandleAllTilesColorChangeEvent);
		}

		void HandleCharacterClick () {
			EventManager.TriggerEvent (Event.CharacterClickEvent);
		}

		void HandleAllTilesColorChangeEvent () {
			SceneManager.LoadScene (1);
		}
	}
}