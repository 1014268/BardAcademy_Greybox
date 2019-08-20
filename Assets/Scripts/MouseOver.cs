using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MouseOver : MonoBehaviour
{
    public bool _playGame;
    public bool _assetShowcase;
    public bool _ready;

    void Start()
    {
        GetComponent<Renderer>().material.color = Color.white;
    }

    void OnMouseEnter()
    {
        GetComponent<Renderer>().material.color = Color.cyan;
    }

    void OnMouseExit()
    {
        GetComponent<Renderer>().material.color = Color.white;
    }

    private void OnMouseUp()
    {
        GetComponent<Renderer>().material.color = Color.blue;
        if (_playGame)
        {
            SceneManager.LoadScene("Instructions", LoadSceneMode.Single);
        }
        else if(_assetShowcase)
        {
            SceneManager.LoadScene("AssetShowcase", LoadSceneMode.Single);
        }
        else if(_ready)
        {
            SceneManager.LoadScene("AcademyLevel", LoadSceneMode.Single);
        }
        else
        {

        }
    }
}
