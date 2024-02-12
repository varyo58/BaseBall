namespace BaseBall.Model;

public class Team
{
    private Player[] Players { get; }
    private int Current { get; set; }

    public Team(Player[] players)
    {
        Players = players;
    }

    public Team(Player player1, Player player2, Player player3, Player player4, Player player5, Player player6, Player player7, Player player8, Player player9)
    {
        Players =
        [
            player1,
            player2,
            player3,
            player4,
            player5,
            player6,
            player7,
            player8,
            player9
        ];

        Current = 0;
    }

    public Player NextBatter()
    {
        var nextBatter = Players[Current];
        Current = (Current + 1) % 9;
        return nextBatter;
    }

}