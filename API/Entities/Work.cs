using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace nsAPI.Entities
{
    /// <summary>
    /// Битовый фильтр.
    /// </summary>
    [Flags]
    public enum FilterWorks
    {
        None = 0,
        //Answers
        /// <summary>
        /// Есть ответ
        /// </summary>
        HaveAnsw = 1,
        /// <summary>
        /// Нет ответов
        /// </summary>
        HaveNotAnsw = 2,
        /// <summary>
        /// Дата начала уже прошла
        /// </summary>
        Started = 4,
        /// <summary>
        /// Жата начала еще не прошла
        /// </summary>
        NotStarted = 8,
        /// <summary>
        /// Есть оценка
        /// </summary>
        Marked = 16,
        /// <summary>
        /// Без оценки
        /// </summary>
        NotMarked = 32,

    }

    public class Works
    {
        public Works()
        {
            TestWorks = new List<TestWork>();
            TextWorks = new List<TextWork>();
            Headers = new ObservableCollection<WorkHeader>();
        }

        private List<TestWork> testWorks;
        public List<TestWork> TestWorks
        {
            get => testWorks;
            set
            {
                testWorks = value;
                OnChangedWorks();
            }
        }

        private List<TextWork> textWorks;
        public List<TextWork> TextWorks
        {
            get => textWorks;
            set
            {
                textWorks = value;
                OnChangedWorks();
            }
        }

        /// <summary>
        /// Специальное свойство для вывода в XAML.
        /// </summary>
        public ObservableCollection<WorkHeader> Headers { get; set; }

        private void OnChangedWorks()
        {
            if (textWorks != null || testWorks != null)
                Headers = new ObservableCollection<WorkHeader>();
            if (textWorks != null) textWorks.ForEach(w => Headers.Add(w.WorkHeader));
            if (testWorks != null) testWorks.ForEach(w => Headers.Add(w.WorkHeader));
        }

        public void AddTest(TestWork test)
        {
            TestWorks.Add(test);
            OnChangedWorks();
        }

        public void AddText(TextWork test)
        {
            TextWorks.Add(test);
            OnChangedWorks();
        }

        public void Update()
        {
            OnChangedWorks();
        }

        /// <summary>
        /// Не решенные работы, определяются по заданным ответам на эти работы.
        /// </summary>
        /// <returns></returns>
        public Works GetUnresolvedWorks(Answers answers) =>
            GetFilteredWorks(FilterWorks.HaveNotAnsw, answers);

        /// <summary>
        /// Фильтрует работы.
        /// Важно: Для некоторых фильтров обязательно надо задать ответы на эти работы;
        /// Возвращаемые работы связаны с исходными!
        /// </summary>
        /// <param name="answers">Ответы на эти рабты.</param>
        /// <param name="filter">Битовый фильтр.</param>
        /// <returns></returns>
        public Works GetFilteredWorks(FilterWorks filter, Answers answers)
        {
            Works filteredworks = new Works();
            // Ищем тестовые работы.
            for (int i = 0; i < TestWorks.Count; i++)
            {
                bool isgood = true;

                // Ответы.
                if (answers != null)
                {
                    TestAnswer answer = null;
                    // Нужно ли находить ответ?
                    if (filter.HasFlag(FilterWorks.HaveAnsw) || filter.HasFlag(FilterWorks.Marked) ||
                        filter.HasFlag(FilterWorks.HaveNotAnsw) || filter.HasFlag(FilterWorks.NotMarked))
                    {
                        // Перебираем список ответов и, когда находим ответ, запоминаем его.
                        for (int j = 0; j < answers.TestAnswers.Count; j++)
                        {
                            if (TestWorks[i].WorkHeader.ID == answers.TestAnswers[j].AnswerHeader.id_Work)
                            {
                                answer = answers.TestAnswers[j];
                                break;
                            }
                        }
                    }

                    // Флаг наличия ответа.
                    isgood = isgood && 
                        ((filter.HasFlag(FilterWorks.HaveAnsw) && (answer != null)) ||
                        (filter.HasFlag(FilterWorks.HaveNotAnsw) && (answer == null)) ||
                        (!filter.HasFlag(FilterWorks.HaveAnsw) && !filter.HasFlag(FilterWorks.HaveNotAnsw)));
                    // Наличие оценки.
                    isgood = isgood && (answer != null) &&
                        ((filter.HasFlag(FilterWorks.Marked) && ((answer.AnswerHeader.Mark != null) ||
                        (answer.AnswerHeader.Mark != "NULL"))) ||
                        ((filter.HasFlag(FilterWorks.NotMarked) && ((answer.AnswerHeader.Mark == null) ||
                        (answer.AnswerHeader.Mark == "NULL")))) ||
                        (!filter.HasFlag(FilterWorks.Marked) && !filter.HasFlag(FilterWorks.NotMarked)));
                }

                //var d1 = DateTime.Parse(TestWorks[i].WorkHeader.DateTimeStart);
                //var d2 = DateTime.Today;
                //var g = d1 <= d2;

                // Работа уже стартовала.
                isgood = isgood && (
                    ((filter.HasFlag(FilterWorks.Started) &&
                    (DateTime.Parse(TestWorks[i].WorkHeader.DateTimeStart) <= DateTime.Today)) ||
                    (filter.HasFlag(FilterWorks.NotStarted) &&
                    (DateTime.Parse(TestWorks[i].WorkHeader.DateTimeStart) > DateTime.Today))) ||
                    (!filter.HasFlag(FilterWorks.Started) && !filter.HasFlag(FilterWorks.NotStarted)));
                // Проверяем, прошла ли эта работа через фильтры.
                if (isgood) filteredworks.AddTest(TestWorks[i]);
            }

            // Ищем письменные работы.
            for (int i = 0; i < TextWorks.Count; i++)
            {
                bool isgood = true;

                // Ответы.
                if (answers != null)
                {
                    TextAnswer answer = null;
                    // Нужно ли находить ответ?
                    if (filter.HasFlag(FilterWorks.HaveAnsw) || filter.HasFlag(FilterWorks.Marked) ||
                        filter.HasFlag(FilterWorks.HaveNotAnsw) || filter.HasFlag(FilterWorks.NotMarked))
                    {
                        // Перебираем список ответов и, когда находим ответ, запоминаем его.
                        for (int j = 0; j < answers.TextAnswers.Count; j++)
                        {
                            if (TextWorks[i].WorkHeader.ID == answers.TextAnswers[j].AnswerHeader.id_Work)
                            {
                                answer = answers.TextAnswers[j];
                                break;
                            }
                        }
                    }

                    // Флаг наличия ответа.
                    isgood = isgood &&
                        ((filter.HasFlag(FilterWorks.HaveAnsw) && (answer != null)) ||
                        (filter.HasFlag(FilterWorks.HaveNotAnsw) && (answer == null)) ||
                        (!filter.HasFlag(FilterWorks.HaveAnsw) && !filter.HasFlag(FilterWorks.HaveNotAnsw)));
                    // Наличие оценки.
                    isgood = isgood && (answer != null) &&
                        (((filter.HasFlag(FilterWorks.Marked) && ((answer.AnswerHeader.Mark != null) ||
                        (answer.AnswerHeader.Mark != "NULL"))) ||
                        (filter.HasFlag(FilterWorks.NotMarked) && ((answer.AnswerHeader.Mark == null) ||
                        (answer.AnswerHeader.Mark == "NULL")))) ||
                        (!filter.HasFlag(FilterWorks.Marked) && !filter.HasFlag(FilterWorks.NotMarked)));
                }

                // Работа уже стартовала.
                isgood = isgood && (
                    ((filter.HasFlag(FilterWorks.Started) &&
                    (DateTime.Parse(TextWorks[i].WorkHeader.DateTimeStart) <= DateTime.Today)) ||
                    (filter.HasFlag(FilterWorks.NotStarted) &&
                    (DateTime.Parse(TextWorks[i].WorkHeader.DateTimeStart) > DateTime.Today))) ||
                    (!filter.HasFlag(FilterWorks.Started) && !filter.HasFlag(FilterWorks.NotStarted)));
                // Проверяем, прошла ли эта работа через фильтры.
                if (isgood) filteredworks.AddText(TextWorks[i]);
            }

            return filteredworks;
        }



    }

    /// <summary>
    /// Базовый класс работы.
    /// </summary>
    public abstract class Work
    {
        [JsonProperty("WorkHeader")]
        public WorkHeader WorkHeader { get; set; }
     
        public Work(WorkHeader workHeader)
        {
            WorkHeader = workHeader;
        }

        public Work()
        {
            WorkHeader = new WorkHeader();
        }

        public void EncryptHeaderByAES()
        {
            WorkHeader.EncryptByAES();
        }
    }

    /// <summary>
    /// Базовый класс работа.
    /// </summary>
    public class TestWork : Work
    {
        public TestWork() : base()
        {
            WorkBody = new List<TestTask>();
        }

        [JsonProperty("WorkBody")]
        public List<TestTask> WorkBody { get; set; }

        public TestWork Clone()
        {
            var tw = new TestWork();
            tw.WorkHeader = WorkHeader.Clone();
            tw.WorkBody = new List<TestTask>();
            WorkBody.ForEach(wb => tw.WorkBody.Add(wb.Clone()));
            return tw;
        }

        /// <summary>
        /// Расшифровка тела.
        /// </summary>
        public void DecryptBodyByAES()
        {
            if (WorkBody != null)
            {
                WorkBody.ForEach(e => e.DecryptByAES());
            }
        }

        public void DecryptHeaderByAES()
        {
            WorkHeader.DecryptByAES();
        }

        public void Decrypt()
        {
            DecryptBodyByAES();
            DecryptHeaderByAES();

        }


        /// <summary>
        /// Шифрование тела.
        /// </summary>
        public void EncryptBodyByAES()
        {
            if (WorkBody != null)
            {
                WorkBody.ForEach(e => e.EncryptByAES());
            }
        }

        public void Encrypt()
        {
            EncryptBodyByAES();
            EncryptHeaderByAES();

        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, Converter.Settings);
        }
    }

    /// <summary>
    /// Текстовая работа.
    /// </summary>
    public class TextWork : Work
    {
        [JsonProperty("WorkBody")]
        public List<TextTask> WorkBody { get; set; }

        /// <summary>
        /// Расшифровка тела.
        /// </summary>
        public void DecryptBodyByAES()
        {
            if (WorkBody != null)
            {
                WorkBody.ForEach(e => e.DecryptByAES());
            }
        }

        public void DecryptHeaderByAES()
        {
            WorkHeader.DecryptByAES();
        }

        public void Decrypt()
        {
            DecryptBodyByAES();
            DecryptHeaderByAES();
        }


        /// <summary>
        /// Шифрованеи тела.
        /// </summary>
        public void EncryptBodyByAES()
        {
            if (WorkBody != null)
            {
                WorkBody.ForEach(e => e.EncryptByAES());
            }
        }

        public void EncryptHeaderByAES()
        {
            WorkHeader.EncryptByAES();
        }

        public void Encrypt()
        {
            EncryptBodyByAES();
            EncryptHeaderByAES();
        }


        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, Converter.Settings);
        }
    }

    /// <summary>
    /// Тело тестовой работы.
    /// </summary>
    public partial class TestTask
    {
        public TestTask()
        {
            selAnsws = new List<bool>() { false, false, false, false};
        }
        [JsonProperty("ID")]
        public string ID { get; set; }

        [JsonProperty("NumTask")]
        public string NumTask { get; set; }

        [JsonProperty("Word")]
        public string Word { get; set; }

        [JsonProperty("PossibleAnsw1")]
        public string PossibleAnsw1 { get; set; }

        [JsonProperty("PossibleAnsw2")]
        public string PossibleAnsw2 { get; set; }

        [JsonProperty("PossibleAnsw3")]
        public string PossibleAnsw3 { get; set; }

        [JsonProperty("PossibleAnsw4")]
        public string PossibleAnsw4 { get; set; }

        [JsonProperty("right_num")]
        public string RightNum { get; set; }

        [JsonProperty("id_Test")]
        public string IdTest { get; set; }

        [JsonIgnore]
        public List<bool> selAnsws { get; set; }

        public TestTask Clone()
        {
            var tt = new TestTask
            {
                ID = ID,
                NumTask = NumTask,
                Word = Word,
                PossibleAnsw1 = PossibleAnsw1,
                PossibleAnsw2 = PossibleAnsw2,
                PossibleAnsw3 = PossibleAnsw3,
                PossibleAnsw4 = PossibleAnsw4,
                RightNum = RightNum,
                IdTest = IdTest,
                selAnsws = selAnsws,
            };
            return tt;
        }

        /// <summary>
        /// Расшифровка тела.
        /// </summary>
        public void DecryptByAES()
        {
            ID = Encryption.AESHelper.DecryptString(ID);
            IdTest = Encryption.AESHelper.DecryptString(IdTest);
            NumTask = Encryption.AESHelper.DecryptString(NumTask);
            Word = Encryption.AESHelper.DecryptString(Word);
            PossibleAnsw1 = Encryption.AESHelper.DecryptString(PossibleAnsw1);
            PossibleAnsw2 = Encryption.AESHelper.DecryptString(PossibleAnsw2);
            PossibleAnsw3 = Encryption.AESHelper.DecryptString(PossibleAnsw3);
            PossibleAnsw4 = Encryption.AESHelper.DecryptString(PossibleAnsw4);
            RightNum = Encryption.AESHelper.DecryptString(RightNum);
        }


        /// <summary>
        /// Шифрование тела.
        /// </summary>
        public void EncryptByAES()
        {
            ID = Encryption.AESHelper.EncryptString(ID);
            IdTest = Encryption.AESHelper.EncryptString(IdTest);
            NumTask = Encryption.AESHelper.EncryptString(NumTask);
            Word = Encryption.AESHelper.EncryptString(Word);
            PossibleAnsw1 = Encryption.AESHelper.EncryptString(PossibleAnsw1);
            PossibleAnsw2 = Encryption.AESHelper.EncryptString(PossibleAnsw2);
            PossibleAnsw3 = Encryption.AESHelper.EncryptString(PossibleAnsw3);
            PossibleAnsw4 = Encryption.AESHelper.EncryptString(PossibleAnsw4);
            RightNum = Encryption.AESHelper.EncryptString(RightNum);
        }
    }

    /// <summary>
    /// Тело текстовой работы.
    /// </summary>
    public partial class TextTask
    {
        [JsonProperty("ID")]
        public string ID { get; set; }

        [JsonProperty("id_Test")]
        public string IdTest { get; set; }

        [JsonProperty("TaskText")]
        public string TaskText { get; set; }

        [JsonProperty("TaskTitle")]
        public string TaskTitle { get; set; }


        /// <summary>
        /// Расшифровка тела.
        /// </summary>
        public void DecryptByAES()
        {
            ID = Encryption.AESHelper.DecryptString(ID);
            IdTest = Encryption.AESHelper.DecryptString(IdTest);
            TaskText = Encryption.AESHelper.DecryptString(TaskText);
            TaskTitle = Encryption.AESHelper.DecryptString(TaskTitle);
        }

        /// <summary>
        /// Шифрование тела.
        /// </summary>
        public void EncryptByAES()
        {
            ID = Encryption.AESHelper.EncryptString(ID);
            IdTest = Encryption.AESHelper.EncryptString(IdTest);
            TaskText = Encryption.AESHelper.EncryptString(TaskText);
            TaskTitle = Encryption.AESHelper.EncryptString(TaskTitle);
        }
    }

    /// <summary>
    /// Заголовок работы.
    /// </summary>
    public partial class WorkHeader
    {
        [JsonProperty("ID")]
        public string ID { get; set; }

        [JsonProperty("id_Class")]
        public string id_Class { get; set; }

        [JsonProperty("DateTimeCreate")]
        public string DateTimeCreate { get; set; }

        [JsonProperty("id_TypeWork")]
        public string id_TypeWork { get; set; }

        /// <summary>
        /// 1 - НЕ на оценку, 0 - На оценку.
        /// </summary>
        [JsonProperty("isNonMark")]
        public string IsNonMark { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Description")]
        public string Description { get; set; }

        [JsonProperty("DateTimeStart")]
        public string DateTimeStart { get; set; }

        [JsonProperty("MaxDuration")]
        public string MaxDuration { get; set; }


        public WorkHeader Clone()
        {
            var wh = new WorkHeader()
            {
                DateTimeStart = DateTimeStart,
                MaxDuration = MaxDuration,
                Name = Name,
                Description = Description,
                IsNonMark = IsNonMark,
                id_Class = id_Class,
                ID = ID,
                DateTimeCreate = DateTimeCreate,
                id_TypeWork = id_TypeWork,
            };
            return wh;
        }

        /// <summary>
        /// Расшифровка заголовка.
        /// </summary>
        public void DecryptByAES()
        {
            ID = Encryption.AESHelper.DecryptString(ID);
            id_Class = Encryption.AESHelper.DecryptString(id_Class);
            DateTimeCreate  = Encryption.AESHelper.DecryptString(DateTimeCreate);
            id_TypeWork  = Encryption.AESHelper.DecryptString(id_TypeWork);
            IsNonMark  = Encryption.AESHelper.DecryptString(IsNonMark);
            Name  = Encryption.AESHelper.DecryptString(Name);
            Description  = Encryption.AESHelper.DecryptString(Description);
            DateTimeStart  = Encryption.AESHelper.DecryptString(DateTimeStart);
            MaxDuration = Encryption.AESHelper.DecryptString(MaxDuration);
        }

        /// <summary>
        /// Шифрование заголовка.
        /// </summary>
        public void EncryptByAES()
        {
            ID = Encryption.AESHelper.EncryptString(ID);
            id_Class = Encryption.AESHelper.EncryptString(id_Class);
            DateTimeCreate = Encryption.AESHelper.EncryptString(DateTimeCreate);
            id_TypeWork = Encryption.AESHelper.EncryptString(id_TypeWork);
            IsNonMark = Encryption.AESHelper.EncryptString(IsNonMark);
            Name = Encryption.AESHelper.EncryptString(Name);
            Description = Encryption.AESHelper.EncryptString(Description);
            DateTimeStart = Encryption.AESHelper.EncryptString(DateTimeStart);
            MaxDuration = Encryption.AESHelper.EncryptString(MaxDuration);
        }
    }

}
