using UnityEngine;
using System.Collections;

public class PlayerChar : MonoBehaviour {

    [SerializeField]private float m_PlayerHealth;
    private float m_PlayerHealthMax;

    [SerializeField]private float m_PlayerSanity;
    private float m_PlayerSanityMax;

    [SerializeField]private float m_PlayerOil;
    private float m_PlayerOilMax;

    [SerializeField]private int m_PlayerAmmo;
    private int m_PlayerAmmoMax;

    private float m_PlayerLanternCount;
    private float m_PlayerLanternCountFin;
    private float m_PlayerSanityDmg;

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
    public float PlayerOil
    {
        get { return m_PlayerOil; }
        set
        {
            m_PlayerOil = Mathf.Clamp(value, 0f, m_PlayerOilMax);
        }
    }



    // Use this for initialization
    void Start ()
    {
        m_PlayerAmmo = 0;
        m_PlayerAmmoMax = 3;
        m_PlayerHealth = 4;
        m_PlayerHealthMax = 4;
        m_PlayerOil = 8;
        m_PlayerOilMax = 8;
        m_PlayerSanity = 100;
        m_PlayerSanityMax = 100;
        m_PlayerLanternCount = 0;
        m_PlayerLanternCountFin = 1;
        m_PlayerSanityDmg = 7;
    }
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    void FixedUpdate ()
    {
        m_PlayerLanternCount += m_PlayerLanternCountFin * Time.fixedDeltaTime;
        if (m_PlayerLanternCount >= m_PlayerLanternCountFin)
        {
            PlayerSanity -= Mathf.Floor(m_PlayerSanityDmg - PlayerOil);
            m_PlayerLanternCount = 0;
        }

        if (m_PlayerHealth <= 0)
        {
            //this does death
        }
    }
}
