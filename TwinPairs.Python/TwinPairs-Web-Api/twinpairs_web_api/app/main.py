import uvicorn

from fastapi import FastAPI, Depends
from controllers import game_controller

app = FastAPI()
app.include_router(game_controller.router)



@app.get("/")
def game_start():
    return {"message": "Hello World 1"}


if __name__ == "__main__":
    uvicorn.run(app, host= "0.0.0.0", port= 8080)

