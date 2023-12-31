﻿# Камень Ножницы Бумага

> ### Установка и запуск
> Скачать можно по ссылке [https://github.com/energizet/RockPaperScissors/releases/tag/publish](https://github.com/energizet/RockPaperScissors/releases)
> 
> Запустить файл RockPaperScissors.exe

### Правила

Игра проводится между двумя игроками или компьютером до 3 побед.

Нужно создать игру и передать её id другому игроку или указать игру с компьютером.\
После того как пользователь подключится к игре, игроки смогут делать ход (выбросив камень, ножницы или бумагу), после чего автоматически начинается новый раунд.\
Каждый пользователь может сделать только один ход, как только все сделают ход начнется новый раунд.\
В любой момент можно посмотреть статистику игры.


### Создание игры
> POST /api/Game/create
> 
> Request:\
> {\
>   "username": "user1",\
>   "isSingle": true\
> }
> 
> Response:\
> {\
>   "gameId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",\
>   "userId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"\
> }

username - имя пользователя\
isSingle - игра ведётся против людей или компьютера\
\
gameId - id игры\
userId - id пользователя

### Присоеденение к игре
> POST /api/Game/join
> 
> Request:\
> {\
>   "gameId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",\
>   "username": "user2"\
> }
> 
> Response:\
> {\
>   "userId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"\
> }

gameId - id игры\
username - имя пользователя\
\
userId - id пользователя

### Сделать ход
> PUT /api/Game/turn
> 
> Request:\
> {\
>   "gameId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",\
>   "userId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",\
>   "turn": 0\
> }
> 
> Response:\
> {\
> "winnerId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",\
> "turns": [\
> {\
> "userId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",\
> "turn": 0\
> }\
> ],\
> "status": 0\
> }

gameId - id игры\
userId - id пользователя\
turn - выбор пользователя\
\
winnerId - id победителя или пустой если ничья\
turns - выборы пользователей\
status - статус раунда

> #### Turn
> 
> {\
> Rock (Камень) = 0,\
> Paper (Бумага) = 1,\
> Scissors (Ножницы) = 2,\
> }
> 
> #### Status
> 
> {\
> InProgress (Пользователи делают ход) = 0,\
> Finished (Раунд завершен) = 1,\
> DrawGame (Ничья) = 2,\
> }

### Получить статистику игры
> GET /api/Game/stat/{gameId}
> 
> Response:\
> {\
> "users": [\
> {\
> "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",\
> "username": "string"\
> }\
> ],\
> "winnerId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",\
> "rounds": [\
> {\
> "winnerId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",\
> "turns": [\
> {\
> "userId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",\
> "turn": 0\
> }\
> ],\
> "turnOptions": 0\
> }\
> ],\
> "stats": [\
> {\
> "userId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",\
> "winsCount": 0\
> }\
> ]\
> }

users - полизователи участвующие в игре\
winnerId - id победителя или пустой если ничья\
rounds - информация по раундам
stats - количество побед у каждого пользователя
