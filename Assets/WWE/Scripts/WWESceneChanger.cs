using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class WWESceneChanger : MonoBase
{
    [SerializeField] private string _loadSceneOnEscape;
    [SerializeField] private string _loadSceneOnTimeOut;
    [SerializeField] private string _loadSceneOnReturnOrSpace;
    [SerializeField] private string _loadSceneOnClick;
    [SerializeField] private   float _timeOut = 120;
    private   float timer = 0;


    private float minTimer = 5;

    private void Start()
    {
        Reset();
    }
    private void Reset()
    {
        timer = _timeOut;
    }

    // Update is called once per frame
    void Update ()
    {
        if (minTimer > 0)
        {
            minTimer -= Time.deltaTime;
            return;
        }
        
        bool proceed = Input.GetKeyDown(KeyCode.Space) ||
                       Input.GetKeyDown(KeyCode.Return); 
        timer -= Time.deltaTime;
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TryLoadScene(_loadSceneOnEscape);
        }
        else if( proceed)
        {
            Reset();
            TryLoadScene(_loadSceneOnReturnOrSpace);
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Reset();
            TryLoadScene(_loadSceneOnClick);
        }
        
        if(_timeOut <= 0)
        {
            _timeOut = 0;
            TryLoadScene(_loadSceneOnTimeOut);
        }
    }

    private void TryLoadScene(string sceneName)
    {
        if (string.IsNullOrWhiteSpace(sceneName) == false)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
        }
    }
}
