﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Jintori.SelectScreen
{
    // --- Class Declaration ------------------------------------------------------------------------
    public class SkillSelectPopup : MonoBehaviour
    {
        // --- Events -----------------------------------------------------------------------------------
        // --- Constants --------------------------------------------------------------------------------
        // --- Static Properties ------------------------------------------------------------------------
        // --- Static Methods ---------------------------------------------------------------------------
        // -----------------------------------------------------------------------------------
        // --- Inspector --------------------------------------------------------------------------------
        // --- Properties -------------------------------------------------------------------------------
        SkillSelectButton[] skillButtons;

        public bool isVisible { get; private set; }

        public Game.Skill.Type selectedSkill { get; private set; }

        // --- MonoBehaviour ----------------------------------------------------------------------------

        // --- Methods ----------------------------------------------------------------------------------
        // -----------------------------------------------------------------------------------	
        private void Initialize()
        {
            skillButtons = GetComponentsInChildren<SkillSelectButton>();
            for (int i = 0; i < skillButtons.Length; i++)
            {
                skillButtons[i].Initialize();
                skillButtons[i].select += OnSkillSelected;
            }
        }
        
        // -----------------------------------------------------------------------------------	
        public void Show(Game.Skill.Type setDefault)
        {
            if (skillButtons == null)
                Initialize();

            selectedSkill = setDefault;

            isVisible = true;
            gameObject.SetActive(true);
            Overlay.instance.background.Show();
            EventSystem.current.SetSelectedGameObject(skillButtons[(int)setDefault].gameObject);
        }

        // -----------------------------------------------------------------------------------	
        private void OnSkillSelected(Selectable sender)
        {
            SkillSelectButton skillButton = sender as SkillSelectButton;
            selectedSkill = skillButton.skill;
            Close();
        }

        // -----------------------------------------------------------------------------------	
        public void Close()
        {
            isVisible = false;
            gameObject.SetActive(false);
            Overlay.instance.background.Hide();
        }
    }
}