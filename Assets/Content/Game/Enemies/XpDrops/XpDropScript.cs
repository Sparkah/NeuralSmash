using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class XpDropScript : MonoBehaviour
{
    [Header("XP amount set on enemy")]
    [SerializeField] private float _xpAmount = 1;
    
    private List<Tween> _tweens = new();
    public void Construct(float xpAmount)
    {
        _xpAmount = xpAmount;
    }
    private void Start()
    {
        transform.position = new Vector3(transform.position.x, 0.25f, transform.position.z);
        Tween tween1 = transform.DOMoveY(1, 2);
        Tween tween2 = transform.DORotate(new Vector3(90, 90, 90), 2).SetLoops(-1);
        _tweens.Add(tween1);
        _tweens.Add(tween2);
    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.TryGetComponent(out PlayerXP playerXP)) return;
        playerXP.TakeXp(_xpAmount);
        foreach (var tween in _tweens)
        {
            tween.Kill();
        }
        Destroy(gameObject);
    }
}
