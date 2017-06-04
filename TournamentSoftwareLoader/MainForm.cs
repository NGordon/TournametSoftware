using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CsQuery;
using TournamentSoftwareLoader.Model;
using System.Net;

namespace TournamentSoftwareLoader
{
    public partial class MainForm : Form
    {
        #region Locals
        private const string _ErrorCaption = "Ошибка ввода данных";

        private const string _Url = "http://www.tournamentsoftware.com/sport/matches.aspx";

        private const int _DrawColumn = 2;
        private const int _FirstTeamColumn = 3;
        private const int _SecondTeamColumn = 5;
        private const int _ScoreColumn = 6;
        #endregion

        #region Contructor
        public MainForm()
        {
            InitializeComponent();
        }
        #endregion

        #region m_startButton_Click
        private void m_startButton_Click(object sender, EventArgs e)
        {
            Enabled = false;
            Cursor = Cursors.WaitCursor;

            if (string.IsNullOrEmpty(m_guidEdit.Text))
            {
                MessageBox.Show(this,
                    @"Не указан GUID турнира.",
                    _ErrorCaption,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            if (!Guid.TryParse(m_guidEdit.Text, out var guid))
            {
                MessageBox.Show(this,
                    @"Неверный формат GUID турнира.",
                    _ErrorCaption,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

#if !DEBUG
            try
            {
#endif
                var url = $"{_Url}?id={m_guidEdit.Text}";
                string pageText;
                
                //Загрузить документ
                var request = WebRequest.Create(url);

                using (var responce = request.GetResponse())
                {
                    using(var stream = responce.GetResponseStream())
                    {
                        if (stream == null)
                            throw new Exception($"Не удалось загрузить веб-страницу по адресу \"{url}\"");

                        var list = new List<int>();
                        var b = stream.ReadByte();

                        while (b >= 0)
                        {
                            list.Add(b);
                            b = stream.ReadByte();
                        }

                        pageText = Encoding.UTF8.GetString(list.Select(bt => (byte)bt).ToArray());
                    }
                }

                var document = new CQ(pageText);

                //Перейти на body id = bdBase
                var body = document["#bdBase"];

                if(!body.Any())
                    throw new Exception("Элемент id = \"bdBase\" в документе не найден");

                body = body.First();

                //Перейти на форму id = formBasePage
                var form = body["#formBasePage"];

                if(!form.Any())
                    throw new Exception("Форма id = \"formBasePage\" в документе не найдена");

                form = form.First();

                //Перейти на контейнер формы id = container
                var container = form["#container"];

                if(!container.Any())
                    throw new Exception("Элемент id = \"container\" в документе не найден");

                container = container.First();

                //Перейти на контент формы id = content
                var content = container["#content"];

                if(!content.Any())
                    throw new Exception("Элемент id = \"content\" в документе не найден");

                content = content.First();

                //Проверить валидность GUID
                var error = content["div.error"];

                if (error.Any())
                    throw new Exception(error.Text());

                //Найти таблицу с результатами
                var table = content["table.ruler.matches"];

                if(!table.Any())
                    throw new Exception("Таблица с результатами не найдена");

                table = table.First();

                //Прочитать таблицу
                var rows = table.Children("tbody").Children("tr");

                if(!rows.Any())
                    throw new Exception("Строки таблицы с результатами не найдены");

                //Печень файлов
                var files = new Dictionary<string, string>();

                foreach(var row in rows)
                {
                    var cols = row.ChildNodes.Where(c => c.NodeName.ToLower() == "td").ToList();

                    if(!cols.Any())
                        continue;

                    //Событие
                    var cq = CQ.Create(cols[_DrawColumn]);

                    var a = cq["a"];

                    var draw = a.First().Text();

                    var i = draw.IndexOf("-", StringComparison.Ordinal);

                    if(i >= 0)
                        draw = draw.Substring(0, i-1).Trim();

                    if(string.IsNullOrEmpty(draw))
                        continue;

                    //Команда 1
                    var team1 = string.Empty;

                    cq = CQ.Create(cols[_FirstTeamColumn]);

                    a = cq["a"];

                    foreach(var ahref in a)
                    {
                        var t = new Team(ahref.FirstChild.ToString(), m_translitCheck.Checked);

                        if (m_prepeareParticipant.Checked)
                            t.ReplaceNameFamily();

                        team1 += t.Name.Trim() + ";";
                    }

                    //Команда 2
                    var team2 = string.Empty;

                    cq = CQ.Create(cols[_SecondTeamColumn]);

                    a = cq["a"];

                    foreach(var ahref in a)
                    {
                        var t = new Team(ahref.FirstChild.ToString(), m_translitCheck.Checked);

                        if (m_prepeareParticipant.Checked)
                            t.ReplaceNameFamily();

                        team2 += t.Name.Trim() + ";";
                    }

                    //Счет
                    cq = CQ.Create(cols[_ScoreColumn]);

                    var scoreValue = string.Join(" ",
                        cq["span"].ToList()
                            .Where(dobj => !string.IsNullOrEmpty(dobj.InnerText))
                            .Select(dobj => dobj.InnerText));

                    var score = new Score(scoreValue);

                    //В файл
                    if (!files.ContainsKey(draw))
                    {
                        files.Add(draw, team1 + team2 + score.BySets);
                    }
                    else
                        files[draw] += "\r\n" + team1 + team2 + score.BySets;
                }

                //Сохранить файлы
                var dir = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                var enc = Encoding.GetEncoding(1251);

                foreach(var kv in files)
                {
                    File.WriteAllText(Path.Combine(dir, kv.Key+".csv"), kv.Value, enc);
                }
#if !DEBUG
            }
            catch(Exception ex)
            {
                MessageBox.Show(this,
                    "Произошла ошибка при чтении данных:\r\n"+ex.Message,
                    @"Ошибка при чтении данных",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
#endif
#if !DEBUG
            finally
            {
#endif
                Cursor = Cursors.Default;
                Enabled = true;
#if !DEBUG
            }
#endif
        }
        #endregion
    }
}
