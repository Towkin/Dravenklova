using UnityEngine;
using System.Collections;

public class AnimationUpdater : MonoBehaviour
{

    private NavMeshAgent m_Agent;
    private Animator m_Animator;
    private EnemyBehaviour m_Behaviour;

   
    void Start()
    {
        m_Agent = GetComponent<NavMeshAgent>();
        m_Animator = GetComponent<Animator>();
        m_Behaviour = GetComponent<EnemyBehaviour>();
    }

    void Update () {


        Vector3 LocalVelocity = Quaternion.Inverse(m_Agent.transform.rotation) * m_Agent.velocity;

        //Debug.Log(LocalVelocity.ToString());

        m_Animator.SetFloat("SpeedZ", LocalVelocity.z);
        m_Animator.SetFloat("SpeedX", LocalVelocity.x);
        m_Animator.SetBool("Attack", m_Behaviour.IsAttacking);
        m_Animator.SetBool("WasHit", m_Behaviour.WasHit);
        m_Animator.SetBool("IsDead", m_Behaviour.IsDead);
    }
}
