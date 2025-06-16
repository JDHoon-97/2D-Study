using System.Transactions;
using UnityEngine;

public class EnermyFinder : MonoBehaviour
{
    [SerializeField] private EnermyController _controller;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _controller.IsDetect = true;
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _controller.IsDetect = false;
        }
    }
}
