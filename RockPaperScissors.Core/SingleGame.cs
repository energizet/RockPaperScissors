namespace RockPaperScissors.Core;

public class SingleGame : Game
{
	private readonly User _pc;
	private readonly Random _random = new();

	public SingleGame(int maxWins = 3) : base(maxWins)
	{
		_pc = JoinUser("PC").Result!;
	}

	protected override void CheckFinish()
	{
		var round = GetRound();
		round.Turn(_pc, (TurnOptions)_random.Next(3));
		base.CheckFinish();
	}
}