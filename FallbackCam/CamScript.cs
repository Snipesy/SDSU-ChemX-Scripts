using UnityEngine;
using UnityEngine.EventSystems;


public class CamScript : MonoBehaviour
{
    // The focus of the camera, used for translational
    private Vector3 focusPosition;
    private Quaternion focusRotation;

    public Transform trackTarget;

    // The rotational focus of the camera
    private Vector3 rotationFocus;

    public float maxOrbitDistance = 3;
    public float minOrbitDistance = 2;
    public float orbitDistance = 3;
    public float angle = 53;
    public float trackingSpeed = 5;

    public float yOffset = 0f;

    public int selection = 1;

    // Use this for initialization
    void Start()
    {
        focusRotation = Quaternion.Euler(angle, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.O))
        {
            if (selection == 1)
                selection = 2;
            else if (selection == 2)
                selection = 1;
        }
        switch (selection)
        {
            // Track a target
            case 0:
                Destroy(gameObject);
                break;
            case 1:


                if (trackTarget != null)
                {
                    var offset = trackTarget.position + new Vector3(0f, yOffset, 0f);


                    float calc = Vector3.Distance(focusPosition, offset);



                    focusPosition = Vector3.Lerp(focusPosition, offset,
                        Time.deltaTime * (trackingSpeed * calc / orbitDistance));
                    rotationFocus = focusPosition;

                    focusRotation = Quaternion.Lerp(focusRotation, Quaternion.Euler(angle,
                        focusRotation.eulerAngles.y, 0), calc * Time.deltaTime * (trackingSpeed));
                }
                break;
            // Track a target and match its rotation
            case 2:
                if (trackTarget != null)
                {
                    var offset = trackTarget.position + new Vector3(0f, yOffset, 0f);


                    float calc = Vector3.Distance(focusPosition, offset);



                    focusPosition = Vector3.Lerp(focusPosition, offset,
                        Time.deltaTime * (trackingSpeed * calc / orbitDistance));
                    rotationFocus = focusPosition;

                    focusRotation = Quaternion.Lerp(focusRotation, Quaternion.Euler(angle,
                        trackTarget.rotation.eulerAngles.y, 0), calc * Time.deltaTime * (trackingSpeed));

                }

                break;
            default:

                break;
        }

        var a = Input.GetAxis("Mouse ScrollWheel");
        if (a != 0)
        {
            orbitDistance += a * 20f;
            if (orbitDistance < minOrbitDistance)
                orbitDistance = minOrbitDistance;
            if (orbitDistance > maxOrbitDistance)
                orbitDistance = maxOrbitDistance;
        }

        // touch data
        if (selection == 1)
            Orbit();

        TransUpdate();
        LookAt();
       


    }

    private void TransUpdate()
    {
        // Unit vector of focus
        if (orbitDistance <= 0)
            return;
        Vector3 offset = -(focusRotation * Vector3.forward) * orbitDistance;

        // Update camera position
        gameObject.transform.position = focusPosition + offset;
    }

    private void LookAt()
    {
        transform.LookAt(rotationFocus);
    }

    public Vector3 priorPosition;
    bool isDragging = false;
    bool isPinching = false;
    float lastPinchDiff = 0;

    private void processOneDown()
    {
        // ignore potential ui events
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            priorPosition = Input.mousePosition;
            isDragging = true;
        }
        if (Input.GetMouseButton(0) && isDragging)
        {
            var pos = Input.mousePosition;
            
            var dif = pos - priorPosition;
            gameObject.transform.RotateAround(focusPosition, Vector3.up, 0.2f * dif.x);
            var prev = angle;
            angle = angle + dif.y * 0.2f;
            if (angle > 80f)
            {
                angle = 80f;
            } else if (angle < -80f)
            {
                angle = -80f;
            }
            var yDif = prev - angle; // makes sure we dont rotate over 80

            var axis = focusRotation * Vector3.right;

            gameObject.transform.RotateAround(focusPosition, axis, yDif);

            priorPosition = pos;
        }
        if (Input.GetMouseButtonUp(0)) {
            isDragging = false;
        }
    }


    // process two finger down (pinch)
    private void processTwoDown() {

        var a = Input.touches[0].position;
        var b = Input.touches[1].position;

        // pinch logic
        var newDp = a.magnitude - b.magnitude;
        var dif = newDp - lastPinchDiff;

        if (!isPinching) {
            isPinching = true;
            lastPinchDiff = newDp;
            return;
        }


        orbitDistance += -dif * 0.01f;
        if (orbitDistance < minOrbitDistance)
            orbitDistance = minOrbitDistance;
        if (orbitDistance > maxOrbitDistance)
            orbitDistance = maxOrbitDistance;

        lastPinchDiff = newDp;


    }

    // Orbits camera by modifying focusRotation
    private void Orbit()
    {

        if (Input.GetKey(KeyCode.Q))
            gameObject.transform.RotateAround(focusPosition, Vector3.up, -100f * Time.deltaTime);
        if (Input.GetKey(KeyCode.E))
            gameObject.transform.RotateAround(focusPosition, Vector3.up, 100f * Time.deltaTime);

        // use same logic for touch and mouse
        if (Input.touchCount == 0 || Input.touchCount == 1)
        {
            processOneDown();
            isPinching = false;
        }
        if (Input.touchCount == 2) {
            processTwoDown();
        }

        
        focusRotation = gameObject.transform.rotation;
    }


}