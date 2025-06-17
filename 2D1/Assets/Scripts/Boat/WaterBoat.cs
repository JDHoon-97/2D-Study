using TMPro;
using UnityEngine;

public class WaterBoat : MonoBehaviour
{
    [SerializeField] private Boat _boat;
    [SerializeField] private Animator _animator;

    private void Update()
    {
        if (_boat._hp == 0)
        {
            _animator.SetTrigger("WaterBoatDestroy");
        }
    }
}
