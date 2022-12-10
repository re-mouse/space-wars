using UnityEngine;

namespace SpaceWars.Input
{
    public class KeyBoardAndMouseInput : IPlayerInput
    {
        private Camera mainCamera;

        private Camera MainCamera
        {
            get
            {
                if (mainCamera == null)
                    mainCamera = Camera.main;
                return mainCamera;
            }
        }
        
        public Vector2 GetMovementDirection()
        {
            return new Vector2(UnityEngine.Input.GetAxis("Horizontal"), UnityEngine.Input.GetAxis("Vertical"));
        }

        public bool IsShootButtonPressed()
        {
            return UnityEngine.Input.GetKeyDown(KeyCode.Mouse0);
        }

        public Vector3 GetCursorWorldPosition()
        {
            Vector3 mousePos = UnityEngine.Input.mousePosition;
            return MainCamera.ScreenToWorldPoint(mousePos);
        }
    }
}