﻿using MainMenu;
using UnityEngine;

namespace Utils.Common
{
    public class Main: MonoBehaviour
    {
        private void Awake()
        {
            var game = new GameObject("[Game]").AddComponent<Game>();
        
            DontDestroyOnLoad(game.gameObject);

            game.nextState = new InitState();
        }
    }
}