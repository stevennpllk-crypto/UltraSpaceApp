using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Nova.Engine6.Input
{
    [DisallowMultipleComponent]
    public sealed class NovaVirtualJoystick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
    {
        [SerializeField] private RectTransform background;
        [SerializeField] private RectTransform handle;
        [SerializeField] private NovaInputReader target;
        [SerializeField, Range(0.1f, 1f)] private float handleRange = 0.7f;

        private Canvas canvas;

        private void Awake()
        {
            canvas = GetComponentInParent<Canvas>();
            if (background == null) background = transform as RectTransform;
        }

        public void OnPointerDown(PointerEventData eventData) { OnDrag(eventData); }

        public void OnDrag(PointerEventData eventData)
        {
            if (background == null || handle == null) return;

            Camera eventCamera = canvas != null && canvas.renderMode != RenderMode.ScreenSpaceOverlay
                ? canvas.worldCamera : null;

            if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(
                background, eventData.position, eventCamera, out Vector2 localPoint))
                return;

            Rect rect = background.rect;
            Vector2 half = rect.size * 0.5f;
            Vector2 normalized = new Vector2(
                half.x > 0f ? localPoint.x / half.x : 0f,
                half.y > 0f ? localPoint.y / half.y : 0f);
            normalized = Vector2.ClampMagnitude(normalized, 1f);

            handle.anchoredPosition = new Vector2(normalized.x * half.x, normalized.y * half.y) * handleRange;
            if (target != null) target.SetMoveInput(normalized);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (handle != null) handle.anchoredPosition = Vector2.zero;
            if (target != null) target.SetMoveInput(Vector2.zero);
        }
    }
}
