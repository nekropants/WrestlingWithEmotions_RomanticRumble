using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinAnim : MonoBehaviour
{
    [SerializeField] private float _frequency = 1;
    [SerializeField] private float _amplitudeRot = 0;
    [SerializeField] private float _amplitudeScale = 0;

    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float sin = Mathf.Sin(Time.time * _frequency);
        transform.eulerAngles = Vector3.forward * (sin * _amplitudeRot);
        transform.localScale = Vector3.one * (1 + sin * _amplitudeScale);
    }
}
