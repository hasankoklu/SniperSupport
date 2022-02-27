using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using Dreamteck.Splines;


public class BulletController : MonoBehaviour
{
    public bool isRpg;
    public float damage;
    public GameObject impactParticle;
    public bool playerBullet;
    public float hsDelay = 1f;
    public Transform target;
    bool isFirstPositionSetted;

    Vector3 defaultScale;

    private void Start()
    {
        transform.localScale = Vector3.zero;
    }
    private void FixedUpdate()
    {
        if (!target || isFirstPositionSetted)
            return;

        if (transform.parent != null)
        {
            isFirstPositionSetted = true;

            Shoot();
        }
    }

    void Shoot()
    {

        float _random = Random.Range(1.25f, 1.65f);

        transform.LookAt(target.transform.position + Vector3.up * _random);

        transform.DOScale(Vector3.one, .2f);

        if (!isRpg)
        {
            transform.DOMove(target.transform.position + Vector3.up * _random, .1f).SetEase(Ease.Linear);
        }
        else
        {
            //transform.DOLookAt(target.transform.position + Vector3.up * _random, 0f);
            transform.DOMove(target.transform.position + Vector3.up * _random, 1f).SetEase(Ease.Linear);
        }
        transform.SetParent(null);
    }

    private void OnCollisionEnter(Collision other)
    {

        if (isRpg)
            ObjectPool.instance.SpawnFromPool("RPGExplode", other.transform.position, Quaternion.identity);

        if (other.gameObject.CompareTag("Enemy") && !playerBullet)
        {
            SoldierController _soldierController = other.gameObject.GetComponentInParent<SoldierController>();
            if (_soldierController)
            {
                _soldierController.lastHittedBullet = gameObject;
                _soldierController.TakeHit(damage);
            }
        }
        if (other.gameObject.CompareTag("Enemy") && playerBullet)
        {
            SoldierController _soldierController = other.gameObject.GetComponentInParent<SoldierController>();
            if (_soldierController)
            {
                _soldierController.gameObject.transform.SetParent(null, true);
                _soldierController.lastHittedBullet = gameObject;
                _soldierController.TakeHit(damage);
            }
            Shooter _shooter = FindObjectOfType<Shooter>();
            if (_shooter.headShot)
            {
                _shooter.headShot.GetComponentInChildren<TextMeshProUGUI>().enabled = false;
                _shooter.headShot.SetActive(true);
            }
        }
        if (playerBullet && other.gameObject.CompareTag("Head"))
        {
            SoldierController _soldierController = other.gameObject.GetComponentInParent<SoldierController>();
            if (_soldierController)
            {
                _soldierController.gameObject.transform.SetParent(null, true);
                _soldierController.lastHittedBullet = gameObject;
                _soldierController.TakeHit(damage * 20000f);
            }
        }
        gameObject.SetActive(false);
    }
}