using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (EventSystem.current.currentSelectedGameObject == gameObject)
        {
            this.transform.localScale = new Vector3(1.1f, 1.1f, 1);
        }
        else
            this.transform.localScale = new Vector3(1,1,1);

    }
}
