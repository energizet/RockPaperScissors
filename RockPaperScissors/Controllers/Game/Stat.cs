using RockPaperScissors.Core;

namespace RockPaperScissors.Controllers.Game;


public record StatRequest(Guid GameId);

public record StatResponse(
	IReadOnlyCollection<User> Users,
	Guid WinnerId,
	IReadOnlyCollection<StatRoundResponse> Rounds,
	IReadOnlyCollection<StatisticResponse> Stats
);

public record StatRoundResponse(
	Guid WinnerId,
	IReadOnlyCollection<StatRoundTurnResponse> Turns,
	RoundStatus TurnOptions
);

public record StatRoundTurnResponse(Guid UserId, TurnOptions Turn);

public record StatisticResponse(Guid UserId, int WinsCount);
