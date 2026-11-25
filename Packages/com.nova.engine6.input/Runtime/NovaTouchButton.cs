using UnityEngine;
using UnityEngine.EventSystems;

namespace Nova.Engine6.Input
{
    public sealed class NovaTouchButton : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private NovaInputReader target;

        public void OnPointerDown(PointerEventData eventData)
        {
            if (target != null) target.SetJumpPressed();
        }
    }
}
