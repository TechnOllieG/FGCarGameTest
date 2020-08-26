using System;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public float aimRotationSpeed = 5f;
    public float roundsPerMinute = 60f;
    public float shootDistance = 500f;
    public LayerMask hitLayer;

    [System.NonSerialized] public Vector2 aimAtPosition;

    private float _timeBetweenShots;
    private float _timeSinceLastShot;
    private Transform _weaponSocket;
    private ParticleSystem _weaponParticleSystem;
    private Coroutine _shootingCoroutine;

    public bool IsShooting { get; private set; } = false;

    public bool IsAimable
    {
        get
        {
            return !Mathf.Approximately(aimRotationSpeed, 0f);
        }
    }

    private void Awake()
    {
        
    }
}
