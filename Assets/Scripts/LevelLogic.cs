using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLogic : MonoBehaviour
{
    public string _colour;
    public string _name;
    bool _interact;

    public GameObject _bluePortal;
    public GameObject _purplePortal;
    public GameObject _redPortal;

    public GameObject _blueVis;
    public GameObject _purpleVis;
    public GameObject _redVis;

    public GameObject _blueChallenge;
    public GameObject _purpleChallenge;
    public GameObject _redChallenge;

    public GameObject _pointBlue;
    public GameObject _pointPurple;
    public GameObject _pointRed;

    // Start is called before the first frame update
    void Start()
    {
        _objectSet();
        _colour = "blue";
    }

    // Update is called once per frame
    void Update()
    {
        _interactCheck();
    }

    private void OnTriggerEnter(Collider other)
    {
        _interact = true;
        Debug.Log("Intereact ON!");
    }

    private void OnTriggerExit(Collider other)
    {
        _interact = false;
        Debug.Log("Intereact OFF!");
    }

    void _objectSet()
    {
        _purplePortal.SetActive(false);
        _redPortal.SetActive(false);

        _purpleVis.SetActive(false);
        _redVis.SetActive(false);

        _blueChallenge.SetActive(false);
        _purpleChallenge.SetActive(false);
        _redChallenge.SetActive(false);

        _pointPurple.SetActive(false);
        _pointRed.SetActive(false);
    }

    void _interactCheck()
    {
        if (Input.GetKeyDown("f"))
        {
            Debug.Log("Pressed the F Key");
            if (_interact)
            {
                _completionCheck();
            }
        }
    }

    void _completionCheck()
    {
        if(_colour == "blue")
        {
            _blueFinish();
            Debug.Log("Blue COMPLETE!");
        }
        else if(_colour == "purple")
        {
            _purpleFinish();
            Debug.Log("Purple COMPLETE!");
        }
        else if(_colour == "red")
        {
            _redFinish();
            Debug.Log("Red COMPLETE!");
        }
        else
        {

        }
    }

    void _blueFinish()
    {
        _blueChallenge.SetActive(true);
        _blueVis.SetActive(false);
        _pointBlue.SetActive(false);

        _purplePortal.SetActive(true);
        _purpleVis.SetActive(true);
        _pointPurple.SetActive(true);

        _interact = false;
        _colour = "purple";
    }

    void _purpleFinish()
    {
        _purpleChallenge.SetActive(true);
        _purpleVis.SetActive(false);
        _pointPurple.SetActive(false);

        _redPortal.SetActive(true);
        _redVis.SetActive(true);
        _pointRed.SetActive(true);

        _interact = false;
        _colour = "red";
    }

    void _redFinish()
    {
        _redChallenge.SetActive(true);
        _redVis.SetActive(false);
        _pointRed.SetActive(false);

        _redPortal.SetActive(true);
        _redVis.SetActive(true);
        _pointRed.SetActive(true);

        _interact = false;
    }
}
