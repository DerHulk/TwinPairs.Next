from typing import  Annotated
from domain.game_service import GameService
from fastapi import FastAPI, Depends

from domain.player_service import PlayerService

# scoped
GameServiceInjection = Annotated[GameService, Depends()]

class SingeltonFactory:
    def __init__(self, instance: PlayerService):
        self._Instance = instance

    def __call__(self):
        return self._Instance

# singelton ?? https://github.com/tiangolo/fastapi/issues/504
# https://fastapi.tiangolo.com/advanced/advanced-dependencies/

playerServiceSingelton = SingeltonFactory(PlayerService())
PlayerServiceInjection = Annotated[PlayerService, Depends(playerServiceSingelton)]

