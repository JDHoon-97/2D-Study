using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class Knife : MonoBehaviour
{
    [SerializeField] private Controller _controller;
    
    private List <Enermy> _enemies = new List<Enermy>();

    public bool CanAttack => _enemies.Count > 0;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enermy"))
        {
            _enemies.Add(collision.gameObject.GetComponent<Enermy>());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enermy"))
        {
            _enemies.Remove(collision.gameObject.GetComponent<Enermy>());
        }
    }

    public void Attack()
    {
        _controller.Animator.SetTrigger("IsKnifeAttack");

        for (int i = 0; i < _enemies.Count; i++)
        {
            _enemies[i].TakeDamage(1);
        }
    }
}
