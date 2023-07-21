using System;
using System.Collections.Generic;
using System.Linq;

namespace Paiza.LevelB
{
    public class B035Jog
    {
        #region 説明


        // B035:ジョギングランキング

        // あなたは健康になるために、部員数 N 人のジョギング部に入りました。
        // ジョギング部では、個々人が好きな日にジョギングをして、その距離を記録していきます。
        // また、部員のやる気向上のため、毎月ジョギングした距離の長い上位 T 人が発表されます。
        // 
        // 今月新しく上位 T 人に入ってきた人には "new"、 先月より順位が上がった人には "up"、 下がった人には "down"、 順位が変わらない人には "same"、 というラベルが付けられます。
        // 
        // 先月の走った距離は引き継がれません。 N 人分の先月の走った距離と今月のジョギングの記録が M 個与えられるので、今月の上位 T 人をラベルとともに出力してください。
        // 
        // 同じ人が複数回ジョギングした場合、順位付けにはその人が今月走った距離の合計を使います。 走った距離が同じときは、辞書順に並べたときに早い名前の方の部員を上位とします。

        // https://paiza.jp/career/challenges/148/retry

        #endregion

        public static void JogMain(string[] args)
        {
            string[] inputs = Console.ReadLine().Split(' ');

            // 部員数
            int N = int.Parse(inputs[0]);
            // 今月のジョギング記録数
            int M = int.Parse(inputs[1]);
            // 成績表の人数
            int T = int.Parse(inputs[2]);

            var records = new List<Record>();

            // 先月のランキング            
            for (int i = 0; i < N; i++)
            {
                inputs = Console.ReadLine().Split(' ');

                var record = new Record()
                {
                    Name = inputs[0],
                    Distance = int.Parse(inputs[1]),
                };

                records.Add(record);
            }

            // 先月のランキング表
            Dictionary<string, int> preRanking = new Dictionary<string, int>();

            HashSet<string> menbers = new HashSet<string>();

            foreach (var record in records.OrderBy(x => x.Name).OrderByDescending(x => x.Distance))
            {
                // 名前, 順位(1スタート)
                preRanking.Add(record.Name.ToString(), preRanking.Count + 1);

                menbers.Add(record.Name);
            }

            records.Clear();

            // 今月の記録を読込
            for (int i = 0; i < M; i++)
            {
                inputs = Console.ReadLine().Split(' ');

                var record = new Record()
                {
                    Name = inputs[1],
                    Distance = int.Parse(inputs[2]),
                };

                records.Add(record);
            }

            // 今月のランキング表
            Dictionary<int, string> currentRanking = new Dictionary<int, string>();

            var rows = records
                        .GroupBy(x => x.Name)
                        .Select(group => new
                        {
                            Name = group.Key,
                            TotalDistance = group.Sum(row => row.Distance)
                        }).OrderBy(x => x.Name).OrderByDescending(result => result.TotalDistance);

            // 今月のランキング表
            foreach (var row in rows)
            {
                currentRanking.Add(currentRanking.Count + 1, row.Name.ToString());

                menbers.Remove(row.Name);
            }

            // 記録なし部員の追加
            foreach (var name in menbers.OrderBy(x => x))
            {
                currentRanking.Add(currentRanking.Count + 1, name);
            }

            // 出力生成
            for (int t = 1; t < T + 1; t++)
            {
                currentRanking.TryGetValue(t, out string name);

                var row = rows.Where(x => x.Name.ToString() == name).FirstOrDefault();

                int distance = row == null ? 0 : row.TotalDistance;

                string label = "new";

                preRanking.TryGetValue(name, out int preRank);

                if (preRank > T)
                {
                    label = "new";
                }                
                else if (preRank > t)
                {
                    label = "up";
                }
                else if (preRank < t)
                {
                    label = "down";
                }
                else
                {
                    label = "same";
                }

                Console.WriteLine($"{name} {distance} {label}");
            }
        }

        public class Record
        {
            public string Name { get; set; }

            public int Distance { get; set; }
        }
    }
}
