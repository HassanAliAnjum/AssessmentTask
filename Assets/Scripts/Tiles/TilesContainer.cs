using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace AT {
    public class TilesContainer : MonoBehaviour {
    
        public void OnTransformChildrenChanged() {
            int remainingTiles = transform.childCount;
            if (remainingTiles == 0) {
                EventManager.TriggerEvent (Event.AllTilesRemovedEvent);
            }
        }
    }
}