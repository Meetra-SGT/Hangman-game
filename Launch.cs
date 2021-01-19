using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Hangman
{
    enum CategoryEnum
    {
        FOOD,
        ANIMALS,
        TRAVEL
    }
    class Launch
    {
        public CategoryEnum CategoryEnum { get; set; }

        public string WordToGuess { get; set; }

        public List<string> LettersToGuess { get; set; } = new List<string>();

        public List<string> UnderlineList { get; set; } = new List<string>();

        public void PrintGreeting()
        {
            Console.Write("     WELCOME TO THE");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("   H A N G M A N   ");
            Console.ResetColor();
            Console.Write("GAME!");
            Console.WriteLine(); //break
            Console.WriteLine(); //break
            Thread.Sleep(1200);
            Console.WriteLine("Your task is to guess the letters to reveal the hidden word!");
            Console.WriteLine(); //break
            Thread.Sleep(1200);
            Console.WriteLine("You may, at any time, attempt to guess the whole word!");
            Console.WriteLine(); //break
            Thread.Sleep(1200);
            Console.WriteLine(@">>>  To exit the game - just type ""stop""  <<<");
            Console.WriteLine(); //break
            Thread.Sleep(1200);
        }
        public CategoryEnum ChoosingCategory()
        {
            Console.WriteLine("Now select the category: ");
            Console.WriteLine(); //break
            Console.WriteLine($"{CategoryEnum.FOOD}    - press 1");
            Console.WriteLine($"{CategoryEnum.ANIMALS} - press 2");
            Console.WriteLine($"{CategoryEnum.TRAVEL}  - press 3");
            Console.WriteLine(); //break

            string userInput = Console.ReadLine();
            bool IfNumber = int.TryParse(userInput, out int number);  //checking if is a number

            while (!IfNumber)
            {
                Console.WriteLine("Wrong input");
                userInput = Console.ReadLine();
                IfNumber = int.TryParse(userInput, out number);
            }

            if (IfNumber) //assigning category
            {
                if (number == 1)
                {
                    CategoryEnum = CategoryEnum.FOOD;
                }
                else if (number == 2)
                {
                    CategoryEnum = CategoryEnum.ANIMALS;
                }
                else if (number == 3)
                {
                    CategoryEnum = CategoryEnum.TRAVEL;
                }
                else
                {
                    Console.WriteLine("Default category selected");
                }
            }
            return CategoryEnum;
        }
        public string GetRandomWord() //assigning random word
        {
            if (CategoryEnum == CategoryEnum.FOOD)
            {
                string[] food = System.IO.File.ReadAllLines(@"C:\Users\37126\Documents\Mētra_.net\Victoria and Meetra\Hangman\Hangman\food.txt");
                WordToGuess = food[new Random().Next(0, food.Length)];
                Choosed();
                return WordToGuess;
            }
            else if (CategoryEnum == CategoryEnum.ANIMALS)
            {
                string[] animals = System.IO.File.ReadAllLines(@"C:\Users\37126\Documents\Mētra_.net\Victoria and Meetra\Hangman\Hangman\animals.txt");
                WordToGuess = animals[new Random().Next(0, animals.Length)];
                Choosed();
                return WordToGuess;
            }
            else
            {
                string[] travel = System.IO.File.ReadAllLines(@"C:\Users\37126\Documents\Mētra_.net\Victoria and Meetra\Hangman\Hangman\travel.txt");
                WordToGuess = travel[new Random().Next(0, travel.Length)];
                Choosed();
                return WordToGuess;
            }
        }
        private void Choosed()
        {
            Console.WriteLine($"You chose: {CategoryEnum} ");
            Console.WriteLine();
            Console.WriteLine("Let's begin!");
            Console.WriteLine();
        }
    }

}
