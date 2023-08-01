namespace RockPaperScissors.Core;

public interface IGameService
{
	ResultFor<Game> Create(bool isSingle);

	ResultFor<Game> Find(Guid gameId);
}