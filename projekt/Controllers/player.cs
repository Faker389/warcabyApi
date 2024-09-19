using Microsoft.AspNetCore.DataProtection.KeyManagement.Internal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Numerics;

namespace projekt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class player : ControllerBase
    {
        public static char[][] ChessBoard = new char[8][];
        public static void resetBoardfkc()
        {
            for (int row = 0; row < 8; row++)
            {
                ChessBoard[row] = new char[8];
                for (int col = 0; col < 8; col++)
                {
                    if ((row + col) % 2 == 0)
                    {
                        ChessBoard[row][col] = ' ';
                    }
                    else
                    {
                        if (row < 3)
                        {
                            ChessBoard[row][col] = 'X';
                        }
                        else if (row > 4)
                        {
                            ChessBoard[row][col] = 'O';
                        }
                        else
                        {
                            ChessBoard[row][col] = ' ';
                        }
                    }
                }
            }
        }
        static player()
        {
            resetBoardfkc();
        }
        public static bool checkLeftUp(int x, int y, int diffrence)
        {
            return (y - diffrence >= 0 && x - diffrence >= 0);
        }
        public static bool checkRightUp(int x, int y, int diffrence)
        {
            return (y + diffrence <= 7 && x - diffrence >= 0);
        }
        public static bool checkLeftDown(int x, int y, int diffrence)
        {
            return (y - diffrence >= 0 && x + diffrence <= 7);
        }
        public static bool checkRightDown(int x, int y, int diffrence)
        {
            return (y + diffrence <= 7 && x + diffrence <= 7);
        }
        [HttpGet("resetBoard")]
        public IActionResult resetBoard()
        {
            resetBoardfkc();
            return Ok(ChessBoard);
        }
        [HttpPost("getMove")]
        public IActionResult GetPossibleMoves([FromBody] PlayerCordinates coords)
        {
            char[][] ChessBoardCopy = new char[8][];
            for (int row = 0; row < 8; row++)
            {
                ChessBoardCopy[row] = new char[8];
                for (int col = 0; col < 8; col++)
                {
                    ChessBoardCopy[row][col] = ChessBoard[row][col];
                }
            }
            int playerX = coords.x;
            int playerY = coords.y;
            if (checkRightUp(playerX, playerY, 1) && ChessBoardCopy[playerX - 1][playerY + 1] == ' ')
            {
                ChessBoardCopy[playerX - 1][playerY + 1] = 'T';
            }
            if ((checkLeftUp(playerX, playerY, 1) && ChessBoardCopy[playerX - 1][playerY - 1] == ' '))
            {
                ChessBoardCopy[playerX - 1][playerY - 1] = 'T';
            }
            if (checkRightDown(playerX, playerY, 1) && ChessBoardCopy[playerX][playerY]=='K' && ChessBoardCopy[playerX + 1][playerY + 1] == ' ')
            {
                ChessBoardCopy[playerX + 1][playerY + 1] = 'T';
            }
            if (checkLeftDown(playerX, playerY, 1) && ChessBoardCopy[playerX][playerY] == 'K' && ChessBoardCopy[playerX + 1][playerY - 1] == ' ')
            {
                ChessBoardCopy[playerX + 1][playerY - 1] = 'T';
            }
            if (checkRightUp(playerX, playerY, 2) && ChessBoardCopy[playerX - 1][playerY + 1] == 'X' && ChessBoardCopy[playerX - 2][playerY + 2] == ' ')
            {
                ChessBoardCopy[playerX - 1][playerY + 1] = 'B';
            }
            if (checkLeftUp(playerX, playerY, 2) && ChessBoardCopy[playerX - 1][playerY - 1] == 'X' && ChessBoardCopy[playerX - 2][playerY - 2] == ' ')
            {
                ChessBoardCopy[playerX - 1][playerY - 1] = 'B';
            }
            if (checkLeftDown(playerX, playerY, 2) && ChessBoardCopy[playerX + 1][playerY - 1] == 'X' && ChessBoardCopy[playerX + 2][playerY - 2] == ' ')
            {
                ChessBoardCopy[playerX + 1][playerY - 1] = 'B';
            }
            if (checkRightDown(playerX, playerY, 2) && ChessBoardCopy[playerX + 1][playerY + 1] == 'X' && ChessBoardCopy[playerX + 2][playerY + 2] == ' ')
            {
                ChessBoardCopy[playerX + 1][playerY + 1] = 'B';
            }
            if (checkRightUp(playerX, playerY, 2) && ChessBoardCopy[playerX - 1][playerY + 1] == 'Q' && ChessBoardCopy[playerX - 2][playerY + 2] == ' ')
            {
                ChessBoardCopy[playerX - 1][playerY + 1] = 'Z';
            }
            if (checkLeftUp(playerX, playerY, 2) && ChessBoardCopy[playerX - 1][playerY - 1] == 'Q' && ChessBoardCopy[playerX - 2][playerY - 2] == ' ')
            {
                ChessBoardCopy[playerX - 1][playerY - 1] = 'Z';
            }
            if (checkLeftDown(playerX, playerY, 2) && ChessBoardCopy[playerX + 1][playerY - 1] == 'Q' && ChessBoardCopy[playerX + 2][playerY - 2] == ' ')
            {
                ChessBoardCopy[playerX + 1][playerY - 1] = 'Z';
            }
            if (checkRightDown(playerX, playerY, 2) && ChessBoardCopy[playerX + 1][playerY + 1] == 'Q' && ChessBoardCopy[playerX + 2][playerY + 2] == ' ')
            {
                ChessBoardCopy[playerX + 1][playerY + 1] = 'Z';
            }
            return Ok(ChessBoardCopy);
        }
        [HttpGet("getBoard")]
        public IActionResult GetChessBoard()
        {
            return Ok(ChessBoard);
        }
        [HttpPost("sendMove")]
        public IActionResult ChoseMove([FromBody] CoordinatesData coords)
        {

            int checkMoves(int px, int py, int mx, int my)
            {
                if (ChessBoard[mx][my] == 'X' || ChessBoard[mx][my]=='Q') return 2;
                if ((mx - 1 == px || mx + 1 == px) && (my - 1 == py || my + 1 == py)) return 1;
                return 0;
            }
            int playerX = coords.playerX;
            int playerY = coords.playerY;
            int moveX = coords.moveX;
            int moveY = coords.moveY;
            char player = ChessBoard[playerX][playerY];
            if (checkMoves(playerX, playerY, moveX, moveY) == 0)
            {
                return BadRequest("Invalid Move");
            }
            else if (checkMoves(playerX, playerY, moveX, moveY) == 2)
            {
                int substractCoordinatesX = playerX - moveX;
                int substractCoordinatesY = playerY - moveY;
                if (checkLeftDown(moveX, moveY, 1))
                {
                    ChessBoard[playerX][playerY] = ' ';
                    ChessBoard[moveX][moveY] = ' ';
                    if (moveX - substractCoordinatesX == 0) player = 'K';
                    ChessBoard[moveX - substractCoordinatesX][moveY - substractCoordinatesY] = player;
                    return Ok(ChessBoard);
                }
                if (checkRightDown(moveX, moveY, 1))
                {
                    ChessBoard[playerX][playerY] = ' ';
                    ChessBoard[moveX][moveY] = ' ';
                    if (moveX - substractCoordinatesX == 0) player = 'K';
                    ChessBoard[moveX - substractCoordinatesX][moveY - substractCoordinatesY] = player;
                    return Ok(ChessBoard);
                }
            }
            else if (checkMoves(playerX, playerY, moveX, moveY) == 1)
            {
                if (ChessBoard[moveX][moveY] == ' ')
                {
                    ChessBoard[playerX][playerY] = ' ';
                    if (moveX == 0)
                    {
                        ChessBoard[moveX][moveY] = 'K';
                        return Ok(ChessBoard);
                    }
                    ChessBoard[moveX][moveY] = player;
                    return Ok(ChessBoard);
                }
            }
            return Ok(ChessBoard);
        }
        public static bool findAttack(int x, int y, char playerN ,char playerK, char enemy)
        {
            if (checkRightDown(x, y, 2) && (ChessBoard[x + 1][y + 1] == playerN|| ChessBoard[x + 1][y + 1] == playerK )&& ChessBoard[x + 2][y + 2] == ' ')
            {
                ChessBoard[x][y] = ' ';
                ChessBoard[x + 1][y + 1] = ' ';
                ChessBoard[x + 2][y + 2] = enemy;
                return true;
            }
            if (checkRightUp(x, y, 2) && (ChessBoard[x - 1][y + 1] == playerN|| ChessBoard[x - 1][y + 1] == playerK) && ChessBoard[x - 2][y + 2] == ' ')
            {
                ChessBoard[x][y] = ' ';
                ChessBoard[x - 1][y + 1] = ' ';
                ChessBoard[x - 2][y + 2] = enemy;
                return true;
            }
            if (checkLeftDown(x, y, 2) && (ChessBoard[x + 1][y - 1] == playerN|| ChessBoard[x + 1][y - 1] == playerK) && ChessBoard[x + 2][y - 2] == ' ')
            {
                ChessBoard[x][y] = ' ';
                ChessBoard[x + 1][y - 1] = ' ';
                ChessBoard[x + 2][y - 2] = enemy;
                return true;
            }
            if (checkLeftUp(x, y, 2) && (ChessBoard[x - 1][y - 1] == playerN|| ChessBoard[x - 1][y - 1] == playerK) && ChessBoard[x - 2][y - 2] == ' ')
            {
                ChessBoard[x][y] = ' ';
                ChessBoard[x - 1][y - 1] = ' ';
                ChessBoard[x - 2][y - 2] = enemy;
                return true;
            }
            return false;
        }
        [HttpGet("enemyMove")]
        public IActionResult enemyMove()
        {
            int len = 0;
            List<int> freePawnPositionY = new List<int>();
            List<int> freePawnPositionX = new List<int>();
            for (int i = 0; i < ChessBoard.Length; i++)
            {
                for (int j = 0; j < ChessBoard[i].Length; j++)
                {
                    if (ChessBoard[i][j] == 'X' || ChessBoard[i][j]=='Q')
                    {
                        freePawnPositionX.Add(i);
                        freePawnPositionY.Add(j);
                        len++;
                    }
                }
            }
            for (int i = 0; i < len; i++)
            {
                if (findAttack(freePawnPositionX[i], freePawnPositionY[i], 'O','K', ChessBoard[freePawnPositionX[i]][freePawnPositionY[i]]))
                {
                    return Ok(ChessBoard);
                };
            }
            while (true)
            {
                Random random = new Random();
                int randomNumber = random.Next(0, len);
                int x = freePawnPositionX[randomNumber];
                int y = freePawnPositionY[randomNumber];
                char enemy = ChessBoard[x][y];
                int len2 = 0;
                List<int> listX = new List<int>() ;
                List<int> listY = new List<int>() ;
                if (checkRightUp(x, y, 1) && enemy == 'Q' && ChessBoard[x - 1][y + 1] == ' ')
                {
                    listX.Add(x - 1);
                    listY.Add(y + 1);
                    len2++;
                }
                if (checkLeftUp(x, y, 1) && enemy == 'Q' && ChessBoard[x - 1][y - 1] == ' ')
                {
                    listX.Add(x - 1);
                    listY.Add(y - 1);
                    len2++;
                }
                if (checkRightDown(x, y, 1) && ChessBoard[x + 1][y + 1] == ' ')
                {
                    if (x + 1 == 7) enemy = 'Q';
                    listX.Add(x + 1);
                    listY.Add(y + 1);
                    len2++;
                }
                if (checkLeftDown(x, y, 1) && ChessBoard[x + 1][y - 1] == ' ')
                {
                    if (x + 1 == 7) enemy = 'Q';
                    listX.Add(x + 1);
                    listY.Add(y - 1);
                    len2++;
                }
               if (len2>0)
                {
                    int moveIndex = random.Next(0,len2);
                    ChessBoard[x][y] = ' ';
                    ChessBoard[listX[moveIndex]][listY[moveIndex]] = enemy;
                    return Ok(ChessBoard);
                }
            }
        }
    }
        public class CoordinatesData
        {
            public int playerX { get; set; }
            public int playerY { get; set; }
            public int moveX { get; set; }
            public int moveY { get; set; }
        }
        public class PlayerCordinates
        {
            public int x { get; set; }
            public int y { get; set; }
        }
}