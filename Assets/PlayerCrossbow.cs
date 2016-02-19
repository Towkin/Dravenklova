using UnityEngine;
using System.Collections;

public class PlayerCrossbow : MonoBehaviour {

    public GameObject BoltPrefab;
    public GameObject Crossbow;
    float m_BoltImpulse = 50f;

    private float m_PlayerReloadCount = 0f;
    private float m_PlayerReloadCountFin = 5f;

    [SerializeField]private bool m_Loaded;
    [SerializeField]private bool m_IsLoading;
    

    private PlayerChar m_PlayerChar;
    
	void Start ()
    {
        m_PlayerChar = GetComponent<PlayerChar>();
        m_Loaded = true;
        m_IsLoading = false;
        
    }
	
	void Update () {
	    if (Input.GetButtonDown("Attack") && !m_IsLoading)
        {
            if (m_Loaded)
            {
                Camera m_Cam = Camera.main;
                GameObject BoltShot = (GameObject)Instantiate(BoltPrefab, Crossbow.transform.position - Crossbow.transform.forward * 1f, m_Cam.transform.rotation);
                Rigidbody BoltBody = BoltShot.GetComponent<Rigidbody>();
                BoltBody.AddForce(m_Cam.transform.forward * m_BoltImpulse, ForceMode.Impulse);
                BoltShot.transform.up = m_Cam.transform.forward;
                m_Loaded = false;
            }
            else if (m_PlayerChar.PlayerAmmo > 0)
            {
                //reload. insert any code regarding meshes and animations, I've done my part! 
                m_IsLoading = true;
            }
            else
            {
                //Play Sound to inform Ammo = 0
                Debug.Log("Out of Ammo");
            }
            
        }

        if (Input.GetButton("Attack") && m_IsLoading)
        {
            m_PlayerReloadCount += Time.deltaTime;

            if (m_PlayerReloadCount >= m_PlayerReloadCountFin)
            {
                m_IsLoading = false;
                m_Loaded = true;
                m_PlayerReloadCount = 0;
                m_PlayerChar.PlayerAmmo -= 1;
            }
        }

        if (Input.GetButtonUp("Attack"))
        {
            m_PlayerReloadCount = 0;
            m_IsLoading = false;
        }
    }
}
