using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLogic : MonoBehaviour
{
    public string _level;
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

    PlayerLogic _playerLogic;

    // Start is called before the first frame update
    void Start()
    {
        _objectSet();
        _playerLogic._target = "blue";
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

        _playerLogic = FindObjectOfType<PlayerLogic>();
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
        if (_level == _playerLogic._target)
        {
            if (_level == "blue")
            {
                _blueFinish();
                Debug.Log("Blue COMPLETE!");
            }
            else if (_level == "purple")
            {
                _purpleFinish();
                Debug.Log("Purple COMPLETE!");
            }
            else if (_level == "red")
            {
                _redFinish();
                Debug.Log("Red COMPLETE!");
            }
            else
            {

            }
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

        _playerLogic._target = "purple";
    }

    void _purpleFinish()
    {
        _purpleChallenge.SetActive(true);
        _purpleVis.SetActive(false);
        _pointPurple.SetActive(false);

        _redPortal.SetActive(true);
        _redVis.SetActive(true);
        _pointRed.SetActive(true);

        _playerLogic._target = "red";
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
