using System.Transactions;
using UnityEngine;

public class EnermyFinder : MonoBehaviour
{
    [SerializeField] private EnermyController _controller;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //TODO : 추적 상태로 전환
            _controller.IsIdle = false;
            _controller.IsWalking = false;
            _controller.IsChasing = true;
        }
    }
}
