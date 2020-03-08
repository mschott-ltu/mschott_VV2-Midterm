using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlatformSwitch : MonoBehaviour
{
    public UnityEvent OnPhone;
    public UnityEvent OnPC;

    // Start is called before the first frame update
    void Start()
    {
        #if UNITY_EDITOR || UNITY_STANDALONE
            OnPC.Invoke();
        #else
            OnPhone.Invoke();  
        #endif
    }
}
