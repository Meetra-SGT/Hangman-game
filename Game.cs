using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Hangman
{
    class Game: Launch
    {
        private List<string> WrongGuess { get; set; } = new List<string>();
        private List<string> Keyboard { get; set; }
        private List<string> KeyboardBorders { get; set; }
        private readonly string alphabet = "abcdefghijklmnopqrstuvwxyz";

        public void AddLettersToTheGuessingList(string wordToGuess) //adding letters to the list
        {
            for (int i = 0; i < wordToGuess.Length; i++)
            {
                LettersToGuess.Add(wordToGuess.Substring(i, 1));
            }
        }
        public void PrintUnderlines(string wordToGuess) //method for printing underlines
        {
            for (int i = 0; i < wordToGuess.Length; i++)
            {
                UnderlineList.Add("_");
                Console.Write($"{UnderlineList[i]} ");
            }
        }
        public void GuessingPart(string WordToGuess) // the main part of the game
        {
            bool Won = true;
            CreateKeyboard();
            int frequency;
            int duration;
            int wrongGuessCounts = 0;
            do
            {
                bool equal = UnderlineList.SequenceEqual(LettersToGuess);  //comparing two lists
                if (equal)
                {
                    Won = true;
                    WinnerSound();
                    Console.WriteLine("CONGRATS, YOU WON!!!");
                    Console.WriteLine(); //break
                    AnotherRound(); //suggesting another round
                }
                Console.WriteLine("Enter a letter:");
                string userGuess = Console.ReadLine();
                Thread.Sleep(500);
                Console.Clear();
                if (userGuess.ToLower() == "stop") //leaves when input "stop"
                {
                    Console.WriteLine("See you next time!!");
                    Environment.Exit(0);
                    break;
                }
                if (userGuess.ToLower() == WordToGuess)
                {
                    WinnerSound();
                    Console.WriteLine("CONGRATS, YOU WON!!!");
                    Console.WriteLine(); //break
                    AnotherRound(); //suggesting another round
                    break;
                }
                if (string.IsNullOrEmpty(userGuess)) //if the input is empty
                {
                    Console.WriteLine("You didn't input anything!");
                    Console.WriteLine(); //break
                    continue;
                }
                if (userGuess.ToLower() != WordToGuess && !alphabet.Contains(userGuess.ToLower())) //checking if not a symbol etc.
                {
                    Console.WriteLine("It has to be a single letter or the whole word...");
                    Console.WriteLine(); //break
                    continue;
                }
                if (LetterRepeats(userGuess))
                    Console.WriteLine($"You already inputted '{userGuess}'");
                Console.WriteLine(); //break
                if (WrongLetter(userGuess) && !LetterRepeats(userGuess))
                {
                    Console.WriteLine("Wrong letter!");
                    wrongGuessCounts++; //when wrong letter - count +1; 
                    frequency = 300;
                    duration = 900;
                    Console.Beep(frequency, duration);
                    HangmanDrawing(wrongGuessCounts); //draws Hangman
                    Console.WriteLine(); //break
                    Console.WriteLine(); //break
                }
                if (WrongLetter(userGuess) && wrongGuessCounts > 9)
                {
                    Console.Beep(280, 400);
                    Console.Beep(474, 200);
                    Console.Beep(344, 700);
                    Console.WriteLine("Oh,no.. you lose!");
                    Console.WriteLine(); //break
                    AnotherRound();      //suggesting another round    
                    break;
                }
                for (int i = 0; i < LettersToGuess.Count; i++)
                {
                    if (string.Equals(LettersToGuess[i], userGuess, StringComparison.OrdinalIgnoreCase))
                    {
                        UnderlineList[i] = LettersToGuess[i]; //adding guessed letters to the underline list
                    }
                    Console.Write($"{UnderlineList[i]} "); //printing underlines
                }
                Console.WriteLine(); //break
                Console.WriteLine(); //break
                if (!equal)
                {
                    Console.WriteLine($"You have {10 - wrongGuessCounts} attempts!"); //showing how many attempts left
                }
                Console.WriteLine(); //break

                PrintKeyboard(0, alphabet.Length / 2, userGuess.ToLower()); //printing the needed part of the keyboard
                PrintKeyboard(alphabet.Length / 2, alphabet.Length / 2, userGuess.ToLower()); //printing the needed part of the keyboard

                Console.WriteLine(); //break

            } while (Won);
        }
        private bool LetterRepeats(string userGuess) //checking if the letter repeats
        {
            return UnderlineList.Contains(userGuess.ToLower())
                   || WrongGuess.Contains(userGuess.ToLower()); //comparing guessed letters with userguess
        }
        private bool WrongLetter(string userGuess) // checking if the letter is correct
        {
            if (LettersToGuess.Contains(userGuess.ToLower()))
            {
                return false;
            }
            return true;
        }
        private void PrintKeyboard(int x, int y, string userGuess)
        {
            for (int i = 0; i < y; i++)  //printing the needed part of the upper border
            {
                Console.Write($" {KeyboardBorders[i]}  ");
            }
            Console.WriteLine(); //moving to the next line

            List<string> keyboardPart = Keyboard.GetRange(x, y).ToList();  //dividing keyboard into several parts

            if (Keyboard.Contains(userGuess) && !LettersToGuess.Contains(userGuess)) //adding wrong guesses to the new list for coloring
            {
                WrongGuess.Add(userGuess);
            }
            foreach (var item in keyboardPart)
            {
                if (UnderlineList.Contains(item))
                {
                    Console.ForegroundColor = ConsoleColor.Green; //coloring correct letters
                }
                if (WrongGuess.Contains(item))
                {
                    Console.ForegroundColor = ConsoleColor.Red; //coloring wrong letters
                }
                Console.Write($"| {item.ToUpper()} | "); //printing the needed part of the keyboard  letters
                Console.ResetColor(); // reset colors 
            }
            Console.WriteLine(); //moving to the next line
            for (int i = 0; i < y; i++)
            {
                Console.Write($" {KeyboardBorders[i]}  "); //printing the needed part of the bottom border (same border list)
            }
            Console.WriteLine(); //break
        }
        private void CreateKeyboard()
        {
            Keyboard = new List<string>();
            for (int i = 0; i < alphabet.Length; i++)  //adding alphabet letters to a list
            {
                Keyboard.Add(alphabet.Substring(i, 1));
            }
            KeyboardBorders = new List<string>();
            for (int i = 0; i < alphabet.Length; i++)  //adding border lines to a list
            {
                KeyboardBorders.Add("---");
            }
        }
        private void HangmanDrawing(int WrongGuessCount)
        {
            if (WrongGuessCount == 1)
            {
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine("             ");
                Console.WriteLine("             ");
                Console.WriteLine("             ");
                Console.WriteLine("             ");
                Console.WriteLine("             ");
                Console.WriteLine("             ");
                Console.WriteLine("+----+       ");
                Console.ResetColor();
            }
            if (WrongGuessCount == 2)
            {
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine("             ");
                Console.WriteLine("|            ");
                Console.WriteLine("|            ");
                Console.WriteLine("|            ");
                Console.WriteLine("|            ");
                Console.WriteLine("|            ");
                Console.WriteLine("+----+       ");
                Console.ResetColor();
            }
            else if (WrongGuessCount == 3)
            {
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine("+---+        ");
                Console.WriteLine("|            ");
                Console.WriteLine("|            ");
                Console.WriteLine("|            ");
                Console.WriteLine("|            ");
                Console.WriteLine("|            ");
                Console.WriteLine("+----+       ");
                Console.ResetColor();
            }
            else if (WrongGuessCount == 4)
            {
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine("+---+        ");
                Console.WriteLine("|   |        ");
                Console.WriteLine("|            ");
                Console.WriteLine("|            ");
                Console.WriteLine("|            ");
                Console.WriteLine("|            ");
                Console.WriteLine("+----+       ");
                Console.ResetColor();
            }
            else if (WrongGuessCount == 5)
            {
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine("+---+        ");
                Console.WriteLine("|   |        ");
                Console.WriteLine("|   0        ");
                Console.WriteLine("|            ");
                Console.WriteLine("|            ");
                Console.WriteLine("|            ");
                Console.WriteLine("+----+       ");
                Console.ResetColor();
            }
            else if (WrongGuessCount == 6)
            {
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine("+---+        ");
                Console.WriteLine("|   |        ");
                Console.WriteLine("|   0        ");
                Console.WriteLine("|   |        ");
                Console.WriteLine("|            ");
                Console.WriteLine("|            ");
                Console.WriteLine("+----+       ");
                Console.ResetColor();
            }
            else if (WrongGuessCount == 7)
            {
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine("+---+        ");
                Console.WriteLine("|   |        ");
                Console.WriteLine("|   0        ");
                Console.WriteLine("|   |\\       ");
                Console.WriteLine("|            ");
                Console.WriteLine("|            ");
                Console.WriteLine("+----+       ");
                Console.ResetColor();
            }
            else if (WrongGuessCount == 8)
            {
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine("+---+        ");
                Console.WriteLine("|   |        ");
                Console.WriteLine("|   0        ");
                Console.WriteLine("|  /|\\       ");
                Console.WriteLine("|            ");
                Console.WriteLine("|            ");
                Console.WriteLine("+----+       ");
                Console.ResetColor();
            }
            else if (WrongGuessCount == 9)
            {
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine("+---+        ");
                Console.WriteLine("|   |        ");
                Console.WriteLine("|   0        ");
                Console.WriteLine("|  /|\\       ");
                Console.WriteLine("|    \\       ");
                Console.WriteLine("|            ");
                Console.WriteLine("+----+       ");
                Console.ResetColor();
            }
            else if (WrongGuessCount == 10)
            {
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine("+---+        ");
                Console.WriteLine("|   |        ");
                Console.WriteLine("|   0        ");
                Console.WriteLine("|  /|\\       ");
                Console.WriteLine("|  / \\       ");
                Console.WriteLine("|            ");
                Console.WriteLine("+----+       ");
                Console.ResetColor();
            }
        }
        private void WinnerSound()
        {
            Console.Beep(523, 200);
            Console.Beep(523, 100);
            Console.Beep(659, 700);
            Console.Beep(659, 100);
            Console.Beep(659, 400);
        }
        public void AnotherRound() //suggesting another round    
        {
            Console.WriteLine(); //break;
            Console.WriteLine("Press ENTER to continue with the next round,");
            Console.WriteLine(); //break;
            Console.WriteLine("(press any other key to exit the game)");
            ConsoleKey keyPressed = Console.ReadKey().Key;

            if (keyPressed == ConsoleKey.Enter)
            {
                UnderlineList.Clear(); // empty the lists
                WrongGuess.Clear();
                LettersToGuess.Clear();
                Console.WriteLine(); //break
                Console.WriteLine("Great!");
                Console.WriteLine(); //break
                ChoosingCategory();   //repeating everything for the next round
                Console.WriteLine();
                GetRandomWord();
                PrintUnderlines(WordToGuess);
                Console.WriteLine();
                Console.WriteLine();
                AddLettersToTheGuessingList(WordToGuess);
                GuessingPart(WordToGuess);
                Console.WriteLine();
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine(); //break
                Console.WriteLine(); //break
                Console.WriteLine("SEE YOU NEXT TIME!!");
                Environment.Exit(0);
            }
        }
    }
}
