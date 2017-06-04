using System.Linq;

namespace TournamentSoftwareLoader.Model
{
    public sealed class Score
    {
        public string BySets { private set; get; }

        public Score(string value)
        {
            if (string.IsNullOrEmpty(value) || value.ToLower() == "no match" || value.ToLower() == "retired")
            {
                BySets = "w;";
                return;
            }

            var result = value.Trim().Split(' ').Select(s => s.Replace("-", ";")).ToList();

            //Вставить счет по партиям
            var p1 = 0;
            var p2 = 0;

            foreach (var arr in result.Select(s => s.Split(';')))
            {
                if (int.Parse(arr[0]) > int.Parse(arr[1]))
                    p1++;
                else
                    p2++;
            }

            BySets = $"{p1};{p2}";
        }
    }
}
