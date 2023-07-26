using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class Music : MonoBehaviour
{
    public AudioSource backgroundMusic;
    private Slider slider;
    private float volume;

    // Start is called before the first frame update
    void Start()
    {
        volume = PlayerPrefs.GetFloat("Volume");
        backgroundMusic.volume = volume;
        slider = gameObject.GetComponent<Slider>();
        slider.value = volume;
        slider.onValueChanged.AddListener(OnSliderValueChanged);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnSliderValueChanged(float value)
    {
        PlayerPrefs.SetFloat("Volume", slider.value);
        backgroundMusic.volume = PlayerPrefs.GetFloat("Volume");
    }
}
