﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace nsAPI.Entities
{
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

        public void AddText(TextWork text)
        {
            TextWorks.Add(text);
            OnChangedWorks();
        }

        public void Update()
        {
            OnChangedWorks();
        }

        /// <summary>
        /// Нерешенные работы, определяются по заданным ответам на эти работы.
        /// </summary>
        /// <returns>Работы, не имеющие ответа в заданном списке ответов.</returns>
        public Works GetUnresolvedWorks(Answers answers) =>
            GetUnresolvedOrResolvedWorks(answers, true);

        /// <summary>
        /// Решенные работы, определяются по заданным ответам на эти работы.
        /// </summary>
        /// <returns>Работы, уже имеющие ответы в заданном списке ответов.</returns>
        public Works GetResolvedWorks(Answers answers) =>
            GetUnresolvedOrResolvedWorks(answers, false);

        private Works GetUnresolvedOrResolvedWorks(Answers answers, bool isUnresolved = true)
        {
            // Ответы.
            if (answers == null)
            {
                throw new Exception("Не задан параметр!");
            }

            Works filteredworks = new Works();
            // Ищем тестовые работы.
            for (int i = 0; i < TestWorks.Count; i++)
            {
                TestAnswer answer = null;

                // Перебираем список ответов и, когда находим ответ, запоминаем его.
                for (int j = 0; j < answers.TestAnswers.Count; j++)
                {
                    if (TestWorks[i].WorkHeader.ID == answers.TestAnswers[j].AnswerHeader.id_Work)
                    {
                        answer = answers.TestAnswers[j];
                        break;
                    }
                }

                // Проверяем.
                if (isUnresolved)
                {
                    if (answer == null) filteredworks.AddTest(TestWorks[i]);
                }
                else if (answer != null) filteredworks.AddTest(TestWorks[i]);
            }

            // Ищем письменные работы.
            for (int i = 0; i < TextWorks.Count; i++)
            {
                TextAnswer answer = null;
                
                // Перебираем список ответов и, когда находим ответ, запоминаем его.
                for (int j = 0; j < answers.TextAnswers.Count; j++)
                {
                    if (TextWorks[i].WorkHeader.ID == answers.TextAnswers[j].AnswerHeader.id_Work)
                    {
                        answer = answers.TextAnswers[j];
                        break;
                    }
                }
                // Проверяем.
                if (isUnresolved)
                {
                    if (answer == null) filteredworks.AddText(TextWorks[i]);
                }
                else if (answer != null) filteredworks.AddText(TextWorks[i]);
            }

            return filteredworks;
        }

        /// <summary>
        /// Возвращает работы только те, которые уже начались.
        /// Возвращаемые работы связаны с исходными!
        /// </summary>
        /// <returns></returns>
        public Works GetStartedWorks() =>
            GetStartedOrNotStartedWorks(true);

        /// <summary>
        /// Возвращает работы только те, которые еще не начались.
        /// Возвращаемые работы связаны с исходными!
        /// </summary>
        /// <returns></returns>
        public Works GetNotStartedWorks() =>
            GetStartedOrNotStartedWorks(true);

        private Works GetStartedOrNotStartedWorks(bool isStarted = true)
        {
            Works filteredworks = new Works();
            // Ищем тестовые работы.
            for (int i = 0; i < TestWorks.Count; i++)
            {
                var dts = DateTime.Parse(TestWorks[i].WorkHeader.DateTimeStart);
                var dtn = DateTime.Now;

                // Проверяем.
                if (isStarted)
                {
                    if (dtn >= dts) filteredworks.AddTest(TestWorks[i]);
                }
                else if (dtn < dts) filteredworks.AddTest(TestWorks[i]);
            }

            // Ищем письменные работы.
            for (int i = 0; i < TextWorks.Count; i++)
            {
                var dts = DateTime.Parse(TextWorks[i].WorkHeader.DateTimeStart);
                var dtn = DateTime.Now;

                // Проверяем.
                if (isStarted)
                {
                    if (dtn >= dts) filteredworks.AddText(TextWorks[i]);
                }
                else if (dtn < dts) filteredworks.AddText(TextWorks[i]);
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
            ID = Encryption.AESHelper.DecryptStringB64(ID);
            IdTest = Encryption.AESHelper.DecryptStringB64(IdTest);
            NumTask = Encryption.AESHelper.DecryptStringB64(NumTask);
            Word = Encryption.AESHelper.DecryptStringB64(Word);
            PossibleAnsw1 = Encryption.AESHelper.DecryptStringB64(PossibleAnsw1);
            PossibleAnsw2 = Encryption.AESHelper.DecryptStringB64(PossibleAnsw2);
            PossibleAnsw3 = Encryption.AESHelper.DecryptStringB64(PossibleAnsw3);
            PossibleAnsw4 = Encryption.AESHelper.DecryptStringB64(PossibleAnsw4);
            RightNum = Encryption.AESHelper.DecryptStringB64(RightNum);
        }


        /// <summary>
        /// Шифрование тела.
        /// </summary>
        public void EncryptByAES()
        {
            ID = Encryption.AESHelper.EncryptStringB64(ID);
            IdTest = Encryption.AESHelper.EncryptStringB64(IdTest);
            NumTask = Encryption.AESHelper.EncryptStringB64(NumTask);
            Word = Encryption.AESHelper.EncryptStringB64(Word);
            PossibleAnsw1 = Encryption.AESHelper.EncryptStringB64(PossibleAnsw1);
            PossibleAnsw2 = Encryption.AESHelper.EncryptStringB64(PossibleAnsw2);
            PossibleAnsw3 = Encryption.AESHelper.EncryptStringB64(PossibleAnsw3);
            PossibleAnsw4 = Encryption.AESHelper.EncryptStringB64(PossibleAnsw4);
            RightNum = Encryption.AESHelper.EncryptStringB64(RightNum);
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
            ID = Encryption.AESHelper.DecryptStringB64(ID);
            IdTest = Encryption.AESHelper.DecryptStringB64(IdTest);
            TaskText = Encryption.AESHelper.DecryptStringB64(TaskText);
            TaskTitle = Encryption.AESHelper.DecryptStringB64(TaskTitle);
        }

        /// <summary>
        /// Шифрование тела.
        /// </summary>
        public void EncryptByAES()
        {
            ID = Encryption.AESHelper.EncryptStringB64(ID);
            IdTest = Encryption.AESHelper.EncryptStringB64(IdTest);
            TaskText = Encryption.AESHelper.EncryptStringB64(TaskText);
            TaskTitle = Encryption.AESHelper.EncryptStringB64(TaskTitle);
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
            ID = Encryption.AESHelper.DecryptStringB64(ID);
            id_Class = Encryption.AESHelper.DecryptStringB64(id_Class);
            DateTimeCreate  = Encryption.AESHelper.DecryptStringB64(DateTimeCreate);
            id_TypeWork  = Encryption.AESHelper.DecryptStringB64(id_TypeWork);
            IsNonMark  = Encryption.AESHelper.DecryptStringB64(IsNonMark);
            Name  = Encryption.AESHelper.DecryptStringB64(Name);
            Description  = Encryption.AESHelper.DecryptStringB64(Description);
            DateTimeStart  = Encryption.AESHelper.DecryptStringB64(DateTimeStart);
            MaxDuration = Encryption.AESHelper.DecryptStringB64(MaxDuration);
        }

        /// <summary>
        /// Шифрование заголовка.
        /// </summary>
        public void EncryptByAES()
        {
            ID = Encryption.AESHelper.EncryptStringB64(ID);
            id_Class = Encryption.AESHelper.EncryptStringB64(id_Class);
            DateTimeCreate = Encryption.AESHelper.EncryptStringB64(DateTimeCreate);
            id_TypeWork = Encryption.AESHelper.EncryptStringB64(id_TypeWork);
            IsNonMark = Encryption.AESHelper.EncryptStringB64(IsNonMark);
            Name = Encryption.AESHelper.EncryptStringB64(Name);
            Description = Encryption.AESHelper.EncryptStringB64(Description);
            DateTimeStart = Encryption.AESHelper.EncryptStringB64(DateTimeStart);
            MaxDuration = Encryption.AESHelper.EncryptStringB64(MaxDuration);
        }
    }

}
