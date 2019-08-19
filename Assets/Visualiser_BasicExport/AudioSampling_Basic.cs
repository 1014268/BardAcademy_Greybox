using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioSampling_Basic : MonoBehaviour
{
    AudioSource _audioSource;

    //Samples Frequencies
    private float[] _samples = new float[512];

    //Audio_8
    private float[] _freqBand8 = new float[8];
    private float[] _bandBuffer8 = new float[8];
    private float[] _bufferDecrease8 = new float[8];
    private float[] _freqBandHighest8 = new float[8];
    public float[] _audioBand8, _audioBandBuffer8;

    //Audio_64
    private float[] _freqBand64 = new float[64];
    private float[] _bandBuffer64 = new float[64];
    private float[] _bufferDecrease64 = new float[64];
    private float[] _freqBandHighest64 = new float[64];
    public float[] _audioBand64, _audioBandBuffer64;


    [HideInInspector]
    private float _AmplitudeHighest;
    public float _Amplitude, _AmplitudeBuffer;
    public float _bufferSpeed;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();

        //Set Buffer Speed
        _bufferSpeed = 1.2f;

        Set8();
        Set64();
    }

    void Update()
    {
        GetSpectrum();
        GetAmplitude();
        Sample8();
        Sample64();
    }

    //Gets ALL Spectrum Values
    void GetSpectrum()
    {
        _audioSource.GetSpectrumData(_samples, 0, FFTWindow.Blackman);
    }

    //Gets ALL Amplitude Values
    void GetAmplitude()
    {
        float _CurrentAmplitude = 0;
        float _CurrentAmplitudeBuffer = 0;
        for (int i = 0; i < 8; i++)
        {
            _CurrentAmplitude += _audioBand8[i];
            _CurrentAmplitudeBuffer += _audioBandBuffer8[i];
        }
        if (_CurrentAmplitude > _AmplitudeHighest)
        {
            _AmplitudeHighest = _CurrentAmplitude;
        }
        else
        {
            _AmplitudeHighest = 1;
        }

        _Amplitude = _CurrentAmplitude / _AmplitudeHighest;
        _AmplitudeBuffer = _CurrentAmplitudeBuffer / _AmplitudeHighest;
    }

    //For 8 Samples
    void Set8()
    {
        _audioBand8 = new float[8];
        _audioBandBuffer8 = new float[8];
    } //Call at Start

    void Sample8()
    {
        CreateFreqBands8();
        BandBuffer8();
        CreateAudioBands8();
    }  //Call at Update

    void CreateFreqBands8()
    {
        int count = 0;

        for (int i = 0; i < 8; i++)
        {
            float average = 0;
            int sampleCount = (int)Mathf.Pow(2, i) * 2;

            for (int j = 0; j < sampleCount; j++)
            {
                average += _samples[count] * (count + 1);
                count++;
            }
            average /= count;
            _freqBand8[i] = average * 10;
        }
    } //Held in Sample8

    void BandBuffer8()
    {
        for (int i = 0; i < 8; i++)
        {
            if (_freqBand8[i] > _bandBuffer8[i])
            {
                _bandBuffer8[i] = _freqBand8[i];
                _bufferDecrease8[i] = 0.005f;
            }
            if (_freqBand8[i] < _bandBuffer8[i])
            {
                _bandBuffer8[i] -= _bufferDecrease8[i];
                _bufferDecrease8[i] *= _bufferSpeed;
            }
        }
    } //Held in Sample8

    void CreateAudioBands8()
    {
        for (int i = 0; i < 8; i++)
        {
            if (_freqBand8[i] > _freqBandHighest8[i])
            {
                _freqBandHighest8[i] = _freqBand8[i];
            }
            else
            {
                _freqBandHighest8[i] = 1f;
            }
            _audioBand8[i] = (_freqBand8[i] / _freqBandHighest8[i]);
            _audioBandBuffer8[i] = (_bandBuffer8[i] / _freqBandHighest8[i]);
        }
    } //Held in Sample8

    //For 64 Samples
    void Set64()
    {
        _audioBand64 = new float[64];
        _audioBandBuffer64 = new float[64];
    } //Call at Start

    void Sample64()
    {
        CreateFreqBands64();
        BandBuffer64();
        CreateAudioBands64();
    }  //Call at Update

    void CreateFreqBands64()
    {
        int count = 0;
        int sampleCount = 1;
        int power = 0;

        for (int i = 0; i < 64; i++)
        {
            float average = 0;
            if (i == 16 || i == 32 || i == 40 || i == 48 || i == 56)
            {
                power++;
                sampleCount = (int)Mathf.Pow(2, 1) * 2;
                if (power == 3)
                {
                    sampleCount -= 2;
                }
            }

            for (int j = 0; j < sampleCount; j++)
            {
                average += _samples[count] * (count + 1);
                count++;
            }
            average /= count;
            _freqBand64[i] = average * 10;
        }
    } //Held in Sample64

    void BandBuffer64()
    {
        for (int i = 0; i < 64; i++)
        {
            if (_freqBand64[i] > _bandBuffer64[i])
            {
                _bandBuffer64[i] = _freqBand64[i];
                _bufferDecrease64[i] = 0.005f;
            }
            if (_freqBand64[i] < _bandBuffer64[i])
            {
                _bandBuffer64[i] -= _bufferDecrease64[i];
                _bufferDecrease64[i] *= _bufferSpeed;
            }
        }
    } //Held in Sample64

    void CreateAudioBands64()
    {
        for (int i = 0; i < 64; i++)
        {
            if (_freqBand64[i] > _freqBandHighest64[i])
            {
                _freqBandHighest64[i] = _freqBand64[i];
            }
            else
            {
                _freqBandHighest64[i] = 1f;
            }
            _audioBand64[i] = (_freqBand64[i] / _freqBandHighest64[i]);
            _audioBandBuffer64[i] = (_bandBuffer64[i] / _freqBandHighest64[i]);
        }
    } //Held in Sample64
}
