using UnityEngine;

#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED

using UnityEngine.InputSystem;

#endif

namespace StarterAssets
{
    public class StarterAssetsInputs : MonoBehaviour
    {
        private ThirdPersonController tpc;
        private bool forwardJumping;

        [Header("Character Input Values")]
        public Vector2 move;

        public Vector2 look;
        public bool jump;
        public bool sprint;

        [Header("Movement Settings")]
        public bool analogMovement;

#if !UNITY_IOS || !UNITY_ANDROID

        [Header("Mouse Cursor Settings")]
        public bool cursorLocked = true;

        public bool cursorInputForLook = true;
#endif

        private void Start()
        {
            tpc = GetComponent<ThirdPersonController>();
        }

#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED

        public void OnMove(InputAction.CallbackContext context)
        {
            MoveInput(context.ReadValue<Vector2>());
        }

        public void OnLook(InputAction.CallbackContext context)
        {
            if (cursorInputForLook)
            {
                LookInput(context.ReadValue<Vector2>());
            }
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            JumpInput(context.performed);
        }

        public void OnSprint(InputAction.CallbackContext context)
        {
            SprintInput(context.performed);
        }

        public void OnChangeJumpMode(InputAction.CallbackContext context)
        {
            if (context.performed)
                tpc.ChangeJumpMode(context.action.actionMap.name);
        }

        public void OnForwardJump(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                tpc.ForwardJumpStart();
            }

            if (context.canceled && forwardJumping)
            {
                tpc.ForwardJump(context.time - context.startTime);
            }
            else if (context.canceled)
            {
                tpc.ForwardJumpCancel();
            }

            forwardJumping = context.performed;
        }

#else
	// old input sys if we do decide to have it (most likely wont)...
#endif

        public void MoveInput(Vector2 newMoveDirection)
        {
            move = newMoveDirection;
        }

        public void LookInput(Vector2 newLookDirection)
        {
            look = newLookDirection;
        }

        public void JumpInput(bool newJumpState)
        {
            jump = newJumpState;
        }

        public void SprintInput(bool newSprintState)
        {
            sprint = newSprintState;
        }

#if !UNITY_IOS || !UNITY_ANDROID

        private void OnApplicationFocus(bool hasFocus)
        {
            SetCursorState(cursorLocked);
        }

        private void SetCursorState(bool newState)
        {
            Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
        }

#endif
    }
}