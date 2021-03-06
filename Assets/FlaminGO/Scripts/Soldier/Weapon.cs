using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private void Start()
    {
        if (!TryGetComponent(out MeshCollider _meshCollider))
        {
            _meshCollider = gameObject.AddComponent<MeshCollider>();
        }
        _meshCollider.convex = true;
    }
    public void Throw(float _delay)
    {
        StartCoroutine(ThrowEnum(_delay));
    }

    IEnumerator ThrowEnum(float _delay)
    {
        yield return new WaitForSeconds(_delay);
        transform.SetParent(null);

        if (!TryGetComponent(out Rigidbody _rigidbody))
            _rigidbody = gameObject.AddComponent<Rigidbody>();

        _rigidbody.AddForce(transform.forward * 10f);
    }
}
