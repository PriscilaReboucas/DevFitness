using System;

namespace DevFitness.API.Core.Entities
{
    public class Meal : BaseEntity
    {

        public Meal()
        {

        }
        public Meal(string descripton, int calories, DateTime date, int userId) : base()
        {
            Descripton = descripton;
            Calories = calories;
            Date = date;
            UserId = userId;
        }

        public string Descripton { get; private set; }

        public int Calories { get; private set; }

        public DateTime Date { get; private set; }

        public int UserId { get; set; }

        public void Update(string descripton, int calories, DateTime date)
        {
            Descripton = descripton;
            Calories = calories;
            Date = date;
        }

    }
}