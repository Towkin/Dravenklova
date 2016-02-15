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
        m_PlayerSanity = 8;
        m_PlayerSanityMax = 8;
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    void FixedUpdate ()
    {

    }
}
