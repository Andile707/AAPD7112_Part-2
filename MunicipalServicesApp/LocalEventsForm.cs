using MunicipalServicesApp.Models;
using MunicipalServicesApp.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MunicipalServicesApp
{
    public partial class LocalEventsForm : Form
    {
        public LocalEventsForm()
        {
            InitializeComponent();
        }

        private void LocalEventsForm_Load(object sender, EventArgs e)
        {
            LoadSampleEvents();
            LoadCategories();

            BuildUpcomingEventsQueue();

            DisplayAllEvents();
            DisplayRecommendations();
        }
        private void AddEvent(LocalEvent localEvent)
        {
            DateTime date = localEvent.EventDate.Date;

            if (!EventRepository.EventsByDate.ContainsKey(date))
            {
                EventRepository.EventsByDate[date] =
                    new List<LocalEvent>();
            }

            EventRepository.EventsByDate[date].Add(localEvent);

            EventRepository.Categories.Add(localEvent.Category);
        }

        private void LoadSampleEvents()
        {
            if (EventRepository.EventsByDate.Count > 0)
                return;

            AddEvent(new LocalEvent
            {
                EventId = 1,
                Title = "Community Clean-Up",
                Description = "Community clean-up campaign.",
                Category = "Community",
                EventDate = DateTime.Today.AddDays(3),
                Location = "Community Hall"
            });

            AddEvent(new LocalEvent
            {
                EventId = 2,
                Title = "Municipal Sports Day",
                Description = "Sports activities for local residents.",
                Category = "Sports",
                EventDate = DateTime.Today.AddDays(7),
                Location = "Municipal Sports Ground"
            });

            AddEvent(new LocalEvent
            {
                EventId = 3,
                Title = "Water Supply Maintenance",
                Description = "Scheduled water infrastructure maintenance.",
                Category = "Utilities",
                EventDate = DateTime.Today.AddDays(2),
                Location = "Ward 5"
            });

            AddEvent(new LocalEvent
            {
                EventId = 4,
                Title = "Youth Career Workshop",
                Description = "Career development workshop for young residents.",
                Category = "Education",
                EventDate = DateTime.Today.AddDays(10),
                Location = "Civic Centre"
            });
        }

        private void LoadCategories()
        {
            cmbCategory.Items.Clear();

            cmbCategory.Items.Add("All Categories");

            List<string> sortedCategories =
                new List<string>(EventRepository.Categories);

            sortedCategories.Sort();

            foreach (string category in sortedCategories)
            {
                cmbCategory.Items.Add(category);
            }

            cmbCategory.SelectedIndex = 0;
        }

        private void DisplayAllEvents()
        {
            lstEvents.Items.Clear();

            BuildUpcomingEventsQueue();

            foreach (LocalEvent localEvent
                in EventRepository.UpcomingEventsQueue)
            {
                lstEvents.Items.Add(localEvent);
            }
        }

        private void cmbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string selectedCategory = cmbCategory.SelectedItem.ToString();
            DateTime selectedDate = dtpEventDate.Value.Date;

            lstEvents.Items.Clear();

            bool found = false;

            foreach (var dateEntry in EventRepository.EventsByDate)
            {
                foreach (LocalEvent localEvent in dateEntry.Value)
                {
                    bool categoryMatches =
                        selectedCategory == "All Categories" ||
                        localEvent.Category == selectedCategory;

                    bool dateMatches =
                        localEvent.EventDate.Date == selectedDate;

                    if (categoryMatches && dateMatches)
                    {
                        lstEvents.Items.Add(localEvent);
                        found = true;
                    }
                }
            }

            if (!found)
            {
                lstEvents.Items.Add("No events found for this search.");
            }

            RecordSearch(selectedCategory, selectedDate);
        }

        private void RecordSearch(string category, DateTime date)
        {
            string search =
                category + " - " + date.ToString("dd MMM yyyy");

            EventRepository.SearchHistory.Push(search);

            if (category != "All Categories")
            {
                if (EventRepository.SearchPreferences.ContainsKey(category))
                {
                    EventRepository.SearchPreferences[category]++;
                }
                else
                {
                    EventRepository.SearchPreferences[category] = 1;
                }
            }

            DisplaySearchHistory();
            DisplayRecommendations();
        }

        private void DisplaySearchHistory()
        {
            lstSearchHistory.Items.Clear();

            foreach (string search in EventRepository.SearchHistory)
            {
                lstSearchHistory.Items.Add(search);
            }
        }

        private void DisplayRecommendations()
        {
            lstRecommendations.Items.Clear();

            if (EventRepository.SearchPreferences.Count == 0)
            {
                lstRecommendations.Items.Add(
                    "Search for events to receive recommendations.");
                return;
            }

            string favouriteCategory = null;
            int highestCount = 0;

            foreach (var preference in EventRepository.SearchPreferences)
            {
                if (preference.Value > highestCount)
                {
                    highestCount = preference.Value;
                    favouriteCategory = preference.Key;
                }
            }

            bool recommendationFound = false;

            foreach (var dateEntry in EventRepository.EventsByDate)
            {
                foreach (LocalEvent localEvent in dateEntry.Value)
                {
                    if (localEvent.Category == favouriteCategory &&
                        localEvent.EventDate.Date >= DateTime.Today)
                    {
                        lstRecommendations.Items.Add(localEvent);
                        recommendationFound = true;
                    }
                }
            }

            if (!recommendationFound)
            {
                lstRecommendations.Items.Add(
                    "No upcoming events available for " +
                    favouriteCategory + ".");
            }
        }

        private void BuildUpcomingEventsQueue()
        {
            EventRepository.UpcomingEventsQueue.Clear();

            foreach (var dateEntry in EventRepository.EventsByDate)
            {
                foreach (LocalEvent localEvent in dateEntry.Value)
                {
                    if (localEvent.EventDate.Date >= DateTime.Today)
                    {
                        EventRepository.UpcomingEventsQueue.Enqueue(localEvent);
                    }
                }
            }
        }

        private void btnShowAll_Click(object sender, EventArgs e)
        {
            cmbCategory.SelectedIndex = 0;

            DisplayAllEvents();
        }

        private void lblDate_Click(object sender, EventArgs e)
        {

        }

        private void lstSearchHistory_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void lblRecommendations_Click(object sender, EventArgs e)
        {

        }
    }
}
