using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace AT {
    public class DraggableTile : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler {
        public RectTransform draggableArea;
        private readonly List<RaycastResult> _raycastResults = new List<RaycastResult> ();
        private RectTransform _draggingObject;
        private Vector2 _draggingObjectOriginalSize;
        private bool _isDragging;
        private RectTransform _rect;

        void Awake () {
            _rect = GetComponent<RectTransform> ();
        }

        #region IBeginDragHandler Members

        public void OnBeginDrag (PointerEventData eventData) {

            GameObject clone = (GameObject) Instantiate (gameObject);
            _draggingObject = clone.GetComponent<RectTransform> ();

            _draggingObjectOriginalSize = gameObject.GetComponent<RectTransform> ().rect.size;
            _draggingObject.SetParent (draggableArea, false);
            _draggingObject.SetAsLastSibling ();

            RefreshSizes ();
            _isDragging = true;
        }

        #endregion

        #region IDragHandler Members

        public void OnDrag (PointerEventData eventData) {
            if (!_isDragging)
                return;

            _draggingObject.position = eventData.position;

            EventSystem.current.RaycastAll (eventData, _raycastResults);
            for (int i = 0; i < _raycastResults.Count; i++) {
                RemoveableTile ele = _raycastResults[i].gameObject.GetComponent<RemoveableTile> ();
                if (ele != null && ele.gameObject.CompareTag (gameObject.tag)) {
                    Destroy (_draggingObject.gameObject);
                    Destroy (ele.gameObject);
                    _isDragging = false;
                    return;
                }
            }

            RefreshSizes ();
        }

        #endregion

        #region IEndDragHandler Members

        public void OnEndDrag (PointerEventData eventData) {
            _isDragging = false;

            if (_draggingObject != null) {
                Destroy (_draggingObject.gameObject);
            }
        }

        #endregion

        private void RefreshSizes () {
            Vector2 size = _draggingObjectOriginalSize;

            _draggingObject.sizeDelta = size;
        }
    }
}