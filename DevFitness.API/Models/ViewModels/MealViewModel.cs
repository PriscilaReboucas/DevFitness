using System;

namespace DevFitness.API.Models.ViewModels
{
    public class MealViewModel
    {
        public MealViewModel(int id, string descripton, int calories, DateTime date)
        {
            Id = id;
            Descripton = descripton;
            Calories = calories;
            Date = date;
        }

        public int Id { get; private set; }
        public string Descripton { get; private set; }
        public int Calories { get; private set; }
        public DateTime Date { get; private set; }

    }
}
