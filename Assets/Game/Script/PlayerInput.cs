using UnityEngine;

namespace FG
{
    [RequireComponent(typeof(MovementController))]
    public class PlayerInput : MonoBehaviour
    {
        private MovementController _movement;
        private WeaponController _weapon;
        private Camera _playerCamera;

        #region InputIDs
        private const string _turnID = "Turn";
        private const string _driveID = "Drive";
        private const string _brakeID = "Brake";
        private const string _fireID = "Fire1";
        #endregion InputIDs
        

        private void Awake()
        {
            _movement = GetComponent<MovementController>();
            _playerCamera = Camera.main;
            _weapon = GetComponent<WeaponController>();
        }
        private void Update()
        {
            _movement.movementInput.Set(Input.GetAxis("Turn"), Input.GetAxis("Drive"));
            _movement.IsBraking = Input.GetButton("Brake");
            _weapon.aimAtPosition = _playerCamera.ScreenToWorldPoint(Input.mousePosition);

            if (Input.GetButtonDown(_fireID))
            {
                _weapon.StartShooting();
            }
            else if(Input.GetButtonUp(_fireID))
            {
                _weapon.StopShooting();
            }
        }
    }
}
