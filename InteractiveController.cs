using UnityEngine;
using UnityEngine.XR.Management;

using UnityEngine.SceneManagement;

public class InteractiveController : MonoBehaviour
{
    public GameObject fallbackControl;
    public GameObject arControl;
    public GameObject anchorPrefab;
    public TapToPlace tapToPlace;
    public FallbackController fallbackController;

    private void StartArSession()
    {
        Instantiate(arControl, this.transform);
    }

    private void StartFallbackSession()
    {
        fallbackController = Instantiate(fallbackControl, this.transform).GetComponent<FallbackController>();
        fallbackController.anchorPrefab = anchorPrefab;
        tapToPlace.isPlaced = true;        
    }

   
    
    // Start is called before the first frame update
    void Start()
    {
        // fetch ar pref
        var num = PlayerPrefs.GetInt("UseAr", 1);
        var useAr = num == 1;
        tapToPlace = GetComponent<TapToPlace>();
        if (!useAr)
        {
            Debug.Log("Non ar session specified.");
            StartFallbackSession();

        }
        else if (XRGeneralSettings.Instance == null || XRGeneralSettings.Instance.Manager.activeLoader == null)
        {
            Debug.LogError("Initializing XR Failed. Check Editor or Player log for details.");
            StartFallbackSession();
        }
        else
        {
            Debug.Log("Starting XR...");
            //XRGeneralSettings.Instance.Manager.StartSubsystems();
            StartArSession();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MainMenu()
    {
        tapToPlace.RemoveAllReferencePoints();
        SceneManager.LoadScene("MainScene", LoadSceneMode.Single);
    }

}
