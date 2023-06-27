namespace ThaiDuongWarehouse.Api.Applications.Exceptions;

public class NotAllowedToDeleteException : Exception
{
	public NotAllowedToDeleteException(string message) : base(message)
	{

	}
}
