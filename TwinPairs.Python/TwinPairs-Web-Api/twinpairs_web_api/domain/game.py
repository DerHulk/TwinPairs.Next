from .card import Card
from .player import Player
from .game_phase import GamePhase
from uuid import uuid4


class Game:

    def __init__(self, cards: list[Card]):
        self._id = uuid4()
        self._cards = cards
        self._players: list[Player] = []
        self._phase = GamePhase.WaitForPlayers

    def start(self):
        if self._phase != GamePhase.WaitForStart:
            raise Exception("Game is not ready.")

        print("started")

    def add(self, player: Player) -> None:
        self._players.append(player)
        if len(self._players) >= 2:
            self._phase = GamePhase.WaitForStart

    @property
    def id(self) -> uuid4:
        return self._id

    @property
    def cards(self) -> list[Card]:
        return self._cards

    @property
    def phase(self) -> GamePhase:
        return self._phase
