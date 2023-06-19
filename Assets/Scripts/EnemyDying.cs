using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDying : MonoBehaviour
{
    private const string _isDying = "IsDying";

    private Animator _animator;
    private GameObject _enemy;
    private float _tossForse = 25;
    private float _dyingTime = 0.5f;

    private void Start()
    {
        _enemy = this.transform.parent.gameObject;
        _animator = _enemy.GetComponent<Animator>();
        _animator.SetBool(_isDying, false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent<Player>(out Player player)) 
        {
            player.GetComponent<Rigidbody2D>().AddForce(transform.up * _tossForse, ForceMode2D.Impulse);

            StartCoroutine(Dying());
        }
    }

    IEnumerator Dying()
    {
        _animator.SetBool(_isDying, true);
        yield return new WaitForSeconds(_dyingTime);
        Destroy(this.transform.parent.gameObject);
    }
}
