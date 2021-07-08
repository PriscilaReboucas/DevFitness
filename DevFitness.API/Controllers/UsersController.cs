using AutoMapper;
using DevFitness.API.Core.Entities;
using DevFitness.API.Models.InputModels;
using DevFitness.API.Models.ViewModels;
using DevFitness.API.Persistence;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace DevFitness.API.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly DevFitnessDbContext _dbContext;
        private readonly IMapper _mapper;

        public UsersController(DevFitnessDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        //api/users/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {

            var user = _dbContext.Users.SingleOrDefault(u => u.Id == id);
            if (user == null)
                return NotFound();

            // usando o automapper
            var userViewModel = _mapper.Map<UserViewModel>(user);
            //var userViewModel = new UserViewModel(user.Id, user.FullName, user.Height, user.Weight, user.BirthDate); 

            return Ok(userViewModel);

        }

        //api/users - método HTTP POST
        [HttpPost]
        public IActionResult Post([FromBody] CreateUserInputModel inputModel)
        {
            var user = new User(inputModel.FullName, inputModel.Height, inputModel.Weight, inputModel.BirthDate);
            _dbContext.Add(user);
            _dbContext.SaveChanges();

            return CreatedAtAction(nameof(Get), new { id = user.Id }, inputModel);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UpdateUserInputModel inputModel)
        {
            var user = _dbContext.Users.SingleOrDefault(u => u.Id == id);
            if (user == null)
                return NotFound();

            user.Update(inputModel.Height, inputModel.Weight);
            _dbContext.SaveChanges();

            return NoContent();
        }
    }
}
