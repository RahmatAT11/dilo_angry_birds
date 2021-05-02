﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    public enum BirdState { Idle, Thrown};
    public GameObject Parent;
    public Rigidbody2D Rigidbody;
    public CircleCollider2D Collider;

    private BirdState _state;
    private float _minVelocity = 0.05f;
    private bool _flagDestroy = false;

    void Start()
    {
        Rigidbody.bodyType = RigidbodyType2D.Kinematic;
        Collider.enabled = false;
        _state = BirdState.Idle;
    }

    void FixedUpdate()
    {
        if (_state == BirdState.Idle
            && Rigidbody.velocity.sqrMagnitude >= _minVelocity)
        {
            _state = BirdState.Thrown;
        }

        if (_state == BirdState.Thrown &&
            Rigidbody.velocity.sqrMagnitude < _minVelocity && !_flagDestroy)
        {
            // Hancurkan gameObject setelah 2 detik
            // Jika kecepatannya sudah kurang dari batas minimum
            _flagDestroy = true;
            StartCoroutine(DestroyAfter(2));
        }
    }

    private IEnumerator DestroyAfter(float second)
    {
        yield return new WaitForSeconds(second);
        Destroy(gameObject);
    }

    public void MoveTo(Vector2 target, GameObject parent)
    {
        gameObject.transform.SetParent(parent.transform);
        gameObject.transform.position = target;
    }

    public void Shoot(Vector2 velocity, float distance, float speed)
    {
        Collider.enabled = true;
        Rigidbody.bodyType = RigidbodyType2D.Dynamic;
        Rigidbody.velocity = velocity * speed * distance;
    }
}
