from uuid import UUID
from domain.game import Game
from pydantic import BaseModel


class GameModel(BaseModel):
    id: UUID
