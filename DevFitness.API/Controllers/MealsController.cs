using AutoMapper;
using DevFitness.API.Core.Entities;
using DevFitness.API.Models.InputModels;
using DevFitness.API.Models.ViewModels;
using DevFitness.API.Persistence;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace DevFitness.API.Controllers
{
    //api/users/4/meals
    [Route("api/users/{userId}/meals")]
    public class MealsController : ControllerBase
    {
        private readonly DevFitnessDbContext _dbContext;
        private readonly IMapper _mapper;

        public MealsController(DevFitnessDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }


        //api/users/4/meals
        [HttpGet]
        public IActionResult GetAll(int userId)
        {
            var meals = _dbContext.Meals.Where(x => x.UserId == userId && x.Active);
            if (meals == null)
                return NotFound();

            var allMealsViewModel = meals.Select(x => new MealViewModel(x.Id, x.Descripton, x.Calories, x.Date));
            return Ok(allMealsViewModel);
        }

        //api/users/4/meals/16
        [HttpGet("{mealId}")]
        public IActionResult Get(int userId, int mealId)
        {
            var meal = _dbContext.Meals.SingleOrDefault(x => x.Id == mealId && x.UserId == userId);
            if (meal == null)
                return NotFound();

            var mealViewModel = new MealViewModel(meal.Id, meal.Descripton, meal.Calories, meal.Date);
            return Ok(mealViewModel);
        }

        //api/users/4/meals
        [HttpPost]
        public IActionResult Post(int userId, [FromBody] CreateMealInputModel inputModel)
        {

            var meal = _mapper.Map<Meal>(inputModel);
            meal.UserId = userId;

            //var meal = new Meal(inputModel.Descripton, inputModel.Calories, inputModel.Date, userId);
            _dbContext.Meals.Add(meal);
            _dbContext.SaveChanges();

            return CreatedAtAction(nameof(Get), new { userId = userId, mealId = meal.Id }, inputModel); // redireciona para essa url
        }

        //api/users/4/meals/16 HTTP PUT
        [HttpPut("{mealId}")]
        public IActionResult Put(int userId, int mealId, [FromBody] UpdateMealInputModel inputModel)
        {
            var meal = _dbContext.Meals.SingleOrDefault(x => x.Id == mealId && x.UserId == userId);
            if (meal == null)
                return NotFound();

            meal.Update(inputModel.Descripton, inputModel.Calories, inputModel.Date);
            _dbContext.SaveChanges();

            return NoContent();
        }

        //api/users/4/meals/16  HTTP DELETE
        [HttpDelete("{mealId}")]
        public IActionResult Delete(int userId, int mealId)
        {
            var meal = _dbContext.Meals.SingleOrDefault(x => x.Id == mealId && x.UserId == userId);
            if (meal == null)
                return NotFound();

            meal.Desactivate();
            _dbContext.SaveChanges();

            return NoContent();
        }

    }
}
