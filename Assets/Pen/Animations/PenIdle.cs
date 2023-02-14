using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PenIdle : MonoBehaviour
{
    [HideInInspector] public float counterSuperIdle = 0;
    [SerializeField] float maxTimeSuperIdle = 5f;
    Animator animator;
    NavMeshAgent navMeshAgent;

    // Start is called before the first frame update
    void Start()
    {
        counterSuperIdle = 0;
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        SuperIdle();
    }

    private void SuperIdle()
    {
        if (navMeshAgent.velocity.sqrMagnitude <= 0.1f)
        {
            counterSuperIdle += Time.deltaTime;
            if (counterSuperIdle >= maxTimeSuperIdle)
            {
                counterSuperIdle = 0;
                animator.SetTrigger("Idle");
            }
        }
        else
        {
            counterSuperIdle = 0;
        }
    }

    //private IEnumerator SetAnimSpeed() {
    //    animator.speed = 1;
    //    while (animator.GetCurrentAnimatorStateInfo(0).IsName("IdlePen")) {
    //        yield return null;
    //    }
    //    animator.speed = navMeshAgent.velocity.magnitude * speedAnimationMult;
    //}
}
