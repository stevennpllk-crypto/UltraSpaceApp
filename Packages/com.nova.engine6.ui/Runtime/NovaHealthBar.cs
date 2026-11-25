using UnityEngine;
using UnityEngine.UI;

namespace Nova.Engine6.UI
{
    [DisallowMultipleComponent]
    public sealed class NovaHealthBar : MonoBehaviour
    {
        [SerializeField] private NovaHealth target;
        [SerializeField] private Slider slider;
        [SerializeField] private Image fillImage;
        [SerializeField] private Gradient fillGradient;

        private void Awake()
        {
            if (slider == null) slider = GetComponent<Slider>();
            Refresh();
        }

        private void Update()
        {
            Refresh();
        }

        public void SetTarget(NovaHealth health) { target = health; Refresh(); }

        private void Refresh()
        {
            if (target == null) return;
            float value = target.Normalized;
            if (slider != null) slider.value = value;
            if (fillImage != null && fillGradient != null)
                fillImage.color = fillGradient.Evaluate(value);
        }
    }
}
