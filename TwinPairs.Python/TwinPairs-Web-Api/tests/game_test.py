from unittest import TestCase
from unittest.mock import MagicMock, patch
from twinpairs_web_api.domain.game import Game
from twinpairs_web_api.domain.card import Card
from twinpairs_web_api.domain.motif import Motif
from twinpairs_web_api.domain.player import Player
from twinpairs_web_api.domain.game_phase import GamePhase


class TestGame(TestCase):
    def setUp(self) -> None:
        self._cards = [Card(1, Motif("1"))]

    def test_game(self):
        # arrange

        target = Game(self._cards)
        # act
        target.start()                
        # assert
        self.assertTrue(True)

    def test_player1(self):
        """Test that we can create a player."""

        # arrange
        target = Game(self._cards)

        expectedName = "hulk"
        expcetdImageSize = (200, 20)
        with patch('domain.player.Player.name') as prop:
            prop.__get__ = MagicMock(return_value = expectedName)

            player = Player()
            player.get_image_size = MagicMock(return_value = expcetdImageSize)

            # act
            result = target.add(player)
            result1 = player.get_image_size()

            # assert
            self.assertEquals(expectedName, result)
            self.assertEquals(expcetdImageSize, result1)

    @patch('twinpairs_web_api.domain.player.Player.name')
    def test_player2(self, prop):
        # arrange
        target = Game(self._cards)

        expectedName = "hulk"
        expcetdImageSize = (200, 20)

        prop.__get__ = MagicMock(return_value = expectedName)

        player = Player()
        player.get_image_size = MagicMock(return_value = expcetdImageSize)

        # act
        result = target.add(player)
        result1 = player.get_image_size()

        # assert
        self.assertEquals(expectedName, result)
        self.assertEquals(expcetdImageSize, result1)

    @patch('twinpairs_web_api.domain.player.Player.name')
    def test_player3(self, prop):
        """Test that we create the correct size of cards."""
        # arrange
        target = Game(self._cards)

        expectedName = "hulk"
        expcetdImageSize = (200, 20)

        player = Player()
        player.get_image_size = MagicMock(return_value = expcetdImageSize)

        # act
        result = player.get_image_size()

        # assert
        self.assertEquals(expcetdImageSize, result)

    def test_add_Player01(self):
        """Test that we change phase if we got to players."""
        # arrange
        expected = GamePhase.WaitForStart
        target = Game(self._cards)
        player1 = Player()
        player2 = Player()

        target.add(player1)

        # act
        target.add(player2)

        # assert
        self.assertEquals(expected, target.phase)

    def test_phase01(self):
        """Test the init phase is WaitingForPlayers"""
        # arrange
        expected = GamePhase.WaitForPlayers
        target = Game(self._cards)
        # act
        result = target.phase

        # assert
        self.assertEquals(expected, result)

    def test_start01(self):
        """Test that we got an exception if we start the game and it is not ready for it"""
        # arrange
        target = Game(self._cards)

        # act & assert
        self.assertRaises(Exception, lambda: target.start())
