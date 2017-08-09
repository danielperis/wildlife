﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jintori.SelectScreen
{
    // --- Class Declaration ------------------------------------------------------------------------
    public class RoundImages : MonoBehaviour
    {
        // --- Events -----------------------------------------------------------------------------------
        // --- Constants --------------------------------------------------------------------------------
        // --- Static Properties ------------------------------------------------------------------------
        // --- Static Methods ---------------------------------------------------------------------------
        // -----------------------------------------------------------------------------------
        // --- Inspector --------------------------------------------------------------------------------
        [SerializeField]
        RoundIcon[] icons = null;

        [SerializeField]
        Sprite lockedSprite = null;

        // --- Properties -------------------------------------------------------------------------------
        CharacterFile.File characterFile;
        Data.CharacterStats stats;

        // --- MonoBehaviour ----------------------------------------------------------------------------
        // -----------------------------------------------------------------------------------	
        private void Start()
        {
            foreach(RoundIcon icon in icons)
                icon.select += OnRoundSelected;
        }

        // --- Methods ----------------------------------------------------------------------------------
        // -----------------------------------------------------------------------------------	
        private void OnRoundSelected(Selectable sender)
        {
            int idx = System.Array.IndexOf(icons, sender);
            if (stats.rounds[idx].cleared)
                Overlay.instance.roundImageViewer.Show(characterFile, idx);
            else
            {
                string diff = idx == 3 ? "hard" : "normal";
                string msg = string.Format("Clear round {0} in {1} difficulty to unlock this image", idx + 1, diff);
                Overlay.instance.messagePopup.Show(msg.ToUpper(), "IMAGE LOCKED");
            }
        }

        // -----------------------------------------------------------------------------------	
        public void SetCharacter(CharacterFile.File characterFile)
        {
            stats = Data.SaveFile.instance.GetCharacterStats(characterFile.guid);
            this.characterFile = characterFile;
            for (int i = 0; i < Config.Rounds; i++)
            {
                if (stats.rounds[i].cleared)
                    icons[i].previewIcon.sprite = characterFile.baseSheet.roundIcons[i];
                else
                    icons[i].previewIcon.sprite = lockedSprite;
            }
        }
    }
}