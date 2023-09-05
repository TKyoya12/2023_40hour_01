using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerArm : MonoBehaviour
{
    Animator animator = null;
    bool isWalk = false;    //�����A�j���[�V����
    bool isTake = false;    //�����Ă�A�j���[�V����
    bool isthrow = false;    //������A�j���[�V����
    private void Start()
    {
        animator = GetComponent<Animator>();    
    }
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            animator.SetBool("isThrow", true);
            animator.SetBool("isTake", false);
            animator.SetBool("isWalk", false);
        }
        if(Input.GetMouseButtonDown(1))
        {
            animator.SetBool("isTake", true);
            animator.SetBool("isWalk", false);
            animator.SetBool("isThrow", false);
        }
    }
    public void finishThrowAnimation()
    {
        animator.SetBool("isWalk", true);
        animator.SetBool("isThrow", false);
    }
}
