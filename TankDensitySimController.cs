using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TankDensitySimController : MonoBehaviour
{

    public Canvas canvas;
    public GameObject hydrogen;
    public GameObject helium;
    public GameObject nitrogen;
    public GameObject oxygen;

    public Text tempReadout;

    // Start is called before the first frame update
    void Start()
    {
        hydrogen.GetComponent<AirDensityController>().volume = 1f;
        helium.GetComponent<AirDensityController>().volume = 1f;
        nitrogen.GetComponent<AirDensityController>().volume = 1f;
        oxygen.GetComponent<AirDensityController>().volume = 1f;

        hydrogen.GetComponent<AirDensityController>().density = 0.0887f;
        helium.GetComponent<AirDensityController>().density = 0.1761f;
        nitrogen.GetComponent<AirDensityController>().density = 1.2323f;
        oxygen.GetComponent<AirDensityController>().density = 1.4076f;

        hydrogen.GetComponent<AirDensityController>().setInit();
        helium.GetComponent<AirDensityController>().setInit();
        nitrogen.GetComponent<AirDensityController>().setInit();
        oxygen.GetComponent<AirDensityController>().setInit();

        if (Camera.main == null)
        {
            Debug.LogError("Dennsity sim has no main camera.");
            return;
        }
        canvas.worldCamera = Camera.main;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setBallonTemps(float temp)
    {
        hydrogen.GetComponent<AirDensityController>().setTemp(temp);
        helium.GetComponent<AirDensityController>().setTemp(temp);
        nitrogen.GetComponent<AirDensityController>().setTemp(temp);
        oxygen.GetComponent<AirDensityController>().setTemp(temp);

        tempReadout.text = temp.ToString() + "K";
    }

    public void ToggleSimEnable(bool state)
    {
        Debug.Log("Sim enable");

        hydrogen.GetComponent<AirDensityController>().isSimulated = state;
        helium.GetComponent<AirDensityController>().isSimulated = state;

        nitrogen.GetComponent<AirDensityController>().isSimulated = state;

        oxygen.GetComponent<AirDensityController>().isSimulated = state;


    }

    public void ResetSim()
    {
        hydrogen.GetComponent<AirDensityController>().resetPos();
        helium.GetComponent<AirDensityController>().resetPos();

        nitrogen.GetComponent<AirDensityController>().resetPos();

        oxygen.GetComponent<AirDensityController>().resetPos();
    }
}
