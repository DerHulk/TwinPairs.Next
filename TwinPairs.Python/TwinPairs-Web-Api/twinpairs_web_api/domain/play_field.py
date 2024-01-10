from .card import Card
from .game import Game
from .motif import Motif


class PlayField:

    @staticmethod
    def build(motifs: list[Motif]) -> Game:
        cards: list[Card] = []
        card_counter = 0

        for m in motifs:
            for i in range(0, 2):
                cards.append(Card(card_counter, m))
                card_counter += 1

        return Game(cards)
