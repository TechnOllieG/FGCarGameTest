﻿using UnityEngine;
using System;

namespace FG
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class MovementController : MonoBehaviour
    {
        public float maxForwardSpeed = 8f;
        public float maxReverseSpeed = 4f;
        public float baseAcceleration = 20f;
        public float baseTurnSpeed = 100f;
        public float speedToTurnTolerance = 0.1f;
        public float maxTurnPower = 3f;

        [Tooltip("Multiplier applied to linear drag of rigidbody")]
        public float brakePower = 2f;

        [NonSerialized] public Vector2 movementInput;
        [NonSerialized] public float forwardSpeed;
        [NonSerialized] public float desiredForwardSpeed;

        [NonSerialized] public float turnSpeed;
        [NonSerialized] public float desiredTurnSpeed;

        private float initialDrag;
        private Rigidbody2D rb;
        private Transform _transform;

        public bool IsBraking { get; set; } = false;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            _transform = transform;
            initialDrag = rb.drag;
        }

        private void FixedUpdate()
        {
            if(movementInput.sqrMagnitude > 1f)
            {
                movementInput.Normalize();
            }
            if(IsBraking)
            {
                rb.drag = Mathf.Lerp(rb.drag, rb.drag * brakePower, Time.fixedDeltaTime);
            }
            else
            {
                rb.drag = initialDrag;
                MoveForward();
            }

            Turn();
        }

        private void MoveForward()
        {
            desiredForwardSpeed = movementInput.y * (movementInput.y > 0f ? maxForwardSpeed : maxReverseSpeed);
            forwardSpeed = Mathf.MoveTowards(forwardSpeed, desiredForwardSpeed, baseAcceleration * Time.fixedDeltaTime);
            rb.AddForce(_transform.right * forwardSpeed);
        }

        private void Turn()
        {
            if(Mathf.Approximately
                (
                Mathf.Clamp(rb.velocity.sqrMagnitude - speedToTurnTolerance, 0f, 1f),
                0f
                ))
            {
                return;
            }

            desiredTurnSpeed = movementInput.x * (movementInput.x < 0f ? maxTurnPower : -maxTurnPower);
            turnSpeed = Mathf.MoveTowards(turnSpeed, desiredTurnSpeed, baseTurnSpeed * Time.fixedDeltaTime);
            rb.AddTorque(desiredTurnSpeed);
        }
    }
}
