using System;
namespace Lr2
{
    public class GameTypes{
        private static readonly Random RandRating = new Random();
        public abstract class BasicGame{
            internal string GameType;
            protected internal int RatingAmount;
            public readonly GameAccounts.BasicGameAccount Player;
            public readonly GameAccounts.BasicGameAccount Opponent;

            protected BasicGame(GameAccounts.BasicGameAccount player, GameAccounts.BasicGameAccount opponent){
                RatingAmount = RandRating.Next(35);
                Player = player;
                Opponent = opponent;
            }
        }

        private class NormalGame : BasicGame{
            public NormalGame(GameAccounts.BasicGameAccount player, GameAccounts.BasicGameAccount opponent) : base(player, opponent)
            {
                GameType = "Normal";
            }
        }

        private class PvEGame : BasicGame{
            public PvEGame(GameAccounts.BasicGameAccount player, GameAccounts.BasicGameAccount opponent) : base(player, opponent){
                GameType = "PvE";
            }
        }

        private class TrainingGame : BasicGame{
            public TrainingGame(GameAccounts.BasicGameAccount player, GameAccounts.BasicGameAccount opponent) : base(player, opponent)
            {
                GameType = "Training";
                RatingAmount = 0;
            }
        }

        public class CreateGame{
            public BasicGame CreateNormalGame(GameAccounts.BasicGameAccount player, GameAccounts.BasicGameAccount opponent){
                return new NormalGame(player, opponent);
            }
            
            public BasicGame CreateTrainingGame(GameAccounts.BasicGameAccount player, GameAccounts.BasicGameAccount opponent){
                return new TrainingGame(player, opponent);
            }
            
            public BasicGame CreatePvEGame(GameAccounts.BasicGameAccount player){
                GameAccounts.BasicGameAccount opponent = new GameAccounts.GameAccount("Bots");
                return new PvEGame(player, opponent);
            }
            
        }
    }
}