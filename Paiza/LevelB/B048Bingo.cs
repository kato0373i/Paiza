using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paiza
{
    public class B048Bingo
    {

        public static void BingoMain(string[] args)
        {
            var input = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();

            int N = input[0];

            int callCount = input[1];

            int[,] bingoCard = new int[N, N];

            Dictionary<int, string> keyValuePairs = new Dictionary<int, string>();

            List<string> addNumbers = new List<string>();

            // ビンゴカードの値格納
            for (int i = 0; i < N; i++)
            {
                input = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();

                for (int j = 0; j < N; j++)
                {
                    bingoCard[i, j] = input[j];

                    keyValuePairs.Add(input[j], $"{i},{j}");
                }
            }

            bool[,] calledNumbers = new bool[N, N];

            // 呼ばれた値
            for (int i = 0; i < callCount - 1; i++)
            {
                int num = int.Parse(Console.ReadLine());

                keyValuePairs.TryGetValue(num, out var Value);

                var axis = Value.Split(',').Select(int.Parse).ToArray();

                calledNumbers[axis[0], axis[1]] = true;
            }

            int cntBingo = 0;

            for (int i = 0; i < N; i++)
            {
                if (IsBingoVertical(i, N, calledNumbers, ref addNumbers))
                {
                    cntBingo += 1;
                }

                if (IsBingoHorizontal(i, N, calledNumbers, ref addNumbers))
                {
                    cntBingo += 1;
                }
            }

            if (IsBingoDiagonal1(N, calledNumbers, ref addNumbers))
            {
                cntBingo += 1;
            }

            if (IsBingoDiagonal2(N, calledNumbers, ref addNumbers))
            {
                cntBingo += 1;
            }

            var addBingo = addNumbers.GroupBy(x => x).ToDictionary(group => group.Key, group => group.Count()).Values.Max();

            Console.WriteLine($"{cntBingo + addBingo}");

        }

        private static bool IsBingoVertical(int column, int N, bool[,] calledNumbers, ref List<string> addNumbers)
        {
            bool isBingo = true;

            string addNumber = string.Empty;

            for (int j = 0; j < N; j++)
            {
                if (!calledNumbers[column, j])
                {
                    if (!isBingo)
                    {
                        // ２つ目の場合、ダメ
                        return false;
                    }

                    isBingo = false;

                    addNumber = $"{column},{j}";
                }
            }

            if (!string.IsNullOrEmpty(addNumber)) addNumbers.Add(addNumber);

            return isBingo;
        }

        private static bool IsBingoHorizontal(int row, int N, bool[,] calledNumbers, ref List<string> addNumbers)
        {
            bool isBingo = true;

            string addNumber = string.Empty;

            for (int j = 0; j < N; j++)
            {
                if (!calledNumbers[j, row])
                {
                    if (!isBingo)
                    {
                        // ２つ目の場合、ダメ
                        return false;
                    }

                    isBingo = false;

                    addNumber = $"{j},{row}";
                }
            }

            if (!string.IsNullOrEmpty(addNumber)) addNumbers.Add(addNumber);

            return isBingo;
        }

        private static bool IsBingoDiagonal1(int N, bool[,] calledNumbers, ref List<string> addNumbers)
        {
            bool isBingo = true;

            string addNumber = string.Empty;

            for (int i = 0; i < N; i++)
            {
                // 斜め 00, 11, 22
                //      50, 41, 32
                if (!calledNumbers[i, i])
                {
                    if (!isBingo)
                    {
                        // ２つ目の場合、ダメ
                        return false;
                    }

                    isBingo = false;

                    addNumber = $"{i},{i}";
                }
            }

            if (!string.IsNullOrEmpty(addNumber)) addNumbers.Add(addNumber);

            return isBingo;
        }

        private static bool IsBingoDiagonal2(int N, bool[,] calledNumbers, ref List<string> addNumbers)
        {
            bool isBingo = true;

            string addNumber = string.Empty;

            for (int i = 0; i < N; i++)
            {
                //      50, 41, 32
                if (!calledNumbers[N - 1 - i, i])
                {
                    if (!isBingo)
                    {
                        // ２つ目の場合、ダメ
                        return false;
                    }

                    isBingo = false;

                    addNumber = $"{N - 1 - i},{i}";
                }
            }

            if (!string.IsNullOrEmpty(addNumber)) addNumbers.Add(addNumber);

            return isBingo;
        }
    }
}
