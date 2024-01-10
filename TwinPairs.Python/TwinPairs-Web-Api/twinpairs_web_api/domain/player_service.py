import random

from domain.player import Player
from domain.simple_player import SimplePlayer


class PlayerService:

    def __init__(self):
        self._id = random.randint(11,22)

    def getplayer(self) -> Player:
        toll = self._id
        return SimplePlayer()

