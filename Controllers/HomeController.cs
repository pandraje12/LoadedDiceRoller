using DiceRoller.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DiceRoller.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        /*
         This function is the captures the post method from the front end. 
        It expects favored face for dice 1, favored factor for dice 1, 
            favored face for dice 2, favored factor for dice 2 and total number of dice rolls as an argument.
        It calls the helper function "RollDice" to generate the random number of each roll.
        It appends the result to the list and returns the view. 
         */

        // POST: /Home/RollDice
        [HttpPost]
        public IActionResult RollDice(int favoredFace1, int favoredFactor1, int favoredFace2, int favoredFactor2, int numRolls)
        {
            // Create a list to store the results of each roll
            List<string> results = new List<string>();
            string result = "";
            // Roll the dice the specified number of times
            for (int i = 0; i < numRolls; i++)
            {
                result = "";
                result = RollDice(favoredFace1, favoredFactor1, favoredFace2, favoredFactor2);                
                // Add the result of the roll to the list
                results.Add(result);
            }

            // Pass the results to the view
            return View("Index", results);
        }

        /*
         This function takes favoredface as input for 2 dices.
         This function also takes fovured factors for the favoured face as input.
         It generates random rumbers between 1 to 6 inclusive and tries.
            If the generated random runmber is not favoured face then it tries to generate the 
                favoured face again and again until the favouredFactor times. 
         It returns string with two dice rolls e.g. ("2,5").
         */
        public string RollDice(int favoredFace1, int favoredFactor1, int favoredFace2, int favoredFactor2)
        {
            string retVal = "";
            // Create two random number generators, one for each die
            Random rand1 = new Random();
            Random rand2 = new Random();
            // Roll first dice
            int firstDiceRoll = rand1.Next(1, 7);           
            for (int i = 1; i < favoredFactor1; i++)
            {
                if (firstDiceRoll == favoredFace1)
                    break;
                else
                    firstDiceRoll = rand1.Next(1, 7);
            }
            // Roll second dice
            int secondDiceRoll = rand2.Next(1, 7);
            for (int i = 1; i < favoredFactor2; i++)
            {
                if (secondDiceRoll == favoredFace2)
                    break;
                else
                    secondDiceRoll = rand2.Next(1, 7);
            }
            retVal = firstDiceRoll.ToString() + "," + secondDiceRoll.ToString();
            return retVal;
        }
    }
}



