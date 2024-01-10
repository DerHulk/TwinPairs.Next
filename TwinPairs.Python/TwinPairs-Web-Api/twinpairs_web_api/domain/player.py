from abc import ABC


class Player(ABC):

    @property
    def name(self):
        raise NotImplementedError

    def get_image_size(self):
        return 100, 100

    async def get_audio_stream(self):
        return 200, 200
