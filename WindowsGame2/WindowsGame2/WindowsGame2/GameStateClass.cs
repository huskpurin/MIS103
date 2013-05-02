using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsGame2
{
    public static class GameStateClass
    {
        public enum GameState
        {
            Start,          // 設定基本資料 : 左右手、
            Menu,           // 選擇
            CountryGuide,   // 國家導覽
            GameNormal,     // 國家 - 普通模式
            GameTime,       // 國家 - 限時模式
            GamePractice,   // 練習模式
            Help
        }

        public static GameState currentGameState;
        
        public static void changeState(GameState gs, Game1 game)
        {
            currentGameState = gs;

            if (gs == GameState.Menu)
                game.Window.Title = "Menu";
            else if (gs == GameState.CountryGuide)
                game.Window.Title = "Guide";
            else if (gs == GameState.GameNormal)
                game.Window.Title = "";
            else if (gs == GameState.GameTime)
                game.Window.Title = "";
            else if (gs == GameState.Help)
                game.Window.Title = "";
            else if (gs == GameState.Help)
                game.Window.Title = "";
        }
    }
}
