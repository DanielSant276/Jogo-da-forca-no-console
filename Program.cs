using System;
using System.Collections.Generic;

namespace Jogo_da_Forca
{
    class Program
    {
        static void Main(string[] args)
        {
            makeQuestion();
        }

        //Faz a pergunta inicial
        static void makeQuestion()
        {
            try
            {
                Console.WriteLine("ESCREVER(e) uma palavra ou RECEBER(r) uma palavra aleatória?");
                char question = Convert.ToChar(Console.ReadLine());
                readQuestion(question);
            }
            catch
            {
                Console.WriteLine("Comando Errado. Digite apenas uma letra para a resposta!");
                makeQuestion();
            }
        }

        //Recebe a resposta da pergunta e gera a palavra
        static void readQuestion(char question)
        {
            string wordTip = null;
            switch (question)
            {
                case 'e':
                    Console.Write("Digite a palavra do jogo: ");
                    string wordToGuess = Console.ReadLine();
                    defineList(wordToGuess, wordTip);
                    break;
                case 'r':
                    randomWord(wordTip);
                    break;
                default:
                    Console.WriteLine("Não foi digitado o comando certo. Digite apenas e para escolher a palavra ou r para receber uma palavra aleatória");
                    makeQuestion();
                    break;
            }

        }

        //Escolhe uma palavra da lista
        static void randomWord(string wordTip)
        {
            Random number = new Random();
            int numberChosed = number.Next(1, 11);

            string wordToGuess;
            string[,] listItems = {{"bola", "objeto"}, {"lapis", "objeto"}, {"samambaia", "planta"}, {"natal", "data comemorativa"}, {"feriado", "descanso"}, {"estudante", "substantivo"}, {"ricardo", "nome"}, {"janeiro", "mês"}, {"elevador", "substantivo"}, {"avestruz", "animal"}};
            
            wordToGuess = listItems[numberChosed, 0];
            wordTip = listItems[numberChosed, 1];
            defineList(wordToGuess, wordTip);
        }

        //Escreve os espaços para ser relacionada com a palavra e executa
        static void defineList(string wordToGuess, string wordTip)
        {
            int error = 0;
            List<string> lettersList = new List<string>();

            for (int i = 0; i < wordToGuess.Length; i++)
            {
                lettersList.Add("___ ");
            }

            List<string> lettersListTest = lettersList;

            List<string> lettersTried = new List<string>();
            lettersTried.Add("");

            execute(error, wordToGuess, lettersList, lettersListTest, wordTip, lettersTried);
        }

        //Executa o principal de todo o código. Basicamente puxa os outros methods
        static void execute(int error, string wordToGuess, List<string> lettersList, List<string> lettersListTest, string wordTip, List<string> lettersTried)
        {
            gallow(error);

            lettersGuessed(error, lettersList, wordTip, lettersTried);

            letterVerification(error, wordToGuess, lettersList, lettersListTest, wordTip, lettersTried);
        }

        //Escreve na tela o número de erros, as palavras utilizadas e os espaços das palavras
        static void lettersGuessed(int error, List<string> lettersList, string wordTip, List<string> lettersTried)
        {
            Console.Write("Erro = " + error);
            Console.Write("        " + "Letras tentadas: ");
            letterUsed(lettersTried);
            Console.WriteLine("Dica: " + wordTip);
            foreach (string item in lettersList)
            {
                Console.Write(item.ToString());
            }
            Console.WriteLine();
        }

        //Cria a lista de letras utilizadas
        static void letterUsed(List<string> lettersTried)
        {
            lettersTried.Sort();
            foreach (string item in lettersTried)
            {
                if (item.ToString() != "")
                {
                    Console.Write(item.ToString());
                    Console.Write(", ");
                }
                else
                {
                    Console.Write(item.ToString());
                }
            }
            Console.WriteLine();
        }

        //Teste de letra acionada
        static void letterVerification(int error, string wordToGuess, List<string> lettersList, List<string> lettersListTest, string wordTip, List<string> lettersTried)
        {
            try
            {
                Console.Write("Escolha uma letra: ");
                char tryToGuess = Convert.ToChar(Console.ReadLine());
                if (!lettersTried.Contains(Convert.ToString(tryToGuess)))
                {
                    lettersTried.Add(Convert.ToString(tryToGuess));
                }
                teste(wordToGuess, tryToGuess, error, lettersList, lettersListTest, wordTip, lettersTried);
            }
            catch
            {
                Console.WriteLine("Por favor, insira apenas uma letra!");
                Console.Write("Aperte Enter para continuar");
                Console.ReadLine();
                execute(error, wordToGuess, lettersList, lettersListTest, wordTip, lettersTried);
            }
        }

        //Confere se existe ou não a letra na forca
        static void teste(string wordToGuess, char tryToGuess, int error, List<string> lettersList, List<string> lettersListTest, string wordTip, List<string> lettersTried)
        {
            bool diference = false;

            for (int i = 0; i < wordToGuess.Length; i++)
            {
                if (tryToGuess == wordToGuess[i])
                {
                    diference = true;
                    lettersList[i] = " " + Convert.ToString(tryToGuess) + "  ";
                }
            }

            if (diference == true)
            {
                lettersListTest = lettersList;
                diference = false;
            }
            else
            {
                error++;
                diference = false;
            }

            endGame(error, lettersList, wordToGuess, wordTip, lettersTried);
            execute(error, wordToGuess, lettersList, lettersListTest, wordTip, lettersTried);
        }

        //Gera a forca
        static void gallow(int error)
        {
            int onlyGallowLines = 12;
            Console.WriteLine("  _____");
            switch (error)
            {
                case 0:
                    for (int i = 0; i != onlyGallowLines; i++)
                    {
                        pole();
                    }
                    break;
                case 1:
                    head();
                    for (int i = 0; i != onlyGallowLines - 3; i++)
                    {
                        pole();
                    }
                    break;
                case 2:
                    head();
                    oneArm();
                    for (int i = 0; i != onlyGallowLines - 6; i++)
                    {
                        pole();
                    }
                    break;
                case 3:
                    head();
                    twoArms();
                    for (int i = 0; i != onlyGallowLines - 6; i++)
                    {
                        pole();
                    }
                    break;
                case 4:
                    head();
                    twoArms();
                    oneLeg();
                    for (int i = 0; i != onlyGallowLines - 10; i++)
                    {
                        pole();
                    }
                    break;
                case 5:
                    head();
                    twoArms();
                    twoLegs();
                    for (int i = 0; i != onlyGallowLines - 10; i++)
                    {
                        pole();
                    }
                    break;
                default:
                    Console.WriteLine("Erro");
                    break;
            }
            Console.WriteLine(@"/_\");

            static void pole()
            {
                Console.WriteLine(" |");
            }

            static void head()
            {
                Console.WriteLine(" |   _|_ ");
                Console.WriteLine(" |  |   |");
                Console.WriteLine(" |  |___| ");
            }

            static void oneArm()
            {
                Console.WriteLine(" |    |  ");
                Console.WriteLine(@" |   /| ");
                Console.WriteLine(@" |  / | ");
            }

            static void twoArms()
            {
                Console.WriteLine(" |    |  ");
                Console.WriteLine(@" |   /|\ ");
                Console.WriteLine(@" |  / | \ ");
            }

            static void oneLeg()
            {
                Console.WriteLine(" |    |");
                Console.WriteLine(" |    |");
                Console.WriteLine(@" |   / ");
                Console.WriteLine(@" |  / ");
            }
            static void twoLegs()
            {
                Console.WriteLine(" |    |");
                Console.WriteLine(" |    |");
                Console.WriteLine(@" |   / \ ");
                Console.WriteLine(@" |  /   \ ");
            }
        }

        //Verifica vitória ou derrota
        static void endGame(int error, List<string> lettersList, string wordToGuess, string wordTip, List<string> lettersTried)
        {
            bool win = false;

            foreach (string item in lettersList)
            {
                if (item.ToString() == "___ ")
                {
                    win = false;
                    break;
                }
                win = true;
            }

            if (error == 5)
            {
                gallow(error);
                lettersGuessed(error, lettersList, wordTip, lettersTried);
                Console.WriteLine("Você perdeu, a palavra é " + wordToGuess + "!");
                Console.ReadLine();
                Environment.Exit(0);
            }

            if (win == true)
            {
                gallow(error);
                lettersGuessed(error, lettersList, wordTip, lettersTried);
                Console.WriteLine("Você ganhou");
                Console.ReadLine();
                Environment.Exit(0);
            }
        }
    }
}