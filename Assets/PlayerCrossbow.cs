using UnityEngine;
using FMOD.Studio;
using System.Collections;

public class PlayerCrossbow : MonoBehaviour {

    [SerializeField]
    private GameObject m_BoltPrefab;
    public GameObject BoltPrefab
    {
        get { return m_BoltPrefab; }
    }

    [SerializeField]
    private GameObject m_Crossbow;
    public GameObject Crossbow
    {
        get { return m_Crossbow; }
    }

    float m_BoltImpulse = 50f;

    private float m_PlayerReloadCount = 0f;
    private float m_PlayerReloadCountFin = 5f;

    [SerializeField]private bool m_Loaded;
    [SerializeField]private bool m_IsLoading;

    [FMODUnity.EventRef]
    [SerializeField]
    private string m_CrossbowShotAudio;
    private EventInstance m_CrossbowShotEvent;
    private CueInstance m_CrossbowCue;

    private PlayerChar m_PlayerChar;
    
	void Start ()
    {
        m_PlayerChar = GetComponent<PlayerChar>();
        m_Loaded = true;
        m_IsLoading = false;
        m_CrossbowShotEvent = FMODUnity.RuntimeManager.CreateInstance(m_CrossbowShotAudio);
        m_CrossbowShotEvent.start();
        m_CrossbowShotEvent.getCue("A", out m_CrossbowCue);
    }
    void OnDestroy()
    {
        m_CrossbowShotEvent.release();
    }

    void Update () {

        FMOD.ATTRIBUTES_3D Attributes3D = new FMOD.ATTRIBUTES_3D();
        Attributes3D.position.x = transform.position.x;
        Attributes3D.position.y = transform.position.y;
        Attributes3D.position.z = transform.position.z;

        Attributes3D.forward.x = transform.forward.x;
        Attributes3D.forward.y = transform.forward.y;
        Attributes3D.forward.z = transform.forward.z;

        m_CrossbowShotEvent.set3DAttributes(Attributes3D);

        if (Input.GetButtonDown("Attack") && !m_IsLoading)
        {
            if (m_Loaded)
            {
                Camera m_Cam = Camera.main;
                GameObject BoltShot = (GameObject)Instantiate(m_BoltPrefab, m_Crossbow.transform.position - m_Crossbow.transform.forward * 1f, m_Cam.transform.rotation);
                Rigidbody BoltBody = BoltShot.GetComponent<Rigidbody>();
                BoltBody.AddForce(m_Cam.transform.forward * m_BoltImpulse, ForceMode.Impulse);
                BoltShot.transform.up = m_Cam.transform.forward;
                m_Loaded = false;
                m_CrossbowCue.trigger();
                m_CrossbowCue.trigger();
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
                m_CrossbowShotEvent.start();
            }
        }

        if (Input.GetButtonUp("Attack"))
        {
            m_PlayerReloadCount = 0;
            m_IsLoading = false;
        }
    }
}
