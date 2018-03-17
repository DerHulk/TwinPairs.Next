using System;
using System.Collections.Generic;
using System.Text;

namespace TwinPairs.Interfaces
{
    // define the possible states a game can be in
    public enum GameState
    {
        AwaitingPlayers,
        InPlay,
        Finished
    }


    // define game outcomes
    public enum GameOutcome
    {
        Win,
        Lose,
        Draw
    }
}
