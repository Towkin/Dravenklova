using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using System.Collections;

[RequireComponent(typeof(Light))]

public class LanternLight : MonoBehaviour {

    private FirstPersonController m_Controller;
    private Light m_Light;
    private PlayerChar m_PlayerChar;
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
        m_Controller = MyController.GetComponent<FirstPersonController>();
        m_Light = GetComponent<Light>();
        m_PlayerChar = MyController.GetComponent<PlayerChar>();
        m_DecreaseDrainHalf = .8f;
        m_DecreaseDrainQuart = .6f;

        m_IntensityMax = 2.5f;

        m_OilDrain = .5f;
        m_OilDrainWalk = m_OilDrain * Time.fixedDeltaTime;
        m_OilDrainRun = ((m_OilDrain * 1.5f) * Time.fixedDeltaTime);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void FixedUpdate ()
    {
        m_CurrIntensity = (m_IntensityMax / m_PlayerChar.GetOilMax) * m_PlayerChar.PlayerOil;

        if (m_PlayerChar.PlayerOil < .5f)
        {
            m_Light.intensity = 0;
        }
        else if (m_PlayerChar.PlayerOil <= m_PlayerChar.GetOilMax / 4)
        {
            if (m_Controller.PlayerWalking)
                m_PlayerChar.PlayerOil -= (m_OilDrainWalk * m_DecreaseDrainQuart);

            else if (!m_Controller.PlayerWalking)
                m_PlayerChar.PlayerOil -= (m_OilDrainRun * m_DecreaseDrainQuart);

            m_Light.intensity = m_CurrIntensity;
        }
        else if (m_PlayerChar.PlayerOil <= m_PlayerChar.GetOilMax / 2)
        {
            if (m_Controller.PlayerWalking)
                m_PlayerChar.PlayerOil -= (m_OilDrainWalk * m_DecreaseDrainHalf);

            else if (!m_Controller.PlayerWalking)
                m_PlayerChar.PlayerOil -= (m_OilDrainRun * m_DecreaseDrainHalf);

            m_Light.intensity = m_CurrIntensity;
        }
        else
        {
            if (m_Controller.PlayerWalking)
                m_PlayerChar.PlayerOil -= m_OilDrainWalk;

            else if (!m_Controller.PlayerWalking)
                m_PlayerChar.PlayerOil -= m_OilDrainRun;

            m_Light.intensity = m_CurrIntensity;
        }
    }
}
