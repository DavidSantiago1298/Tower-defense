using UnityEngine;
using UnityEngine.Events;
using System.Collections;
public class Enemy : MonoBehaviour
{
    [SerializeField]
    private EnemyData _enemyData;
    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private UnityEvent _onInitialize;
    private bool _isRunning;
    private Transform _targetTransform;
    private Health _targetHealth;
    private Coroutine _attackCoroutine;
    private void OnEnable()
    {
        _isRunning = false;
        _onInitialize?.Invoke();
        GetTarget();
    }
    private void GetTarget()
    {
        GameObject target = GameObject.FindGameObjectWithTag(_enemyData.primaryTargetTag);
        if (target != null && !_isRunning)
        {
            _targetTransform = target.transform;
            _targetHealth = target.GetComponent<Health>();
            _isRunning = true;
            _animator.Play(_enemyData.runAnimationName);
        }
    }
    private void Update()
    {
        if (_isRunning)
        {
            transform.position = Vector3.MoveTowards(transform.position, _targetTransform.position, _enemyData.runSpeed * Time.deltaTime);
            transform.LookAt(_targetTransform.position);
            if (Vector3.Distance(transform.position, _targetTransform.position) <= _enemyData.attackRange)
            {
                _isRunning = false;
                _attackCoroutine = StartCoroutine(Attack());
            }
        }
    }
    private IEnumerator Attack()
    {
        while (_targetHealth != null && _targetHealth.CurrentHealth > 0)
        {
            _animator.Play(_enemyData.attckAnimationName, 0, 0f);
            yield return new WaitForSeconds(_enemyData.attackDuration);
            if (_targetHealth != null)
            {
                _targetHealth.TakeDamage(_enemyData.attackDamage);
            }
            yield return new WaitForSeconds(_enemyData.attackCooldown);
        }
        _targetHealth = null;
        _targetTransform = null;
        GetTarget();
    }

    private void Win()
    {
        _animator.Play(_enemyData.winAnimationName);
    }

    public void Die()
    {
        StopAllCoroutines();
        _isRunning = false;
        StartCoroutine(DieCoroutine());
    }



    private IEnumerator DieCoroutine()
    {
        _animator.Play(_enemyData.dieAnimationName);
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
    }


    private void OnDisable()
    {
        if (_attackCoroutine != null)
        {
            StopCoroutine(_attackCoroutine);
            _targetTransform = null;
            _targetHealth = null;
        }
    }
}
 
