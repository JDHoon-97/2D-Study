using System;
using UnityEngine;

public class FollowingCamera : MonoBehaviour
{
    //Damping Camera
    
    [SerializeField] private Transform _target;

    private void Update()
    {
        var position = _target.position;
        position.z = -10;
        
        transform.position = position;
    }
}
