using RockPaperScissors.Core;

namespace RockPaperScissors.Controllers.Game;

public record TurnRequest(Guid GameId, Guid UserId, TurnOptions Turn);

public record TurnResponse(
	Guid WinnerId,
	IReadOnlyCollection<TurnStatResponse> Turns,
	RoundStatus Status
);

public record TurnStatResponse(Guid UserId, TurnOptions Turn);