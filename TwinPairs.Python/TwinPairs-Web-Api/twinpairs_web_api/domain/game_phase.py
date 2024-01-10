from enum import Enum


class GamePhase (int, Enum):
    WaitForStart = 0
    WaitForPlayers = 1
    Running = 2
    Finished = 3
