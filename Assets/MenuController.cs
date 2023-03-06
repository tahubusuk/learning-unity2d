using System.Collections;
using System.Collections.Generic;
using Unity.VersionControl.Git;
using UnityEditor;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    [SerializeField] GameObject menu;

    private bool _isActive = false;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenOrCloseMenu()
    {
        Debug.Log("menu opened");
        menu.SetActive(_isActive = !_isActive);
    }

}
