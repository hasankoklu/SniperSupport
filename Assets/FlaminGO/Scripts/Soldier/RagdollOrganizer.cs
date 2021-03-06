using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class RagdollOrganizer : MonoBehaviour
{
    public int targetLayerIndex;
    void Start()
    {
        
    }

    [Button]
    public void SetLayer()
    {
        foreach (Rigidbody _rigidbody in GetComponentsInChildren<Rigidbody>())
        {
            _rigidbody.gameObject.layer = targetLayerIndex;
        }
    }
    [Button]
    public void IsKżnematic()
    {
        foreach (Rigidbody _rigidbody in GetComponentsInChildren<Rigidbody>())
        {
            _rigidbody.isKinematic = true;
        }
    }
    [Button]
    public void SetTag(string _tag)
    {
        foreach (Rigidbody _rigidbody in GetComponentsInChildren<Rigidbody>())
        {
            _rigidbody.gameObject.tag = _tag;
        }
    }
    [Button]
    public void SetCollisionType()
    {
        foreach (Rigidbody _rigidbody in GetComponentsInChildren<Rigidbody>())
        {
            _rigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        }
    }
}
