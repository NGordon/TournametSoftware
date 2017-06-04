using System;

namespace TournamentSoftwareLoader.Model
{
    public sealed class Team
    {
        public string Name { private set; get; }

        public Team(string value, bool needTranslit = false)
        {
            Name = value;

            //Убрать страну
            var i = Name.IndexOf("(", StringComparison.OrdinalIgnoreCase);

            if (i >= 0)
                Name = Name.Substring(0, i) + Name.Substring(Name.IndexOf(")", StringComparison.OrdinalIgnoreCase) + 1);

            //Убрать номер посева
            i = Name.IndexOf("[", StringComparison.OrdinalIgnoreCase);

            if (i >= 0)
                Name = Name.Substring(0, i) + Name.Substring(Name.IndexOf("]", StringComparison.OrdinalIgnoreCase) + 1);

            if (!needTranslit)
                return;

            Name = Translit.From(Name);
        }

        public void ReplaceNameFamily()
        {
            var i = Name.Trim().LastIndexOf(" ", StringComparison.Ordinal);

            if (i == -1)
                return;

            Name = Name.Substring(i + 1).Trim() + " " + Name.Substring(0, i).Trim();
        }
    }
}
