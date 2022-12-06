using System;
using System.Collections.Generic;

namespace Lr2
{
    public class GameAccounts
    {
        public static int BasicGameId = 690068;
        public abstract class BasicGameAccount{ 
        public readonly string UserName;
        private protected AccountTypes AccountType = AccountTypes.Basic;
        private int _curRating = 1;

        protected int CurrentRating
        {
            get => _curRating;
            set
            {
                _curRating = value;
                if (_curRating < 1){
                    _curRating = 1;
                }
            }
        }
        protected readonly List<Game> GameList = new List<Game>();

        protected BasicGameAccount(string userName){
            UserName = userName;
        }

        public virtual void WinGame(String opponentName, GameTypes.BasicGame basicGame){
            CurrentRating += basicGame.RatingAmount;
            Game winGame = new Game(BasicGameId, basicGame.RatingAmount, opponentName, "Win", basicGame.GameType);
            GameList.Add(winGame);
        }

        public virtual void LoseGame(String opponentName,GameTypes.BasicGame basicGame){
            CurrentRating -= basicGame.RatingAmount;
            Game loseGame = new Game(BasicGameId, -basicGame.RatingAmount, opponentName, "Lose", basicGame.GameType);
            GameList.Add(loseGame);
        }

        public virtual void PrintStats(){
            Console.WriteLine();
            Console.WriteLine(UserName+" stats:");
            Console.WriteLine("| Current rating: "+CurrentRating+" | Games played: "+GameList.Count+" | Account type: "+AccountType+" |");
            Console.WriteLine();
            foreach (var game in GameList){
                Console.Write("\n|\t Game ID:"+game.GameId+" \t|\t Opponent: "+game.OpponentName+" \t|\t Game type: "+game.GameType+" \t|\t Game result: "+game.GameResult+" \t|\t Game rating: "+game.RatingAmount+" \t|");
            }
        }
    }

        public class GameAccount : BasicGameAccount
        {
            public GameAccount(string userName) : base(userName){
            }
        }
        
        public class PrimeAccount : BasicGameAccount
        {
            public PrimeAccount(string userName) : base(userName)
            {
                AccountType = AccountTypes.Prime;
            }

            public override void WinGame(string opponentName, GameTypes.BasicGame basicGame){
                CurrentRating += (int)Math.Round(basicGame.RatingAmount+basicGame.RatingAmount*CheckWinStreak());
                Game winGame = new Game(BasicGameId, (int)Math.Round(basicGame.RatingAmount+Math.Round(basicGame.RatingAmount*CheckWinStreak())), opponentName, "Win", basicGame.GameType);
                GameList.Add(winGame);
            }

            public override void PrintStats(){   
                Console.WriteLine();
                Console.WriteLine(UserName+" stats:");
                Console.WriteLine("| Current rating: "+CurrentRating+" | Games played: "+GameList.Count+" | Account type: "+AccountType+" |");
                Console.WriteLine();
                foreach (var game in GameList){
                    if (game.RatingAmount > 0)
                    {
                        int noBonusRating = (int)Math.Round(game.RatingAmount-Math.Round(game.RatingAmount - Math.Round(game.RatingAmount * CheckForPrint(game))) * CheckForPrint(game));
                        Console.WriteLine("|\t Game ID:"+game.GameId+" \t|\t Opponent: "+game.OpponentName+" \t|\t Game type: "+game.GameType+" \t|\t Game result: "+game.GameResult+" \t|\t Game rating: "+game.RatingAmount+" ("+noBonusRating+" + "+(int)Math.Round(Math.Round(game.RatingAmount-game.RatingAmount*CheckForPrint(game))*CheckForPrint(game))+") \t |");
                    }
                    else{
                        Console.WriteLine("|\t Game ID:"+game.GameId+" \t|\t Opponent: "+game.OpponentName+" \t|\t Game type: "+game.GameType+" \t|\t Game result: "+game.GameResult+" \t|\t Game rating: "+game.RatingAmount+" ("+game.RatingAmount+" + 0) \t |");
                    }
                }
            }

            private double CheckWinStreak(){
                int streakCounter = 0;
                int i = GameList.Count-1;
                while (i >= 0 && GameList[i].GameResult.Equals(GameResults.Win.ToString()) && streakCounter <= 5){
                    streakCounter++;
                    i--;
                }
                return 0.05*streakCounter;
            }

            private double CheckForPrint(Game game){
                int streakCounter = 0;
                int i = GameList.IndexOf(game)-1;
                while (i >= 0 && GameList[i].GameResult.Equals(GameResults.Win.ToString()) && streakCounter <= 5){
                    streakCounter++;
                    i--;
                }
                return 0.05*streakCounter;
            }
        }
        
        public class PrimeDeluxeAccount : BasicGameAccount
        {
            public PrimeDeluxeAccount(string userName) : base(userName){
                AccountType = AccountTypes.PrimeDeluxe;
            }
            
            public override void WinGame(string opponentName, GameTypes.BasicGame basicGame){
                CurrentRating += (int)Math.Round(basicGame.RatingAmount+basicGame.RatingAmount*CheckWinStreak());
                Game winGame = new Game(BasicGameId, (int)Math.Round(basicGame.RatingAmount+basicGame.RatingAmount*CheckWinStreak()), opponentName, "Win", basicGame.GameType);
                GameList.Add(winGame);
            }

            public override void LoseGame(string opponentName, GameTypes.BasicGame basicGame){
                CurrentRating -= (int)Math.Round(basicGame.RatingAmount-basicGame.RatingAmount*0.25);
                Game loseGame = new Game(BasicGameId, -(int)Math.Round(basicGame.RatingAmount-basicGame.RatingAmount*0.25), opponentName, "Lose", basicGame.GameType);
                GameList.Add(loseGame);
            }
            
            public override void PrintStats(){   
                Console.WriteLine();
                Console.WriteLine(UserName+" stats:");
                Console.WriteLine("| Current rating: "+CurrentRating+" | Games played: "+GameList.Count+" | Account type: "+AccountType+" |");
                Console.WriteLine();
                foreach (var game in GameList){
                    if (game.RatingAmount > 0){
                        int noBonusRating = (int)Math.Round(game.RatingAmount-Math.Round(game.RatingAmount - Math.Round(game.RatingAmount * CheckForPrint(game))) * CheckForPrint(game));
                        Console.WriteLine("|\t Game ID:"+game.GameId+" \t|\t Opponent: "+game.OpponentName+" \t|\t Game type: "+game.GameType+" \t|\t Game result: "+game.GameResult+" \t|\t Game rating: "+game.RatingAmount+" ("+noBonusRating+" + "+(int)Math.Round(Math.Round(game.RatingAmount-game.RatingAmount*CheckForPrint(game))*CheckForPrint(game))+") \t |");
                    }
                    else{
                        Console.WriteLine("|\t Game ID:"+game.GameId+" \t|\t Opponent: "+game.OpponentName+" \t|\t Game type: "+game.GameType+" \t|\t Game result: "+game.GameResult+" \t|\t Game rating: "+game.RatingAmount+" ("+(int)Math.Round(-game.RatingAmount+game.RatingAmount*-0.33)+" - "+(int)Math.Round(game.RatingAmount*-0.33)+") \t |");
                    }
                }
            }

            private double CheckForPrint(Game game){
                int streakCounter = 0;
                int i = GameList.IndexOf(game)-1;
                while (i >= 0 && GameList[i].GameResult.Equals(GameResults.Win.ToString()) && streakCounter <= 5){
                    streakCounter++;
                    i--;
                }
                return 0.05*streakCounter;
            }

            private double CheckWinStreak(){
                int streakCounter = 0;
                int i = GameList.Count-1;
                while (i >= 0 && GameList[i].GameResult.Equals(GameResults.Win.ToString()) && streakCounter <= 5){
                    streakCounter++;
                    i--;
                }
                return 0.05*streakCounter;
            }
        }
        
        public class BotAccount : BasicGameAccount
        {
            public BotAccount(string userName) : base(userName)
            {
                AccountType = AccountTypes.Bot;
            }
        }
        public class Game{
            public readonly int GameId;
            public readonly int RatingAmount;
            public readonly string OpponentName;
            public readonly string GameResult;
            public readonly GameTypesNames GameType;

            public Game(int gameId, int ratingAmount, string opponentName, string gameResult, GameTypesNames gameType){
                GameId = gameId+1;
                RatingAmount = ratingAmount;
                OpponentName = opponentName;
                GameResult = gameResult;
                GameType = gameType;
            }
        }

        public enum AccountTypes
        {
            Basic,
            Prime,
            PrimeDeluxe,
            Bot
        }
    }
}