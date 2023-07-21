using System;
using System.Linq;

namespace Paiza.LevelB
{
    public class B134SeedingArea
    {
        public static void SeedingAreaMain(string[] args)
        {
            string[] range = Console.ReadLine().Split(' ');

            // 縦
            int N = int.Parse(range[0]);
            // 横
            int M = int.Parse(range[1]);

            // 種まき回数
            int T = int.Parse(Console.ReadLine());

            // 畑の配列
            int[,] area = new int[N, M];

            // 種まきループ
            for (int i = 0; i < T; i++)
            {
                // 種まき情報の読込
                string[] seeding = Console.ReadLine().Split(' ');

                // 高さ
                int H = int.Parse(seeding[0]);

                // 座標
                int axisY = int.Parse(seeding[1]);
                int axisX = int.Parse(seeding[2]);

                // 種がまかれたエリアの算出
                CalculateSeedingArea(H, axisX, axisY, ref area);
            }

            // 結果出力            
            for (int n = 0; n < N; n++)
            {
                // 一行毎に抽出
                int[] extractedColumn = Enumerable.Range(0, M).Select(row => area[n, row]).ToArray();

                Console.WriteLine(string.Join(" ", extractedColumn));
            }
        }

        private static void CalculateSeedingArea(int H, int axisX, int axisY, ref int[,] area)
        {
            // 種まき半径
            int r = (H + 1) / 2;

            for (int y = 0; y < H; y++)
            {
                for (int x = 0; x < H; x++)
                {
                    // 外周判定
                    var isOuterY = (y == 0 && y == H - 1);
                    var isOuterX = (x == 0 && x == H - 1);

                    if (!isOuterY || !isOuterX)
                    {
                        continue;
                    }

                    // 種が蒔かれる座標
                    int areaX = axisX - r + x;
                    int areaY = axisY - r + y;

                    // 畑の範囲外も有り得るため、回避
                    var IsOutofRange = (areaX < 0 || areaY < 0 || areaX > area.GetLength(0) - 1 || areaY > area.GetLength(1) - 1);

                    if(IsOutofRange)
                    {
                        continue;
                    }                    

                    // 種まき加算
                    area[areaX, areaY] += 1;
                }
            }
        }
    }
}
