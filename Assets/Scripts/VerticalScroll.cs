using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalScroll : MonoBehaviour
{
    [Tooltip("Game units per second")]
    [SerializeField] float scrollRate = 1f;
        
    void Update()
    {
        transform.Translate(0, scrollRate * Time.deltaTime, 0);    
    }
}
