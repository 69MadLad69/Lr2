using System;

namespace Lr2
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            GameTypes.CreateGame createGame = new GameTypes.CreateGame();
            GameAccounts.BasicGameAccount tilt = new GameAccounts.GameAccount("tilt");
            GameAccounts.BasicGameAccount oleja = new GameAccounts.GameAccount("Oleja");
            GameAccounts.BasicGameAccount bober = new GameAccounts.GameAccount("bober");
            GameAccounts.BasicGameAccount kirgo = new GameAccounts.PrimeAccount("kirgo");
            GameAccounts.BasicGameAccount chokopie = new GameAccounts.PrimeDeluxeAccount("chokopie");
            GameAccounts.BasicGameAccount scamenko = new GameAccounts.GameAccount("scamenko");
            DecideGameResult(createGame.CreateNormalGame(kirgo,bober),GameResults.Win);
            DecideGameResult(createGame.CreateNormalGame(kirgo,oleja),GameResults.Win);
            DecideGameResult(createGame.CreateNormalGame(kirgo,chokopie),GameResults.Win);
            DecideGameResult(createGame.CreatePvEGame(kirgo),GameResults.Win);
            DecideGameResult(createGame.CreatePvEGame(kirgo),GameResults.Win);
            DecideGameResult(createGame.CreateTrainingGame(kirgo,chokopie),GameResults.Win);
            DecideGameResult(createGame.CreatePvEGame(chokopie),GameResults.Lose);
            DecideGameResult(createGame.CreatePvEGame(chokopie),GameResults.Lose);
            DecideGameResult(createGame.CreateNormalGame(chokopie,bober),GameResults.Win);
            DecideGameResult(createGame.CreateNormalGame(chokopie,oleja),GameResults.Win);
            DecideGameResult(createGame.CreateNormalGame(chokopie,bober),GameResults.Win);
            DecideGameResult(createGame.CreatePvEGame(chokopie),GameResults.Lose);
            DecideGameResult(createGame.CreateNormalGame(tilt,kirgo),GameResults.Win);
            DecideGameResult(createGame.CreateNormalGame(tilt,oleja),GameResults.Win);
            DecideGameResult(createGame.CreateNormalGame(tilt,chokopie),GameResults.Win);
            DecideGameResult(createGame.CreateNormalGame(tilt,scamenko),GameResults.Lose);
            DecideGameResult(createGame.CreateNormalGame(tilt,bober),GameResults.Win);
            oleja.PrintStats();
            bober.PrintStats();
            tilt.PrintStats();
            kirgo.PrintStats();
            chokopie.PrintStats();
            scamenko.PrintStats();
        }

        private static void DecideGameResult(GameTypes.BasicGame game, Enum gameResult){
            GameAccounts.BasicGameId++;
            if (gameResult.Equals(GameResults.Win)){
                game.Player.WinGame(game.Opponent.UserName, game);
                game.Opponent.LoseGame(game.Player.UserName, game);
            }else{
                if (gameResult.Equals(GameResults.Lose)){
                    game.Player.LoseGame(game.Opponent.UserName, game);
                    game.Opponent.WinGame(game.Player.UserName, game);
                }
            }
        }
    }

    public enum GameResults{
        Win,
        Lose
    }
}