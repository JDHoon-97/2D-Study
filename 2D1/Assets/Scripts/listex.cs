using System.Collections.Generic;
using NUnit.Framework;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class listex : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        LinkedList<int> list = new LinkedList<int>();
        list.AddLast(1);
        list.AddLast(2);
        list.AddLast(3);
        list.AddLast(4);

        var node = list.Find(2);
        var newNode = new LinkedListNode<int>(3);
        list.AddAfter(node,newNode);

        foreach (var val in list)
        {
            Debug.Log(val);
        }
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
