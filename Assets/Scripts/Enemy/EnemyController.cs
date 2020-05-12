using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public float m_speedEnemy = 5f;
    public float m_detectionRadius = 5f;
    public float m_attackRange = 3f;

    public float m_timeAttackDelay = 1f;
    private float m_timer = 0;

    public Animator m_animator;
    private NavMeshAgent m_agent;

    private Transform m_target;

    public enum StateEnemy {Idle, Moving, Attack, None};
    public StateEnemy currentState = StateEnemy.Idle;

    private void Start()
    {
        m_agent = this.GetComponent<NavMeshAgent>();
        m_target = MainPlayer.s_instance.GetPlayerTransform();
        m_agent.speed = m_speedEnemy;
    }

    private void FixedUpdate()
    {
        switch(currentState)
        {
            case StateEnemy.Idle:
            {
                StateEnemyIdle();
                break;
            }
            case StateEnemy.Moving:
            {
                StateEnemyMoving();
                break;
            }
            case StateEnemy.Attack:
            {
                StateEnemyAttack();
                break;
            }
            case StateEnemy.None:
            {
                break;
            }
        }
    }

    void StateEnemyIdle()
    {
        m_animator.SetFloat("Moving", m_agent.velocity.magnitude/ m_speedEnemy);

        float distanceToPlayer = Vector3.SqrMagnitude(m_target.position - transform.position);
        if(distanceToPlayer < m_detectionRadius * m_detectionRadius)
        {
            m_agent.isStopped = false;
            currentState = StateEnemy.Moving;
        }

    }

    void StateEnemyMoving()
    {
        m_animator.SetFloat("Moving", m_agent.velocity.magnitude/ m_speedEnemy);
        m_agent.SetDestination(m_target.position);

        float rangeAttack = Vector3.SqrMagnitude(m_target.position - transform.position);
        float distanceToPlayer = Vector3.SqrMagnitude(m_target.position - transform.position);
        if(distanceToPlayer > m_detectionRadius * m_detectionRadius)
        {
            m_agent.isStopped = true;
            currentState = StateEnemy.Idle;
        }

        //enemy attack
        if(rangeAttack < m_attackRange * m_attackRange)
        {
            m_agent.ResetPath();
            m_agent.isStopped = true;
            m_agent.velocity = Vector3.zero;
            currentState = StateEnemy.Attack;
        }

    }

    void StateEnemyAttack()
    {
        float rangeAttack = Vector3.SqrMagnitude(m_target.position - transform.position);
        if(rangeAttack > m_attackRange * m_attackRange)
        {
            m_agent.isStopped = false;
            currentState = StateEnemy.Moving;
        }
        else
        {
            //m_animator.SetBool("Attack0", false);
            m_animator.SetFloat("Moving", 0);
            m_timer += Time.deltaTime;
            if(m_timer > m_timeAttackDelay)
            {
                //m_animator.SetBool("Attack0", true);
                transform.LookAt(m_target.position);
                m_animator.SetTrigger("Attack");
                m_timer = 0; 
            }
            
        }
    }
    
}
