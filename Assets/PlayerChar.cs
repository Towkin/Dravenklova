using UnityEngine;
using UnityEditor;
using System.Collections;

public class PlayerChar : MonoBehaviour {

    [SerializeField]private float m_PlayerHealth;
    [SerializeField]private float m_PlayerHealthMax;

    [SerializeField]private float m_PlayerSanity;
    [SerializeField]private float m_PlayerSanityMax;

    [SerializeField]private float m_PlayerOil;
    [SerializeField]private float m_PlayerOilMax;

    [SerializeField]private int m_PlayerAmmo;
    [SerializeField]private int m_PlayerAmmoMax;
    [SerializeField]private float m_PlayerDamage;

    private float m_PlayerHolyMod;
    private float m_PlayerLanternCount;
    private float m_PlayerLanternCountFin;
    [SerializeField]private float m_PlayerSanityDmg;
    

    public float PlayerHealth
    {
        get { return m_PlayerHealth; }
        set
        {
            m_PlayerHealth = Mathf.Clamp(value, 0f, m_PlayerHealthMax);
        }
    }

    public int PlayerAmmo
    {
        get { return m_PlayerAmmo; }
        set
        {
            m_PlayerAmmo = Mathf.Clamp(value, 0, m_PlayerAmmoMax);
        }
    }

    public float PlayerSanity
    {
        get { return m_PlayerSanity; }
        set
        {
            m_PlayerSanity = Mathf.Clamp(value, 0f, m_PlayerSanityMax);
        }
    }

    public float GetOilMax
    {
        get { return m_PlayerOilMax; }
    }

    public float PlayerOil
    {
        get { return m_PlayerOil; }
        set
        {
            m_PlayerOil = Mathf.Clamp(value, 0f, m_PlayerOilMax);
        }
    }

    public float PlayerDamage
    {
        get { return m_PlayerDamage; }
        set
        {
            m_PlayerDamage = value;
        }
    }



    // Use this for initialization
    void Start ()
    {
        m_PlayerHolyMod = 15;
        m_PlayerAmmoMax = 3;
        m_PlayerAmmo = 3;
        m_PlayerHealth = 100;
        m_PlayerHealthMax = 100;
        m_PlayerOilMax = 100;
        m_PlayerOil = m_PlayerOilMax;
        m_PlayerSanityMax = 100;
        m_PlayerSanity = 100;
        m_PlayerLanternCount = 0;
        m_PlayerLanternCountFin = 2;
        m_PlayerSanityDmg = 90;
    }
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    void FixedUpdate ()
    {
        m_PlayerLanternCount += Time.fixedDeltaTime;
        if (m_PlayerLanternCount >= m_PlayerLanternCountFin)
        {
            PlayerSanity -= (Mathf.Floor(m_PlayerSanityDmg - PlayerOil)) / m_PlayerHolyMod;
            m_PlayerLanternCount = 0;
        }

        if (m_PlayerHealth <= 0)
        {
            //TODO: this does death
            EditorApplication.isPlaying = false;
        }
    }
}
