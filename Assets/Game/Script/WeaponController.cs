using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;

public class WeaponController : MonoBehaviour
{
    public float aimRotationSpeed = 5f;
    public float roundsPerMinute = 60f;
    public float shootDistance = 500f;
    public LayerMask hitLayer;

    [System.NonSerialized] public Vector2 aimAtPosition;

    private float _timeBetweenShots;
    private float _timeAtLastShot;
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
        _weaponSocket = transform.Find("Graphics/weaponSocket");
        Assert.IsNotNull(_weaponSocket, "_weaponSocket == null");

        _weaponParticleSystem = GetComponentInChildren<ParticleSystem>();

        _timeBetweenShots = 60f / roundsPerMinute;
    }

    private void LateUpdate()
    {
        if (IsAimable)
        {
            AimWeapon();
        }
    }
    private void AimWeapon()
    {
        Vector2 direction = aimAtPosition - (Vector2)_weaponSocket.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        _weaponSocket.rotation = Quaternion.Slerp(_weaponSocket.rotation, rotation, aimRotationSpeed * Time.deltaTime);
    }

    public void StartShooting()
    {
        if (IsShooting)
        {
            return;
        }
        IsShooting = true;
        _shootingCoroutine = StartCoroutine(Shooting(Mathf.Max(0f, _timeAtLastShot + _timeBetweenShots - Time.time)));
    }

    public void StopShooting()
    {
        IsShooting = false;
        StopCoroutine(_shootingCoroutine);
    }
    
    private IEnumerator Shooting(float startDelay)
    {
        yield return new WaitForSeconds(startDelay);
        while (true)
        {
            _weaponParticleSystem.Play();
            FireShot();
            _timeAtLastShot = Time.time;
            yield return new WaitForSeconds(_timeBetweenShots);
        }
    }

    private void FireShot()
    {
        Debug.DrawRay(_weaponSocket.position, _weaponSocket.right * shootDistance, Color.red, 0.5f);
        // todo fire bullet
        // todo fix fire rate bug?
    }
}
