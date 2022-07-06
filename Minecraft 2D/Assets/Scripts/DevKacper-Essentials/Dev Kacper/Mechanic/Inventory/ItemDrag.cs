using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DevKacper.Mechanic
{
    [RequireComponent(typeof(CanvasGroup))]
    public class ItemDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [SerializeField] protected float OnDragAlphaValue = 0.75f;
        [SerializeField] protected Transform parent = null;

        protected Canvas canvas = null;
        protected RectTransform rectTransform = null;
        protected CanvasGroup canvasGroup = null;
        protected Vector3 startingPosition;

        protected virtual void Start()
        {
            canvas = FindObjectOfType<Canvas>();
            rectTransform = GetComponent<RectTransform>();
            canvasGroup = GetComponent<CanvasGroup>();
            startingPosition = rectTransform.anchoredPosition;

            if(parent == null)
            {
                parent = rectTransform.parent;
            }
        }

        public virtual void OnBeginDrag(PointerEventData eventData)
        {
            rectTransform.SetParent(canvas.transform);
            canvasGroup.alpha = OnDragAlphaValue;
            canvasGroup.blocksRaycasts = false;
        }

        public virtual void OnDrag(PointerEventData eventData)
        {
            rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        }

        public virtual void OnEndDrag(PointerEventData eventData)
        {
            rectTransform.SetParent(parent);
            rectTransform.anchoredPosition = startingPosition;
            canvasGroup.alpha = 1f;
            canvasGroup.blocksRaycasts = true;
        }
    }
}



