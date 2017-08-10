﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Jintori.SelectScreen
{
    // --- Class Declaration ------------------------------------------------------------------------
    public class Options : IllogicGate.SingletonBehaviour<Options>
    {
        [System.Serializable]
        struct Volume
        {
            public Slider slider;
            public Text text;

            int _value;
            public int value
            {
                get { return _value; }
                set
                {
                    _value = Mathf.Clamp(value, 0, 100);
                    text.text = _value.ToString();
                    slider.value = _value;
                }
            }
        }

        // --- Events -----------------------------------------------------------------------------------
        // --- Constants --------------------------------------------------------------------------------
        // --- Static Properties ------------------------------------------------------------------------
        // --- Static Methods ---------------------------------------------------------------------------
        // -----------------------------------------------------------------------------------
        // --- Inspector --------------------------------------------------------------------------------
        [SerializeField]
        Dropdown difficulty = null;

        [SerializeField]
        Volume sfx;

        [SerializeField]
        Volume bgm;

        [SerializeField]
        Toggle fullscreen = null;

        [SerializeField]
        Button accept;

        [SerializeField]
        Button cancel;

        // --- Properties -------------------------------------------------------------------------------
        public bool isDone { get; private set; }

        // --- MonoBehaviour ----------------------------------------------------------------------------
        // -----------------------------------------------------------------------------------	
        private void Start()
        {
            sfx.slider.onValueChanged.AddListener((float value) => { sfx.value = Mathf.RoundToInt(value); });
            bgm.slider.onValueChanged.AddListener((float value) => { bgm.value = Mathf.RoundToInt(value); });
            accept.onClick.AddListener(OnAccept);
            cancel.onClick.AddListener(OnDone);
        }
        // --- Methods ----------------------------------------------------------------------------------
        // -----------------------------------------------------------------------------------
        void OnAccept()
        {
            if (!Screen.fullScreen)
                Config.instance.resolution = new Vector2(Screen.width, Screen.height);

            Config.instance.difficulty = (Config.Difficulty)difficulty.value;
            Config.instance.sfxVolume = sfx.value;
            Config.instance.bgmVolume = bgm.value;
            Config.instance.fullScreen = fullscreen.isOn;

            // apply screen resolution
            int w, h;
            if (fullscreen.isOn)
            {
                w = Screen.currentResolution.width;
                h = Screen.currentResolution.height;
            }
            else
            {
                w = (int)Config.instance.resolution.x;
                h = (int)Config.instance.resolution.y;
            }
            Screen.SetResolution(w, h, fullscreen.isOn);

            // save and close
            Config.instance.SaveOptions();
            OnDone();
        }

        // -----------------------------------------------------------------------------------
        void OnDone()
        {
            isDone = true;
        }

        // -----------------------------------------------------------------------------------
        public void Hide()
        {
            gameObject.SetActive(false);
        }

        // -----------------------------------------------------------------------------------
        public void Show()
        {
            isDone = false;
            gameObject.SetActive(true);

            difficulty.value = (int)Config.instance.difficulty;
            sfx.value = Config.instance.sfxVolume;
            bgm.value = Config.instance.bgmVolume;
            fullscreen.isOn = Config.instance.fullScreen;

            difficulty.Select();
        }
    }
}