﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jintori
{
    // --- Class Declaration ------------------------------------------------------------------------
    public class CharacterIconGrid : MonoBehaviour
    {
        // --- Events -----------------------------------------------------------------------------------
        /// <summary> Raised when the icon is switched </summary>
        public event System.Action<string> switched;

        /// <summary> Raised when the icon is selected </summary>
        public event System.Action<string> selected;

        // --- Constants --------------------------------------------------------------------------------
        // --- Static Properties ------------------------------------------------------------------------
        // --- Static Methods ---------------------------------------------------------------------------
        // -----------------------------------------------------------------------------------
        // --- Inspector --------------------------------------------------------------------------------
        [SerializeField]
        CharacterIcon sampleIcon = null;

        // --- Properties -------------------------------------------------------------------------------
        // --- MonoBehaviour ----------------------------------------------------------------------------
        // -----------------------------------------------------------------------------------	
        void Start()
        {
            sampleIcon.gameObject.SetActive(false);
            Clear();
        }

        // --- Methods ----------------------------------------------------------------------------------
        // -----------------------------------------------------------------------------------	
        /// <summary>
        /// Clears the grid from all icons
        /// </summary>
        public void Clear()
        {
            while (transform.childCount > 1)
            {
                Transform child = transform.GetChild(0);
                if (child == sampleIcon.transform)
                    child = transform.GetChild(1);

                child.SetParent(null);
                DestroyObject(child.gameObject);
            }
        }

        // -----------------------------------------------------------------------------------	
        /// <summary>
        /// Adds an icon to the grid
        /// </summary>
        /// <param name="sprite">Sprite to use</param>
        /// <param name="characterFile">Character file the sprite belongs to</param>
        public void Add(Sprite sprite, string characterFile)
        {
            CharacterIcon newIcon = Instantiate(sampleIcon);
            newIcon.gameObject.SetActive(true);
            newIcon.Setup(sprite, characterFile);

            newIcon.selected += selected;
            newIcon.switched += switched;

            newIcon.transform.SetParent(transform);
            newIcon.transform.localScale = Vector3.one;
        }
    }
}