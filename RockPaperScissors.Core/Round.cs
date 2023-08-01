namespace RockPaperScissors.Core;

public class Round
{
	private readonly int _usersCount;
	private RoundStatus _status = RoundStatus.InProgress;

	public Dictionary<User, TurnOptions> Turns { get; } = new();
	public User Winner { get; private set; } = User.Empty;

	public RoundStatus Status => _status;
	public bool IsFinished => _status is RoundStatus.Finished or RoundStatus.DrawGame;
	public bool IsDrawGame => _status is RoundStatus.DrawGame;


	public Round(int usersCount)
	{
		_usersCount = usersCount;
	}

	public ResultFor<bool> Turn(User user, TurnOptions turnOptions)
	{
		if (Turns.ContainsKey(user))
		{
			return new("User turned");
		}

		Turns[user] = turnOptions;

		CheckFinish();
		return new(true);
	}

	private void CheckFinish()
	{
		if (Turns.Count == _usersCount)
		{
			Finish();
		}
	}

	private void Finish()
	{
		_status = RoundStatus.Finished;
		FindAndSetWinner();
	}

	private void FindAndSetWinner()
	{
		var user1 = Turns.First();
		var user2 = Turns.Skip(1).First();

		if (user1.Value == user2.Value)
		{
			_status = RoundStatus.DrawGame;
			return;
		}

		Winner = IsFirstWinner(user1.Value, user2.Value) ? user1.Key : user2.Key;
	}

	private bool IsFirstWinner(TurnOptions turn1, TurnOptions turn2)
	{
		return turn1 == TurnOptions.Rock && turn2 == TurnOptions.Scissors
		       || turn1 == TurnOptions.Scissors && turn2 == TurnOptions.Paper
		       || turn1 == TurnOptions.Paper && turn2 == TurnOptions.Rock;
	}
}