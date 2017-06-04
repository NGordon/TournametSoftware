using System.Collections.Generic;

namespace TournamentSoftwareLoader.Model
{
    public static class Translit
    {
        #region Locals
        private static readonly Dictionary<string, string> _lexem = new Dictionary<string, string>
        {
            {"a", "а"},
            {"A", "А"},
            {"b", "б"},
            {"B", "Б"},
            {"c", null},
            {"C", null},
            {"d", "д"},
            {"D", "Д"},
            {"e", "е"},
            {"E", "Е"},
            {"f", "ф"},
            {"F", "Ф"},
            {"g", "г"},
            {"G", "Г"},
            {"h", "х"},
            {"H", "Х"},
            {"i", "ий"},
            {"I", "ИЙ"},
            {"j", null},
            {"J", null},
            {"k", "к"},
            {"K", "К"},
            {"l", "л"},
            {"L", "Л"},
            {"m", "м"},
            {"M", "М"},
            {"n", "н"},
            {"N", "Н"},
            {"o", "о"},
            {"O", "О"},
            {"p", "п"},
            {"P", "П"},
            {"q", null},
            {"Q", null},
            {"r", "р"},
            {"R", "Р"},
            {"s", "с"},
            {"S", "С"},
            {"t", "т"},
            {"T", "Т"},
            {"u", "у"},
            {"U", "У"},
            {"v", "в"},
            {"V", "В"},
            {"w", null},
            {"W", null},
            {"x", "кс"},
            {"X", "Кс"},
            {"y", "йы"},
            {"Y", "ЙЫ"},
            {"z", "з"},
            {"Z", "З"},
            {"ch", "ч"},
            {"Ch", "Ч"},
            {"ck", "к"},
            {"Ck", "К"},
            {"sh", "ш"},
            {"Sh", "Ш"},
            {"kh", "х"},
            {"Kh", "Х"},
            {"ts", "ц"},
            {"Ts", "Ц"},
            {"zh", "ж"},
            {"Zh", "Ж"},
            {"ya", "я"},
            {"Ya", "Я"},
            {"yu", "ю"},
            {"Yu", "Ю"},
            {"ju", "ю"},
            {"Ju", "Ю"},
            {"'", "ь"},
            {"sch", "щ"},
            {"Sch", "Щ"}
        };

        private static readonly List<string> _vowels = new List<string> {"a", "e", "i", "o", "u", "y"};
        #endregion

        #region From
        public static string From(string source)
        {
            var result = string.Empty;
            var i = 0;

            while(i < source.Length)
            {
                //проверить триграфы
                if(i < source.Length-2)
                {
                    var str = source.Substring(i, 3);

                    if(_lexem.ContainsKey(str))
                    {
                        result += _lexem[str];
                        i += 3;
                        continue;
                    }
                }

                //проверить диграфы
                if(i < source.Length-1)
                {
                    var str = source.Substring(i, 2);

                    if(_lexem.ContainsKey(str))
                    {
                        result += _lexem[str];
                        i += 2;
                        continue;
                    }
                }

                //Единичная буква
                var leter = source.Substring(i, 1);

                string r;

                //Особые случаи для "i" и "y"
                switch(leter.ToLower())
                {
                    case "i":
                        if(i > 0 && _vowels.Contains(source.Substring(i-1, 1).ToLower()))
                            r = _lexem[leter].Substring(1, 1);
                        else
                            r = _lexem[leter].Substring(0, 1);
                        break;
                    case "y":
                        if(i > 0 && _vowels.Contains(source.Substring(i-1, 1).ToLower()))
                            r = _lexem[leter].Substring(0, 1);
                        else
                            r = _lexem[leter].Substring(1, 1);
                        break;
                    //Остальные буквы
                    default:
                        r = _lexem.ContainsKey(leter) ? _lexem[leter] : leter;
                        break;
                }

                result += r;
                i++;
            }

            return result;
        }
        #endregion
    }
}