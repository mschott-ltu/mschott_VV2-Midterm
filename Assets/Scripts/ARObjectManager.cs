using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ARObjectManager : MonoBehaviour
{
    [System.Serializable]
    public enum State {Place, Move, Rotate, None};

    [Header("THIS IS WHERE YOUR MODEL GOES V")]
    [Tooltip("The prefab of the object that you can spawn in AR")]
    public Transform ARObj;
    GameObject selectedObject;
    [Tooltip("The layer of the surface the player can touch")]
    public LayerMask layers;
    //The current state of the manager
    private State state;

    public UnityEvent OnSpawn;


    // Start is called before the first frame update
    void Start()
    {
        state = State.Place;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = Vector3.zero;
        bool inputDown = false;

        if (Input.GetMouseButton(0) && !DetectUIHover.IsPointerOverUIElement())
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layers))
            {
                pos = hit.point;
                inputDown = true;
            }
        }
        else if (Input.touchCount > 0 && !DetectUIHover.IsPointerOverUIElement())
        {
            Touch touch = Input.GetTouch(0);

            
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(touch.position);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layers))
            {
                pos = hit.point;
                inputDown = true;
            }
        }

        if (inputDown)
        {
            switch (state)
            {
                case State.Place:
                    selectedObject = Instantiate(ARObj, pos, Quaternion.identity).gameObject;
                    state = State.Move;
                    OnSpawn.Invoke();
                    break;
                case State.Move:
                    selectedObject.transform.position = pos;
                    break;
                case State.Rotate:
                    break;
            }
        }
    }

    public void SetState(State st)
    {
        state = st;
    }

    public void SetState(int st)
    {
        state = (State)st;
    }

    public void RotateObject(float to)
    {
        if (selectedObject == null || state != State.Rotate)
            return;

        Vector3 newRot = selectedObject.transform.eulerAngles;
        newRot.y = (360 * to);
        selectedObject.transform.eulerAngles = newRot;
    }
}
