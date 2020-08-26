using UnityEngine;

namespace FG
{
    [RequireComponent(typeof(MovementController))]
    public class PlayerInput : MonoBehaviour
    {
        private MovementController movement;
        private Camera playerCamera;

        private void Awake()
        {
            movement = GetComponent<MovementController>();
            playerCamera = Camera.main;
        }
        private void Update()
        {
            movement.movementInput.Set(Input.GetAxis("Turn"), Input.GetAxis("Drive"));
            movement.IsBraking = Input.GetButton("Brake");
        }
    }
}
