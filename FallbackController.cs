using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallbackController : MonoBehaviour
{
    public GameObject anchorPrefab;
    // Start is called before the first frame update
    void Start()
    {
        // just instantiate for now at 0 0

        var obj = Instantiate(anchorPrefab);
        var cam = (Camera)GameObject.FindObjectOfType(typeof(Camera));

        cam.GetComponent<CamScript>().trackTarget = obj.transform;

        cam.GetComponent<CamScript>().yOffset = .3f;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
