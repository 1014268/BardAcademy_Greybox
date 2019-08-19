using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BandSampling : MonoBehaviour
{
    AudioSampling audioSampling;

    [Header("Sampling Options")]
    public int _band;
    public float _minScale, _maxScale;
    public bool _use8Samples;
    public bool _use64Samples;
    public bool _useFrequency;
    public bool _useAmplitude;
    public bool _useBuffer;

    //Setting other values
    Material _material;
    Color _colour;

    private float _audioBand;
    private float _audioBandBuffer;
    private float _amplitude;
    private float _amplitudeBuffer;


    // Start is called before the first frame update
    void Start()
    {
        audioSampling = GameObject.FindObjectOfType<AudioSampling>();
        _material = GetComponent<MeshRenderer>().materials[0];
        _colour = _material.color;

    }

    // Update is called once per frame
    void Update()
    {
        CheckSampleSize();
        FreqScale();
        AmpScale();
    }

    void CheckSampleSize()
    {
        if (_use8Samples)
        {
            _use64Samples = false;
            _audioBand = audioSampling._audioBand8[_band];
            _audioBandBuffer = audioSampling._audioBandBuffer8[_band];
            _amplitude = audioSampling._Amplitude;
            _amplitudeBuffer = audioSampling._AmplitudeBuffer;
        }
        else if (_use64Samples)
        {
            _use8Samples = false;
            _audioBand = audioSampling._audioBand64[_band];
            _audioBandBuffer = audioSampling._audioBandBuffer64[_band];
            _amplitude = audioSampling._Amplitude;
            _amplitudeBuffer = audioSampling._AmplitudeBuffer;
        }
        else
        {
            Debug.Log("Check Sample Size!");
        }
    }

    void FreqScale()
    {
        if (_useFrequency)
        {
            if (_useBuffer) //To use buffering
            {
                transform.localScale = new Vector3(transform.localScale.x, ((_audioBandBuffer * _maxScale) + _minScale), transform.localScale.z);
                Color _colour = new Color(_audioBandBuffer, _audioBandBuffer, _audioBandBuffer);
                _material.SetColor("_EmissionColor", _colour);
            }
            else if (!_useBuffer) //To use strict scaling
            {
                transform.localScale = new Vector3(transform.localScale.x, ((_audioBand * _maxScale) + _minScale), transform.localScale.z);
                Color _colour = new Color(_audioBand, _audioBand, _audioBand);
                _material.SetColor("_EmissionColor", _colour);
            }
        }
        else if (!_useFrequency)
        {

        }
    }

    void AmpScale()
    {
        if (_useAmplitude)
        {
            if (_useBuffer) //To use buffering
            {
                transform.localScale = new Vector3(transform.localScale.x, ((_amplitudeBuffer * _maxScale) + _minScale), transform.localScale.z);
                Color _colour = new Color(_amplitudeBuffer, _amplitudeBuffer, _amplitudeBuffer);
                _material.SetColor("_EmissionColor", _colour);
            }
            else if (!_useBuffer) //To use strict scaling
            {
                transform.localScale = new Vector3(transform.localScale.x, ((_amplitude * _maxScale) + _minScale), transform.localScale.z);
                Color _colour = new Color(_amplitude, _amplitude, _amplitude);
                _material.SetColor("_EmissionColor", _colour);
            }
        }
        else if (!_useAmplitude)
        {

        }


    }
}
