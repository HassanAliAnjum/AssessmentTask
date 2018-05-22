using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AT {
	public class CharacterController : MonoBehaviour {
		
		private Animator animator;
		private CharacterAnimationState animationState;

		void Awake () {
			animator = gameObject.GetComponent<Animator> ();
			DontDestroyOnLoad (gameObject);
		}

		void OnEnable () {
			EventManager.StartListening (Event.CharacterClickEvent, HandleCharacterClickEvent);
		}

		void OnDisable () {
			EventManager.StopListening (Event.CharacterClickEvent, HandleCharacterClickEvent);
		}

		public void OnEndAnimation (string animationClip) {
			animator.SetBool (animationClip, false);
			animationState = CharacterAnimationState.Idle;
		}

		public void HandleCharacterClickEvent () {
			if (animationState == CharacterAnimationState.Idle) {
				Array values = Enum.GetValues(typeof(CharacterAnimationState));
                System.Random random = new System.Random();
				animationState = (CharacterAnimationState) values.GetValue(random.Next(1, values.Length));
				animator.SetBool (animationState.ToString (), true);
			}
		}
	}

	public enum CharacterAnimationState {
		Idle,
		Walk,
		Shake
	}
}