using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using Random = UnityEngine.Random;
using System.Collections;

[RequireComponent(typeof(Light))]

public class LanternLight : MonoBehaviour {

    private FirstPersonController m_Controller;
    private Light m_Light;
    private PlayerChar m_PlayerChar;
    private float m_Rnd;

    private float m_RangeMax;
    private float m_RangeMin;
    private float m_CurrRange;

    private float m_IntensityMax;
    private float m_IntensityMin;
    private float m_CurrIntensity;
    private float m_DecreaseDrainHalf;
    private float m_DecreaseDrainQuart;

    [SerializeField]private float m_OilDrainWalk;
    [SerializeField]private float m_OilDrainRun;
    [SerializeField]private float m_OilDrain;


    [SerializeField]
    private GameObject m_MyController;
    private GameObject MyController
    {
        get { return m_MyController; }
    }

    // Use this for initialization
    void Start ()
    {
        m_Rnd = Random.value * 100;
        m_Controller = MyController.GetComponent<FirstPersonController>();
        m_Light = GetComponent<Light>();
        m_PlayerChar = MyController.GetComponent<PlayerChar>();
        m_DecreaseDrainHalf = .8f;
        m_DecreaseDrainQuart = .6f;

        m_RangeMax = 20;
        m_RangeMin = 8;

        m_IntensityMax = 2.5f;
        m_IntensityMin = .7f;

        m_OilDrain = .5f;
        m_OilDrainWalk = m_OilDrain * Time.fixedDeltaTime;
        m_OilDrainRun = ((m_OilDrain * 1.5f) * Time.fixedDeltaTime);
	}
	
	// Update is called once per frame
	void Update ()
    {
	    
	}

    void FixedUpdate ()
    {
        m_CurrIntensity = m_IntensityMin + (((m_IntensityMax - m_IntensityMin) / m_PlayerChar.GetOilMax) * m_PlayerChar.PlayerOil);

        if (m_PlayerChar.PlayerOil <= m_PlayerChar.GetOilMax / 4)
        {
            if (m_Controller.PlayerWalking)
                m_PlayerChar.PlayerOil -= (m_OilDrainWalk * m_DecreaseDrainQuart);

            else if (!m_Controller.PlayerWalking)
                m_PlayerChar.PlayerOil -= (m_OilDrainRun * m_DecreaseDrainQuart);
        }
        else if (m_PlayerChar.PlayerOil <= m_PlayerChar.GetOilMax / 2)
        {
            if (m_Controller.PlayerWalking)
                m_PlayerChar.PlayerOil -= (m_OilDrainWalk * m_DecreaseDrainHalf);

            else if (!m_Controller.PlayerWalking)
                m_PlayerChar.PlayerOil -= (m_OilDrainRun * m_DecreaseDrainHalf);
        }
        else
        {
            if (m_Controller.PlayerWalking)
                m_PlayerChar.PlayerOil -= m_OilDrainWalk;

            else if (!m_Controller.PlayerWalking)
                m_PlayerChar.PlayerOil -= m_OilDrainRun;
        }
        if (m_CurrIntensity <= m_IntensityMin)
        {
            m_CurrIntensity = 0;
        }
        else
        {
            m_CurrIntensity += 2 * Mathf.PerlinNoise(m_Rnd + Time.time, m_Rnd + 1 + Time.time * 1) - 1;
            float x = Mathf.PerlinNoise(m_Rnd + 0 + Time.time * 2, m_Rnd + 1 + Time.time * 2) - 0.5f;
            float y = Mathf.PerlinNoise(m_Rnd + 2 + Time.time * 2, m_Rnd + 3 + Time.time * 2) - 1f;
            float z = Mathf.PerlinNoise(m_Rnd + 4 + Time.time * 2, m_Rnd + 5 + Time.time * 2) - 0.5f;
            transform.localPosition = Vector3.up + new Vector3(x, y, z) * 1;
        }
        
        m_Light.intensity = m_CurrIntensity;

        m_CurrRange = (((m_RangeMax - m_RangeMin) / m_PlayerChar.GetOilMax) * m_PlayerChar.PlayerOil) + m_RangeMin;

        if (m_CurrRange <= m_RangeMin)
        {
            m_CurrRange = 0;
        }
        m_Light.range = m_CurrRange;
    }
}
