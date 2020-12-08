using System;

namespace blablabla
{
    class GameController
    {
        readonly Player Player = new Player();
        readonly GameQuestionReader Question = new GameQuestionReader();

        public void Menu_Switch()
        {

            Question.QuestionMaker();
            Question.Question_Sorter();

            while (true)
            {
                Console.WriteLine("Choose 1 or 2");
                Console.WriteLine("1. Play a new game\n2. Quit game");
                int menuInput = 0;
                try
                {
                    menuInput = Convert.ToInt32(Console.ReadLine());
                }
                catch (FormatException)
                {

                }

                switch (menuInput)
                {
                    case 1:
                        Start_New_Game();
                        break;
                    case 2:
                        Exit_Game();
                        break;
                    default:
                        Console.WriteLine("You can only choose 1 or 2");
                        Menu_Switch();
                        break;
                }
            }
        }

        public void Start_New_Game()
        {
            Console.Clear();
            for (int i = 1; i < 6; i++)
            {
                Questionnaire();
                if (i == 5)
                {
                    if (Player.Points > 0)
                    {
                        LastQuestion();
                    }
                    else
                    {
                        Console.WriteLine("Sorry! You've lost...\n\n\n");

                    }
                }
            }

        }
        public void Exit_Game()
        {
            Console.WriteLine("Thank you for playing!");
            Console.WriteLine("Press any key to close application");
            Environment.Exit(0);
        }

        public void Questionnaire()
        {
            int choice = 0;
            int index;
            while (true)
            {

                Console.WriteLine("Choose category by pressing 1 - 5 ");

                Question.List_Categories();
                while (true)
                {
                    try
                    {
                        choice = int.Parse(Console.ReadLine());
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Please use correct format");
                    }
                    if (choice < 1 || choice > 5)
                    {
                        Console.WriteLine("Please pick only from the list");
                    }
                    else
                        break;
                }
                string category = Question.Category(choice);
                index = Question.QuestionFinder(category);
                if (index != -1)
                    break;
            }
            string played_value = Question.Answer_Checker(index);
            Player.WinQuestion(played_value);

            Player.ShowStatistics();
        }

        private void LastQuestion()
        {
            Console.WriteLine("************************************************************");

            Console.WriteLine("************************************************************");
            Console.WriteLine("Last Question, Choose points: ");
            Console.WriteLine("************************************************************");
            Console.WriteLine("************************************************************");

            int bet = 0;

            while (true)
            {
                try
                {
                    bet = Convert.ToInt32(Console.ReadLine());
                }
                catch (FormatException)
                {
                }
                if (bet > Player.Points || bet < 1)
                {
                    Console.WriteLine("You can´t bet more than what you have, please bet again");
                    Console.WriteLine("Place your bet");
                }
                else
                    break;
            }

            Player.AddBet(bet);
            int choice = 0;
            int index;
            while (true)
            {
                Console.WriteLine("--------------------------------------------------");
                Console.WriteLine("Choose category by entering a number between 1 - 5");
                Console.WriteLine("--------------------------------------------------");
                Question.List_Categories();
                while (true)
                {
                    try
                    {
                        choice = int.Parse(Console.ReadLine());
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Please use correct format");
                    }
                    if (choice < 1 || choice > 5)
                    {
                        Console.WriteLine("Please pick only from the list");
                    }
                    else
                        break;
                }
                string category = Question.Category(choice);
                index = Question.QuestionFinder(category);
                if (index != -1)
                    break;
            }
            Question.Answer_Checker(index);
            Player.WinLastQuestion(bet);
            Player.ShowEndResult();
        }
    }


    public class Player
    {
        public static int QuestionsCompleted { get; set; }
        public int Points { get; set; }

        public void AddBet(int bet)
        {
            Points -= bet;
        }
        public void ClearQuestionsCompleted()
        {
            QuestionsCompleted = 0;
        }

        public void WinQuestion(string value)
        {
            Points += Int32.Parse(value);
            QuestionsCompleted++;
        }
        public void WinLastQuestion(int bet)
        {
            Points += bet * 2;
        }
        public void ShowEndResult()
        {
            Console.WriteLine("Thanks for playing, yor final score is: {0}\n\n\n", Points);
            ClearQuestionsCompleted();
        }
        public void ShowStatistics()
        {
            Console.WriteLine("Your points: {0}", Points);
            Console.WriteLine("Questions answered {0} of 6", QuestionsCompleted);
        }
    }
}