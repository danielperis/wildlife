﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jintori.Common.UI
{
    // --- Class Declaration ------------------------------------------------------------------------
    public abstract class Popup : MonoBehaviour
    {
        // --- Events -----------------------------------------------------------------------------------
        // --- Constants --------------------------------------------------------------------------------
        // --- Static Properties ------------------------------------------------------------------------
        // --- Static Methods ---------------------------------------------------------------------------
        // -----------------------------------------------------------------------------------
        // --- Inspector --------------------------------------------------------------------------------
        // --- Properties -------------------------------------------------------------------------------
        // --- MonoBehaviour ----------------------------------------------------------------------------
        // -----------------------------------------------------------------------------------	
        private void Update()
        {
            if (Input.GetButtonDown("Cancel"))
                Hide();
        }

        // --- Methods ----------------------------------------------------------------------------------
        // -----------------------------------------------------------------------------------	
        virtual public void Show()
        {
            gameObject.SetActive(true);
        }
        // -----------------------------------------------------------------------------------	
        virtual public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}