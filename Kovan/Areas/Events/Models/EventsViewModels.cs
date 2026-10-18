using Kovan.Core.Entities;
using Kovan.Core.Enums;
using Kovan.Core.Helpers;
using Kovan.Models;

namespace Kovan.Areas.Events.Models;

/// <summary>Events/Index: kategori sekmeleri + etkinlik kartları.</summary>
public class EventIndexViewModel
{
    public EventCategory? SelectedCategory { get; set; }
    public List<EventCardViewModel> Events { get; set; } = new();
}

/// <summary>Events/Details</summary>
public class EventDetailsViewModel
{
    public EventCardViewModel Card { get; set; } = new();
    public string Description { get; set; } = string.Empty;
    public string TimeRange { get; set; } = string.Empty;
    public string OrganizerName { get; set; } = string.Empty;
    public string OrganizerInitials { get; set; } = string.Empty;
    public int RemainingSeats { get; set; }
    public int WaitlistCount { get; set; }

    public static EventDetailsViewModel FromEntity(CampusEvent e) => new()
    {
        Card = EventCardViewModel.FromEntity(e),
        Description = e.Description,
        TimeRange = $"{e.StartsAt:HH:mm} – {e.EndsAt:HH:mm}",
        OrganizerName = e.Organizer?.FullName ?? "-",
        OrganizerInitials = e.Organizer?.Initials ?? "?",
        RemainingSeats = e.RemainingSeats,
        WaitlistCount = e.WaitlistCount
    };
}

/// <summary>Takvimdeki bir gün hücresi.</summary>
public class CalendarDayViewModel
{
    public DateTime Date { get; set; }
    public bool IsCurrentMonth { get; set; }
    public bool IsToday { get; set; }
    public List<(int Id, string Title, string Icon, string Time)> Events { get; set; } = new();
}

/// <summary>Events/Calendar: aylık takvim ızgarası.</summary>
public class CalendarViewModel
{
    public int Year { get; set; }
    public int Month { get; set; }
    public string MonthTitle { get; set; } = string.Empty;
    public List<CalendarDayViewModel> Days { get; set; } = new();   // 6 hafta x 7 gün = 42 hücre

    public DateTime Prev => new DateTime(Year, Month, 1).AddMonths(-1);
    public DateTime Next => new DateTime(Year, Month, 1).AddMonths(1);

    /// <summary>
    /// Verilen ay için Pazartesi'den başlayan 42 günlük takvim ızgarasını oluşturur.
    /// </summary>
    public static CalendarViewModel Build(int year, int month, IEnumerable<CampusEvent> events)
    {
        var first = new DateTime(year, month, 1);
        // Pazartesi = 0 olacak şekilde kaydırma (DayOfWeek.Sunday = 0 olduğu için)
        int offset = ((int)first.DayOfWeek + 6) % 7;
        var start = first.AddDays(-offset);

        var vm = new CalendarViewModel
        {
            Year = year,
            Month = month,
            MonthTitle = first.ToString("MMMM yyyy", DisplayHelpers.Tr)
        };

        for (int i = 0; i < 42; i++)
        {
            var day = start.AddDays(i);
            vm.Days.Add(new CalendarDayViewModel
            {
                Date = day,
                IsCurrentMonth = day.Month == month,
                IsToday = day.Date == DateTime.Today,
                Events = events.Where(e => e.StartsAt.Date == day.Date)
                               .Select(e => (e.Id, e.Title, e.Category.ToIcon(), e.StartsAt.ToString("HH:mm")))
                               .ToList()
            });
        }
        return vm;
    }
}
