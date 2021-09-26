using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private Animator animator;
    [SerializeField] GameObject doorHead;
    
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Open()
    {
        print("opening");
        Instantiate(doorHead, transform.position, transform.rotation, transform);
        animator.SetTrigger("open");
    }

}
