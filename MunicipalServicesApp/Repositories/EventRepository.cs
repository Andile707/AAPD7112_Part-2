using System;
using System.Collections.Generic;
using MunicipalServicesApp.Models;

namespace MunicipalServicesApp.Repositories
{
    public static class EventRepository
    {
        public static SortedDictionary<DateTime, List<LocalEvent>> EventsByDate
            = new SortedDictionary<DateTime, List<LocalEvent>>();

        public static HashSet<string> Categories
            = new HashSet<string>();

        public static Stack<string> SearchHistory
            = new Stack<string>();

        public static Dictionary<string, int> SearchPreferences
            = new Dictionary<string, int>();

        public static Queue<LocalEvent> UpcomingEventsQueue
             = new Queue<LocalEvent>();
    }
}
