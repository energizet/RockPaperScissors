namespace RockPaperScissors.Core;

public class Game
{
	private const int MaxUsersCount = 2;
	private readonly int _maxWins;
	private readonly List<User> _users = new();
	private readonly List<Round> _rounds = new();

	public Guid Id { get; } = Guid.NewGuid();
	public IReadOnlyCollection<User> Users => _users;

	public IReadOnlyCollection<Round> Rounds => _rounds;

	public GameStatus Status { get; private set; } = GameStatus.Created;

	public User Winner { get; private set; } = User.Empty;

	public Dictionary<User, int> WinStreaks { get; private set; } = new();

	public Game(int maxWins = 3)
	{
		_maxWins = maxWins;
	}

	public ResultFor<User> JoinUser(string username)
	{
		if (Status != GameStatus.Created)
		{
			return new("Cannot join to already started game");
		}

		var user = new User(Guid.NewGuid(), username);
		_users.Add(user);
		if (_users.Count == MaxUsersCount)
		{
			Status = GameStatus.InProgress;
		}

		return new(user);
	}

	public ResultFor<Round> Turn(Guid userId, TurnOptions turnOptions)
	{
		if (Status != GameStatus.InProgress)
		{
			return new("Cannot turn to not in progress game");
		}

		var user = _users.FirstOrDefault(item => item.Id == userId);
		if (user == null)
		{
			return new("User not found");
		}

		var round = GetRound();
		var turnResult = round.Turn(user, turnOptions);
		if (turnResult.IsError)
		{
			return new(turnResult.Message);
		}

		CheckFinish();
		return new(round);
	}

	protected Round GetRound()
	{
		var round = _rounds.LastOrDefault();
		if (round is { IsFinished: false })
		{
			return round;
		}

		round = new Round(_users.Count);
		_rounds.Add(round);

		return round;
	}

	protected virtual void CheckFinish()
	{
		var winStreak = GetWinStreak();
		if (winStreak.winsCount == _maxWins)
		{
			Finish(winStreak.user);
		}
	}

	private void Finish(User winner)
	{
		Status = GameStatus.Finished;
		Winner = winner;
	}

	private (User user, int winsCount) GetWinStreak()
	{
		SaveWinStreaks();
		if (WinStreaks.Any() == false)
		{
			return (User.Empty, 0);
		}

		var mostWins = WinStreaks.MaxBy(item => item.Value);
		return (mostWins.Key, mostWins.Value);
	}

	private void SaveWinStreaks()
	{
		WinStreaks = _rounds.Where(item => item is { IsFinished: true, IsDrawGame: false })
			.GroupBy(item => item.Winner, (key, group) => new
			{
				User = key,
				WinsCount = group.Count(),
			})
			.ToDictionary(item => item.User, item => item.WinsCount);
	}
}