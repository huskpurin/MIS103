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
            Start,
            GameMenu,
            Game1,
            Game2,
            Help
        }

        public static GameState currentGameState;
        
        public static void changeState(GameState gs, Game1 game)
        {
            currentGameState = gs;

            if (gs == GameState.GameMenu)
                game.Window.Title = "Menu";
            else if (gs == GameState.Game1)
                game.Window.Title = "";
            else if (gs == GameState.Game2)
                game.Window.Title = "";
            else if (gs == GameState.Help)
                game.Window.Title = "";
        }
    }
}
