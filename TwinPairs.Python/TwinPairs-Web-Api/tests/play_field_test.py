from unittest import TestCase
from twinpairs_web_api.domain.motif import Motif
from twinpairs_web_api.domain.player import Player
from twinpairs_web_api.domain.play_field import PlayField


class PlayFieldTests(TestCase):
    """Test that we create the correct size of cards."""
    def test_build01(self):
        # arrange
        expectedLength = 4
        motifs = [Motif("A nice tree"), Motif("A beach in the Morning")]

        # act
        result = PlayField.build(motifs)

        # assert
        self.assertIsNotNone(result)        
        self.assertEquals(expectedLength, len(result.cards))
