using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

namespace AT {
	public class DragSceneHandler : MonoBehaviour {
		[SerializeField]
		RawImage image;

		[SerializeField]
		Button characterButton;

		void OnEnable () {
			characterButton.onClick.AddListener (HandleCharacterClick);

			EventManager.StartListening (Event.AllTilesRemovedEvent, HandleAllTilesRemovedEvent);
		}

		void OnDisable () {
			characterButton.onClick.RemoveListener (HandleCharacterClick);

			EventManager.StopListening (Event.AllTilesRemovedEvent, HandleAllTilesRemovedEvent);
		}

		void HandleCharacterClick () {
			EventManager.TriggerEvent (Event.CharacterClickEvent);
		}

		void HandleAllTilesRemovedEvent () {
			#if UNITY_EDITOR
			string path = Path.Combine(Application.dataPath + "/StreamingAssets", "sketch");
			#elif UNITY_ANDROID
			string path = Path.Combine("jar:file://" + Application.dataPath + "!/assets/", "sketch");
			#elif UNITY_IOS
			string path = Path.Combine(Application.dataPath + "/Raw", "sketch");
			#endif

			 var myLoadedAssetBundle = AssetBundle.LoadFromFile(path);
			if (myLoadedAssetBundle == null) {
				Debug.Log("Failed to load AssetBundle!");
				return;
			}
			image.gameObject.SetActive (true);
			image.texture = myLoadedAssetBundle.LoadAsset<Texture>("sketch");
		}
	}
}