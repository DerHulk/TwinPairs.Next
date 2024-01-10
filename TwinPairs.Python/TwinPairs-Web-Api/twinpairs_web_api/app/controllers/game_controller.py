import uuid

from fastapi import FastAPI, APIRouter
from app.ioc import GameServiceInjection, PlayerServiceInjection
from app.models.game_model import GameModel
from uuid import UUID, uuid4
from fastapi import status

router = APIRouter(prefix="/game")


@router.get("/")
def index(gameService: GameServiceInjection, playerService: PlayerServiceInjection) -> list[GameModel]:
    result = []
    for game in gameService.loadGames():
        result.append(GameModel(id=uuid4()))


    player = playerService.getplayer()

    return result


@router.post("/start/{gameId}")
def start(gameId: UUID) -> int:
    return status.HTTP_200_OK


@router.post("/join/{gameId}")
def join(gameId: UUID, playerId: UUID) -> int:
    return status.HTTP_200_OK


@router.post("/leave/{gameId}")
def leave(gameId: UUID, playerId: UUID) -> int:
    return status.HTTP_200_OK
