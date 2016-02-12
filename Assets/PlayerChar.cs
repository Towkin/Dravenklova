using UnityEngine;
using System.Collections;

public class PlayerChar : MonoBehaviour {

    [SerializeField]private int PlayerHealth;
    private int PlayerHealthMax;

    [SerializeField]private int PlayerSanity;
    private int PlayerSanityhMax;

    [SerializeField]private float PlayerOil;
    private float PlayerOilMax;

    public int PlayerHealthChange
    {
        get { return PlayerHealth; }
        set { PlayerHealth -= value; }
    }

    public int PlayerSanityChange
    {
        get { return PlayerSanity; }
        set { PlayerSanity -= value; }
    }
    public float PlayerOilChange
    {
        get { return PlayerOil; }
        set { PlayerOil -= value; }
    }


    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
