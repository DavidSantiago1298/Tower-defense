using System.Collections;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField]
    private string _appearAnimationName = "CoinAppear";

    [SerializeField]
    private string _disappearAnimationName = "CoinDisappear";

    [SerializeField]
    private Animator _animator;

    [SerializeField]
    private float _timeToDisappear = 3f;

    private Collider _collider;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
    }

    private void OnEnable()
    {
        _collider.enabled = true;
        StartCoroutine(AppearCoroutine());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    public void Collect()
    {
        _collider.enabled = false;
        _animator.Play(_disappearAnimationName);
    }

    private IEnumerator AppearCoroutine()
    {
        _animator.Play(_appearAnimationName);
        yield return new WaitForSeconds(_timeToDisappear);
        _animator.Play(_disappearAnimationName);
        yield return new WaitForSeconds(_animator.GetCurrentAnimatorStateInfo(0).length);
        gameObject.SetActive(false);

    }

    private IEnumerator DisappearCoroutine()
    {
        _collider.enabled = false;
        _animator.Play(_disappearAnimationName);
        yield return new WaitForSeconds(_animator.GetCurrentAnimatorStateInfo(0).length);
        gameObject.SetActive(false);
    }
}
