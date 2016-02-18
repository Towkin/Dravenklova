using UnityEngine;
using System.Collections;

public class CrossbowBoltBehaviour : MonoBehaviour {

    PlayerChar m_PlayerChar;
    private bool m_IsFlying;

    //private float m_DistanceCheck;
    private Rigidbody m_BoltBody;

	// Use this for initialization
	void Start ()
    {
        m_PlayerChar = GetComponent<PlayerChar>();
        m_IsFlying = true;
        m_BoltBody = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (m_IsFlying)
        {
            RaycastHit m_Hit;
            Ray m_RayPath = new Ray(transform.position, m_BoltBody.velocity.normalized);

            if (Physics.Raycast(m_RayPath, out m_Hit, m_BoltBody.velocity.magnitude * Time.deltaTime * 2))
            {
                m_IsFlying = false;
                if (m_Hit.collider.tag == "Enemy")
                {
                    // TODO: Deal damage on enemy
                    

                    Destroy(gameObject);
                }
                else
                {
                    float m_Rnd = Random.Range(1, 5);
                    if (m_Rnd > 3)
                        Destroy(this.gameObject);
                    Debug.Log(m_Rnd);

                    transform.position = m_Hit.point;
                    Destroy(m_BoltBody);
                    
                }
                
            }
            else
            {
                if (m_IsFlying)
                {
                    transform.up = m_BoltBody.velocity.normalized;
                }
            }
        }
	}
}
