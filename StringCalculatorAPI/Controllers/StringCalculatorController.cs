using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace StringCalculatorAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StringCalculatorController : ControllerBase
    {
        private const string SEPARATOR = ",";
        private static int CalledCount = 0;

        // GET: api/StringCalculator
        [HttpGet]
        public int GetCalledCount()
        {
            return CalledCount;
        }

        // GET: api/StringCalculator/Add
        [HttpPost]
        public string Add(QueryTestRequest testRequest)
        {
            string number = testRequest.Number;
            CalledCount = CalledCount + 1;
            if (!String.IsNullOrEmpty(number.Trim()))
            {
                string numbersWithoutDelimiter = Regex.Replace(number, @"[^\d{0}-]", SEPARATOR);
                return AddNumbers(numbersWithoutDelimiter);
            }
            else
            {
                return "0";
            }
        }

        private static string AddNumbers(String numbers)
        {
            int returnValue = 0;

            String[] numbersArray = numbers.Split(SEPARATOR);
            
            StringBuilder negativeNumbers = new StringBuilder();

            foreach (string number in numbersArray)
            {
                if (number.Trim().Length > 0)
                {
                    int numberInt = Convert.ToInt32(number.Trim());
                    if ((numberInt < 0))
                    {
                        negativeNumbers.Append(numberInt);
                    }
                    else if ((numberInt <= 1000))
                    {
                        returnValue = (returnValue + numberInt);
                    }

                }

            }

            if ((negativeNumbers.Length > 0))
            {
                //throw Exception("Negative Not allowed : " + Convert.ToString(negativeNumbers));

                return Convert.ToString("Negatives not allowed: " + Convert.ToString(negativeNumbers));
            }

            return Convert.ToString(returnValue);
        }
    }

    public class QueryTestRequest
    {
        public string Number { get; set; }
    }
}
