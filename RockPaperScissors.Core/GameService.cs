using System.Collections.Concurrent;

namespace RockPaperScissors.Core;

public class GameService : IGameService
{
	private readonly ConcurrentDictionary<Guid, Game> _games = new();

	public ResultFor<Game> Create(bool requestIsSingle)
	{
		var game = requestIsSingle ? new SingleGame() : new Game();
		_games[game.Id] = game;

		return new(game);
	}

	public ResultFor<Game> Find(Guid gameId)
	{
		if (_games.ContainsKey(gameId) == false)
		{
			return new("Game not found");
		}

		return new(_games[gameId]);
	}
}