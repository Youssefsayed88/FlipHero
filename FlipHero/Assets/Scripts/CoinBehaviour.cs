using UnityEngine;
using System;
using DG.Tweening;

public class CoinBehaviour : MonoBehaviour
{
    public static event Action onCoinCollected;

    float coinSpeed;

    private void Awake()
    {
        coinSpeed = GameManager.Instance.playerSpeed;
        PlayerBehaviour.onPlayerDeath += StopMoving;
        transform.position = new Vector3(transform.position.x, transform.position.y, UnityEngine.Random.Range(-5,5));
    }

    void Start()
    {
        GameManager.onSpeedChange += ChangeCoinSpeed;
        transform.DORotate(new Vector3(0, 360f, 90f), 1f, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear);
        transform.DOMoveX(0, (coinSpeed * 10)/ coinSpeed);
    }

    private void Update()
    {
        if (transform.position.x < 0)
        {
            Destroy(gameObject);
        }
    }

    public void CollectCoin()
    {
        onCoinCollected.Invoke();
    }

    void ChangeCoinSpeed(float newSpeed)
    {
        coinSpeed = GameManager.Instance.playerSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CollectCoin();
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        DOTween.Kill(transform);
    }

    void StopMoving()
    {
        DOTween.Kill(transform);
    }
}
