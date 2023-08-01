namespace RockPaperScissors.Controllers.Game;

public record JoinRequest(Guid GameId, string Username);

public record JoinResponse(Guid UserId);
