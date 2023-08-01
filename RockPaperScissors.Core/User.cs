namespace RockPaperScissors.Core;

public class User
{
	public static readonly User Empty = new(Guid.Empty, string.Empty);

	public Guid Id { get; }
	public string Username { get; }

	public User(Guid id, string username)
	{
		Id = id;
		Username = username;
	}

	public override bool Equals(object? obj)
	{
		if (obj is not User user)
		{
			return false;
		}

		return Equals(user);
	}

	public bool Equals(User user)
	{
		return Id == user.Id;
	}

	public override int GetHashCode()
	{
		return Id.GetHashCode();
	}
}