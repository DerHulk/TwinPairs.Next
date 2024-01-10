from unittest import IsolatedAsyncioTestCase
from unittest.mock import AsyncMock, patch
from twinpairs_web_api.domain.player import Player


class TestGame(IsolatedAsyncioTestCase):

    async def test_player(self):
        # arrange
        target = Player()
        expected = (0,0)

        target.get_audio_stream = AsyncMock(return_value= expected)

        # act
        result = await target.get_audio_stream()

        # assert
        self.assertEquals(expected, result)
