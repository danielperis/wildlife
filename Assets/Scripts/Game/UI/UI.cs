﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Jintori.Game
{
    // --- Class Declaration ------------------------------------------------------------------------
    public class UI : IllogicGate.SingletonBehaviour<UI>
    {
        // --- Events -----------------------------------------------------------------------------------
        // --- Constants --------------------------------------------------------------------------------
        // --- Static Properties ------------------------------------------------------------------------
        // --- Static Methods ---------------------------------------------------------------------------
        // -----------------------------------------------------------------------------------
        // --- Inspector --------------------------------------------------------------------------------
        [SerializeField]
        Text livesText = null;

        [SerializeField]
        Image livesIcon = null;
        
        [SerializeField]
        TimeDisplay _timeDisplay = null;
        public TimeDisplay timeDisplay { get { return _timeDisplay; } }

        [SerializeField]
        PercentageBar _percentageBar = null;
        public PercentageBar percentageBar { get { return _percentageBar; } }

        [SerializeField]
        SkillBar _skillBar = null;
        public SkillBar skillBar { get { return _skillBar; } }

        [SerializeField]
        RoundStart _roundStart = null;
        public RoundStart roundStart { get { return _roundStart; } }

        [SerializeField]
        GameResult gameResult = null;

        [SerializeField]
        ScoreDisplay _scoreDisplay = null;
        public ScoreDisplay scoreDisplay {  get { return _scoreDisplay; } }

        [SerializeField]
        BossTracker _bossTracker = null;
        public BossTracker bossTracker { get { return _bossTracker; } }

        [SerializeField]
        ScoreResults _scoreResults = null;
        public ScoreResults scoreResults { get { return _scoreResults; } }


        // --- Properties -------------------------------------------------------------------------------
        /// <summary> Total lives to show </summary>
        public int lives { set { livesText.text = string.Format("x{0:00}", value); } }

        // --- MonoBehaviour ----------------------------------------------------------------------------
        // -----------------------------------------------------------------------------------	

        // --- Methods ----------------------------------------------------------------------------------
        // -----------------------------------------------------------------------------------	
        /// <summary>
        /// Resets the UI to the beginning of the round
        /// </summary>
        /// <param name="clearPercentage"> percentage needed to clear the round </param>
        /// <param name="livesLeft"> lives left in the round </param>
        public void Reset(
            int livesLeft, int clearPercentage, int roundTime, 
            float maxSkillTime, float remainingSkillTime)
        {
            gameObject.SetActive(true);
            ShowHeader(true);

            lives = livesLeft;
            livesIcon.color = Config.instance.skillColor;

            timeDisplay.Reset(roundTime);
            percentageBar.Reset(clearPercentage);
            skillBar.Reset(maxSkillTime, remainingSkillTime);
            roundStart.Reset();
            gameResult.Reset();
            scoreDisplay.Reset();
            bossTracker.Reset();
        }

        // -----------------------------------------------------------------------------------	
        /// <summary>
        /// Play the game result
        /// </summary>
        public void PlayResult(bool cleared)
        {
            ShowHeader(false);
            gameResult.gameObject.SetActive(true);
            // show "cleared / game over"
            if (cleared)
                StartCoroutine(gameResult.PlayCleared());
            else
                StartCoroutine(gameResult.PlayGameOver());
        }
        
        // -----------------------------------------------------------------------------------	
        /// <summary>
        /// Hides the "cleared / game over" part of the UI
        /// </summary>
        public void HideResult()
        {
            gameResult.gameObject.SetActive(false);
        }

        // -----------------------------------------------------------------------------------	
        /// <summary>
        /// Shows / Hides the "header" part of the UI (percentage, lives, score, etc)
        /// </summary>
        void ShowHeader(bool show)
        {
            timeDisplay.gameObject.SetActive(show);
            scoreDisplay.gameObject.SetActive(show);
            percentageBar.gameObject.SetActive(show);
            skillBar.gameObject.SetActive(show);
            livesText.gameObject.SetActive(show);
            bossTracker.gameObject.SetActive(show);

            livesIcon.gameObject.SetActive(show);
            livesIcon.transform.parent.gameObject.SetActive(show); // icon background
        }
    }
}