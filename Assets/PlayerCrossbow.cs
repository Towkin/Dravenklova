using UnityEngine;
using System.Collections;

public class PlayerCrossbow : MonoBehaviour {

    public GameObject BoltPrefab;
    float m_BoltImpulse = 50f;

    private float m_PlayerReloadCount = 0f;
    private float m_PlayerReloadCountFin = 5f;
    //private float m_SecCount = 1f;

    [SerializeField]private bool m_Loaded;
    [SerializeField]private bool m_IsLoading;

    private PlayerChar m_PlayerChar;

	// Use this for initialization
	void Start ()
    {
        m_PlayerChar = GetComponent<PlayerChar>();
        m_Loaded = false;
        m_IsLoading = false;
        
        //m_SecCount = 1.0f * Time.deltaTime;
    }
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetButtonDown("Attack") && !m_IsLoading)
        {
            Debug.Log("Step 1");
            if (m_Loaded)
            {
                Camera m_Cam = Camera.main;
                GameObject BoltShot = (GameObject)Instantiate(BoltPrefab, m_Cam.transform.position, m_Cam.transform.rotation);
                Rigidbody BoltBody = BoltShot.GetComponent<Rigidbody>();
                BoltBody.AddForce(m_Cam.transform.forward * m_BoltImpulse, ForceMode.Impulse);
                m_Loaded = false;
                Debug.Log("Step 2");
            }
            else if (m_PlayerChar.PlayerAmmo > 0)
            {
                //reload
                m_IsLoading = true;
                Debug.Log("Step 3");
            }
            else
            {
                //Play Sound to inform Ammo = 0
                Debug.Log("Out of Ammo");
            }
            
        }
        if (m_IsLoading)
        {
            m_PlayerReloadCount += Time.deltaTime;
            Debug.Log("Step 4");
            if (m_PlayerReloadCount >= m_PlayerReloadCountFin)
            {
                Debug.Log("Step 5");
                m_IsLoading = false;
                m_Loaded = true;
                m_PlayerReloadCount = 0;
                m_PlayerChar.PlayerAmmo -= 1;
            }
        }
    }
}
