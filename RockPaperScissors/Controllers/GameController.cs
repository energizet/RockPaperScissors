using Microsoft.AspNetCore.Mvc;
using RockPaperScissors.Controllers.Game;
using RockPaperScissors.Core;

namespace RockPaperScissors.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GameController : ControllerBase
{
	private readonly IGameService _gameService;

	public GameController(IGameService gameService)
	{
		_gameService = gameService;
	}

	[HttpPost("create")]
	public ActionResult<CreateResponse> Create([FromBody] CreateRequest request)
	{
		var gameResult = _gameService.Create(request.IsSingle);
		if (gameResult.IsError)
		{
			return BadRequest(gameResult.Message);
		}

		var game = gameResult.Result!;
		var userResult = game.JoinUser(request.Username);
		if (userResult.IsError)
		{
			return BadRequest(userResult.Message);
		}

		return new CreateResponse(game.Id, userResult.Result!.Id);
	}

	[HttpPost("join")]
	public ActionResult<JoinResponse> Join([FromBody] JoinRequest request)
	{
		var gameResult = _gameService.Find(request.GameId);
		if (gameResult.IsError)
		{
			return BadRequest(gameResult.Message);
		}

		var game = gameResult.Result!;
		var userResult = game.JoinUser(request.Username);
		if (userResult.IsError)
		{
			return BadRequest(userResult.Message);
		}

		return new JoinResponse(userResult.Result!.Id);
	}

	[HttpPut("turn")]
	public ActionResult<TurnResponse> Turn([FromBody] TurnRequest request)
	{
		var gameResult = _gameService.Find(request.GameId);
		if (gameResult.IsError)
		{
			return BadRequest(gameResult.Message);
		}

		var game = gameResult.Result!;
		var roundResult = game.Turn(request.UserId, request.Turn);
		if (roundResult.IsError)
		{
			return BadRequest(roundResult.Message);
		}

		var round = roundResult.Result!;
		return new TurnResponse(
			round.Winner.Id,
			round.Turns
				.Select(item => new TurnStatResponse(
					item.Key.Id,
					item.Value
				)).ToList(),
			round.Status
		);
	}

	[HttpGet("stat/{gameId}")]
	public ActionResult<StatResponse> Stat(Guid gameId)
	{
		var gameResult = _gameService.Find(gameId);
		if (gameResult.IsError)
		{
			return BadRequest(gameResult.Message);
		}

		var game = gameResult.Result!;
		return new StatResponse(
			game.Users,
			game.Winner.Id,
			game.Rounds
				.Select(item => new StatRoundResponse(
					item.Winner.Id,
					item.Turns
						.Select(item2 => new StatRoundTurnResponse(
							item2.Key.Id,
							item2.Value
						)).ToList(),
					item.Status
				)).ToList(),
			game.WinStreaks
				.Select(item => new StatisticResponse(
					item.Key.Id,
					item.Value
				)).ToList()
		);
	}
}