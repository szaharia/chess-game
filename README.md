# Chess Game

The application manages the players and games inside a Chess tournament.

Player information is as follows:

- First name
- Last name
- Rating (integer in range 0-3000)

Game information is as follows:

- White player
- Black player
- Date
- Opening classification (which is one letter followed by two digits, letter in range A-E, digits in range 0-9, e.g. A18, E52, B02)
- Result (win for white displayed as “1-0”, draw (“0.5-0.5”), win for black (“0-1”))


Main features of the application:
- Players: Add, remove, modify, search (by last name).
- Games: Add, remove, modify, search (by player or by opening classification).


### Installation

GameChess requires VS and SQLServer lite in order to run.
1. Download source code from https://github.com/szaharia/chess-game
2. Open up ChessGame\ChessGame.sln solution
3. Start ChessGame project (it's already marked as startup project in solution)


### Technical

ChessGame is an ASP.NET Core MVC application, which connects to a local SQLServer instance for storing data.
In addition, it uses a number of open source projects:

* [Twitter Bootstrap] 
* [jQuery] 

When the application starts up, it verifies that the database exists and, if not, creates it based on the EF migrations data defined in ChessGame\ChessGame.Data\Migrations folder.

The application has 3 controllers:
- HomeController
-- it just has links to the Index action on the other 2 controllers
- PlayerController
-- used to manage the player data
- GameController
-- used to manage the game data 
