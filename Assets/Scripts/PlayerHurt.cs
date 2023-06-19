using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHurt : MonoBehaviour
{
    private const string _isHurt = "IsHurt";

    [SerializeField] private Player _player;

    private GameObject _enemy;
    private EnemyDying _enemyDying;
    private Animator _animator;
    private float _recoveryTime = 1;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _animator.SetBool(_isHurt, false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent<DeathZone>(out DeathZone deathzone)) 
        {
            _enemy = deathzone.transform.parent.gameObject;
            _enemyDying = _enemy.transform.GetComponentInChildren<EnemyDying>();

            {
                StartCoroutine(WaitingRecovery());
            }
        }
    }

    IEnumerator WaitingRecovery() 
    {
        _animator.SetBool(_isHurt, true);
        Physics2D.IgnoreCollision(_player.GetComponent<Collider2D>(), _enemy.GetComponent<Collider2D>());
        Physics2D.IgnoreCollision(_player.GetComponent<Collider2D>(), _enemyDying.GetComponent<Collider2D>());
        yield return new WaitForSeconds(_recoveryTime);
        Physics2D.IgnoreCollision(_player.GetComponent<Collider2D>(), _enemy.GetComponent<Collider2D>(), false);
        Physics2D.IgnoreCollision(_player.GetComponent<Collider2D>(), _enemyDying.GetComponent<Collider2D>(), false);
        _animator.SetBool(_isHurt, false);
    }
}
