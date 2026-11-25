using UnityEngine;

namespace Nova.Engine6.Input
{
    [DisallowMultipleComponent]
    public sealed class NovaInputReader : MonoBehaviour
    {
        [Header("Keyboard (legacy input API)")]
        [SerializeField] private bool readKeyboard = true;
        [SerializeField] private KeyCode forwardKey = KeyCode.W;
        [SerializeField] private KeyCode backwardKey = KeyCode.S;
        [SerializeField] private KeyCode leftKey = KeyCode.A;
        [SerializeField] private KeyCode rightKey = KeyCode.D;
        [SerializeField] private KeyCode jumpKey = KeyCode.Space;

        [Header("External / UI input")]
        [SerializeField] private Vector2 externalMove;
        [SerializeField] private bool externalJump;

        public Vector2 Move { get; private set; }
        public bool JumpPressed { get; private set; }

        private void Update()
        {
            Vector2 keyboard = Vector2.zero;
            if (readKeyboard)
            {
                if (UnityEngine.Input.GetKey(forwardKey)) keyboard.y += 1f;
                if (UnityEngine.Input.GetKey(backwardKey)) keyboard.y -= 1f;
                if (UnityEngine.Input.GetKey(rightKey)) keyboard.x += 1f;
                if (UnityEngine.Input.GetKey(leftKey)) keyboard.x -= 1f;
            }

            Move = Vector2.ClampMagnitude(keyboard + externalMove, 1f);
            JumpPressed = (readKeyboard && UnityEngine.Input.GetKeyDown(jumpKey)) || externalJump;
            externalJump = false;
        }

        public void SetMoveInput(Vector2 value)
        {
            externalMove = Vector2.ClampMagnitude(value, 1f);
        }

        public void SetJumpPressed()
        {
            externalJump = true;
        }

        public void ClearExternalInput()
        {
            externalMove = Vector2.zero;
            externalJump = false;
        }
    }
}
