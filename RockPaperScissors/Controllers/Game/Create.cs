namespace RockPaperScissors.Controllers.Game;

public record CreateRequest(string Username, bool IsSingle);

public record CreateResponse(Guid GameId, Guid UserId);