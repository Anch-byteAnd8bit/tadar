using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace nsAPI.Entities
{
    public class Answers
    {
        public Answers()
        {
            TestAnswers = new List<TestAnswer>();
            TextAnswers = new List<TextAnswer>();
            Headers = new List<AnswerHeader>();
        }

        public List<TestAnswer> testAnswers;
        public List<TestAnswer> TestAnswers
        {
            get => testAnswers;
            set
            {
                testAnswers = value;
                OnChangedAnswers();
            }
        }
        public List<TextAnswer> textAnswers;
        public List<TextAnswer> TextAnswers
        {
            get => textAnswers;
            set
            {
                textAnswers = value;
                OnChangedAnswers();
            }
        }

        /// <summary>
        /// Специальное свойство для вывода в XAML.
        /// </summary>
        public List<AnswerHeader> Headers { get; set; }

        private void OnChangedAnswers()
        {
            if (textAnswers != null || testAnswers != null)
                Headers = new List<AnswerHeader>();
            if (textAnswers != null) textAnswers.ForEach(w => Headers.Add(w.AnswerHeader));
            if (testAnswers != null) testAnswers.ForEach(w => Headers.Add(w.AnswerHeader));
        }

        public void AddTest(TestAnswer test)
        {
            TestAnswers.Add(test);
            OnChangedAnswers();
        }

        public void AddText(TextAnswer text)
        {
            TextAnswers.Add(text);
            OnChangedAnswers();
        }
        public void Update()
        {
            OnChangedAnswers();
        }

        /// <summary>
        /// Ответы, на которые есть оценки.
        /// </summary>
        public Answers GetMarkedAnswers() => GetMarkedOrNonMarkedAnswers(true);
        /// <summary>
        /// Ответы, на которые нет оценки.
        /// </summary>
        public Answers GetNonMarkedAnswers() => GetMarkedOrNonMarkedAnswers(false);

        private Answers GetMarkedOrNonMarkedAnswers(bool isMarked = true)
        {
            Answers filteredAnswers = new Answers();
            // Ищем тестовые работы.
            for (int i = 0; i < TestAnswers.Count; i++)
            {
                Answer answer = TestAnswers[i];

                if (isMarked)
                {
                    if ((answer.AnswerHeader.Mark != null) && (answer.AnswerHeader.Mark != "NULL"))
                        filteredAnswers.AddTest(TestAnswers[i]);
                }
                else if ((answer.AnswerHeader.Mark == null) || (answer.AnswerHeader.Mark == "NULL"))
                    filteredAnswers.AddTest(TestAnswers[i]);

            }

            // Ищем письменные работы.
            for (int i = 0; i < TextAnswers.Count; i++)
            {

                Answer answer = TextAnswers[i];

                if (isMarked)
                {
                    if ((answer.AnswerHeader.Mark != null) && (answer.AnswerHeader.Mark != "NULL"))
                        filteredAnswers.AddText(TextAnswers[i]);
                }
                else if ((answer.AnswerHeader.Mark == null) || (answer.AnswerHeader.Mark == "NULL"))
                    filteredAnswers.AddText(TextAnswers[i]);

            }

            return filteredAnswers;
        }
        /// <summary>
        /// Возвращает список только тех, ответов, которые решал пользователь с указанным ID.
        /// </summary>
        /// <param name="id_User">ID пользователя.</param>
        /// <returns></returns>
        public Answers GetAnswersByIDUser(string id_User)
        {
            Answers answers = new Answers();

            answers.TestAnswers = new List<TestAnswer>();
            TestAnswers.ForEach(ta =>
            {
                if (ta.AnswerHeader.id_User == id_User)
                    answers.AddTest(ta);
            });

            answers.TextAnswers = new List<TextAnswer>();
            TextAnswers.ForEach(ta =>
            {
                if (ta.AnswerHeader.id_User == id_User)
                    answers.AddText(ta);
            });

            return answers;
        }
    }

    public abstract class Answer
    {
        [JsonProperty("AnswerHeader")]
        public AnswerHeader AnswerHeader { get; set; }

        public Answer()
        {
            AnswerHeader = new AnswerHeader();
        }

        public void DecryptHeaderByAES()
        {
            AnswerHeader.DecryptByAES();
        }

    }

    /// <summary>
    /// Ответ на тестовую работу.
    /// </summary>
    public class TestAnswer : Answer
    {
        public TestAnswer():base()
        {
            AnswerBody = new List<TestAnswerBody>();
        }


        [JsonProperty("AnswerBody")]
        public List<TestAnswerBody> AnswerBody { get; set; }

        /// <summary>
        /// Расшифровка тела.
        /// </summary>
        public void DecryptBodyByAES()
        {
            if (AnswerBody != null)
            {
                AnswerBody.ForEach(e => e.DecryptByAES());
            }
        }
        
    }

    /// <summary>
    /// Ответы на текстовую работу.
    /// </summary>
    public class TextAnswer : Answer
    {

        [JsonProperty("AnswerBody")]
        public List<TextAnswerBody> AnswerBody { get; set; }

        /// <summary>
        /// Расшифровка тела.
        /// </summary>
        public void DecryptBodyByAES()
        {
            if (AnswerBody != null)
            {
                AnswerBody.ForEach(e => e.DecryptByAES());
            }
        }
    }

    /// <summary>
    /// Класс ответа ТЕСТОВОЙ работы для добавления.
    /// </summary>
    public class TestAnswerForAdd : Answer
    {

        [JsonProperty("AnswerBody")]
        public List<TestAnswerBody> AnswerBody { get; set; }

        public void EncryptByAES()
        {
            AnswerHeader.EncryptByAES();
            AnswerBody.ForEach(e => e.EncryptByAES());
        }

        public TestAnswerForAdd Clone()
        {
            var d = new TestAnswerForAdd();
            d.AnswerHeader = AnswerHeader.Clone();
            d.AnswerBody = new List<TestAnswerBody>();
            AnswerBody.ForEach(ab => d.AnswerBody.Add(ab.Clone()));
            return d;
        }
    }

    /// <summary>
    /// Класс ответа ПИСЬМЕНОЙ работы для добавления.
    /// </summary>
    public class TextAnswerForAdd : Answer
    {
        [JsonProperty("AnswerBody")]
        public List<TextAnswerBody> AnswerBody { get; set; }

        public void EncryptByAES()
        {
            AnswerHeader.EncryptByAES();
            AnswerBody.ForEach(e => e.EncryptByAES());
        }
    }

    /// <summary>
    /// Тело ответа тестовой работы.
    /// </summary>
    public partial class TestAnswerBody
    {
        [JsonProperty("ID")]
        public string ID { get; set; }

        [JsonProperty("id_Task")]
        public string id_Task { get; set; }

        /// <summary>
        /// Заполняется автоматически
        /// </summary>
        [JsonProperty("id_ExecutionOfWork")]
        public string id_ExecutionOfWork { get; set; }

        [JsonProperty("num_Answ")]
        public string num_Answ { get; set; }

        public TestAnswerBody Clone()
        {
            var ab = new TestAnswerBody();
            ab.num_Answ = num_Answ;
            ab.id_Task = id_Task;
            ab.ID = ID;
            ab.id_ExecutionOfWork = id_ExecutionOfWork;
            return ab;
        }

        /// <summary>
        /// Расшифровка тела.
        /// </summary>
        public void DecryptByAES()
        {
            ID = Encryption.AESHelper.DecryptString(ID);
            id_Task = Encryption.AESHelper.DecryptString(id_Task);
            id_ExecutionOfWork = Encryption.AESHelper.DecryptString(id_ExecutionOfWork);
            num_Answ = Encryption.AESHelper.DecryptString(num_Answ);
        }


        /// <summary>
        /// Шифрование тела.
        /// </summary>
        public void EncryptByAES()
        {
            ID = Encryption.AESHelper.EncryptString(ID);
            id_Task = Encryption.AESHelper.EncryptString(id_Task);
            id_ExecutionOfWork = Encryption.AESHelper.EncryptString(id_ExecutionOfWork);
            num_Answ = Encryption.AESHelper.EncryptString(num_Answ);
        }
    }

    /// <summary>
    /// Тело ответа текстовой работы.
    /// </summary>
    public partial class TextAnswerBody
    {
        [JsonProperty("ID")]
        public string ID { get; set; }

        [JsonProperty("id_Task")]
        public string id_Task { get; set; }

        [JsonProperty("AnswText")]
        public string AnswText { get; set; }

        /// <summary>
        /// Это поле заполняется автоматически!
        /// </summary>
        [JsonProperty("id_ExecutionOfWork")]
        public string id_ExecutionOfWork { get; set; }


        /// <summary>
        /// Расшифровка тела.
        /// </summary>
        public void DecryptByAES()
        {
            ID = Encryption.AESHelper.DecryptString(ID);
            id_Task = Encryption.AESHelper.DecryptString(id_Task);
            AnswText = Encryption.AESHelper.DecryptString(AnswText);
            id_ExecutionOfWork = Encryption.AESHelper.DecryptString(id_ExecutionOfWork);
        }

        /// <summary>
        /// Шифрование тела.
        /// </summary>
        public void EncryptByAES()
        {
            ID = Encryption.AESHelper.EncryptString(ID);
            id_Task = Encryption.AESHelper.EncryptString(id_Task);
            AnswText = Encryption.AESHelper.EncryptString(AnswText);
            id_ExecutionOfWork = Encryption.AESHelper.EncryptString(id_ExecutionOfWork);
        }
    }

    /// <summary>
    /// Заголовок работы.
    /// </summary>
    public partial class AnswerHeader
    {
        [JsonProperty("ID")]
        public string ID { get; set; }

        [JsonProperty("id_Work")]
        public string id_Work { get; set; }

        [JsonProperty("id_UserInClasses")]
        public string id_UserInClasses { get; set; }

        [JsonProperty("id_User")]
        public string id_User { get; set; }

        [JsonProperty("Mark")]
        public string Mark { get; set; }

        [JsonProperty("DateTimeS")]
        public string DateTimeS { get; set; }

        [JsonProperty("DateTimeE")]
        public string DateTimeE { get; set; }

        /// <summary>
        /// Из таблицы worktypes. Можно получить значения методом
        /// var wtypes = await api.GetDataFromRefbook("worktypes");
        /// </summary>
        [JsonProperty("id_TypeWork")]
        public string id_TypeWork { get; set; }

        public AnswerHeader Clone()
        {
            var a = new AnswerHeader();
            a.DateTimeE = DateTimeE;
            a.ID = ID;
            a.Mark = Mark;
            a.DateTimeS = DateTimeS;
            a.id_UserInClasses = id_UserInClasses;
            a.id_Work = id_Work;
            a.id_TypeWork = id_TypeWork;
            a.id_User = id_User;
            return a;
        }

        /// <summary>
        /// Расшифровка заголовка.
        /// </summary>
        public void DecryptByAES()
        {
            ID = Encryption.AESHelper.DecryptString(ID);
            id_Work = Encryption.AESHelper.DecryptString(id_Work);
            id_UserInClasses = Encryption.AESHelper.DecryptString(id_UserInClasses);
            Mark = Encryption.AESHelper.DecryptString(Mark);
            DateTimeS = Encryption.AESHelper.DecryptString(DateTimeS);
            DateTimeE = Encryption.AESHelper.DecryptString(DateTimeE);
            id_TypeWork = Encryption.AESHelper.DecryptString(id_TypeWork);
            id_User = Encryption.AESHelper.DecryptString(id_User);
        }

        /// <summary>
        /// Шифрование заголовка.
        /// </summary>
        public void EncryptByAES()
        {
            ID = Encryption.AESHelper.EncryptString(ID);
            id_Work = Encryption.AESHelper.EncryptString(id_Work);
            id_UserInClasses = Encryption.AESHelper.EncryptString(id_UserInClasses);
            Mark = Encryption.AESHelper.EncryptString(Mark);
            DateTimeS = Encryption.AESHelper.EncryptString(DateTimeS);
            DateTimeE = Encryption.AESHelper.EncryptString(DateTimeE);
            id_TypeWork = Encryption.AESHelper.EncryptString(id_TypeWork);
            id_User = Encryption.AESHelper.EncryptString(id_User);
        }
    }

}
