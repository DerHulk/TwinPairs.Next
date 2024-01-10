from .player import Player
from .constants import UNKNOWN


class SimplePlayer(Player):

    def __init__(self):
        self._name = UNKNOWN

    @property
    def name(self):
        return self._name
