using Domain.Abstractions;


namespace Infrastructure.Services;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime GetCurrentTime()
    {
        return DateTime.UtcNow;
    }
}
