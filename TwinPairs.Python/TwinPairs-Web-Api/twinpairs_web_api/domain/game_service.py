import random
from typing import List

from domain.game import Game
from domain.card import Card
from domain.motif import Motif


class GameService:

    def __init__(self):
        self._id = random.randint(0,9)

    def loadGames(self) -> list[Game]:
        toll = self._id

        cards = [Card(1, Motif("test"))]
        return [Game(cards), Game(cards), Game(cards)]

