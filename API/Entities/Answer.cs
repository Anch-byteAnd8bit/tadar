using Newtonsoft.Json;
using System.Collections.Generic;

namespace nsAPI.Entities
{
    public class Answers
    {
        public Answers()
        {
            TestAnswers = new List<TestAnswer>();
            TextAnswers = new List<TextAnswer>();
        }

        public List<TestAnswer> TestAnswers { get; set; }
        public List<TextAnswer> TextAnswers { get; set; }
    }

    /// <summary>
    /// Ответ на тестовую работу.
    /// </summary>
    public class TestAnswer
    {
        public TestAnswer()
        {
            AnswerHeader = new AnswerHeader();
            AnswerBody = new List<TestAnswerBody>();
        }

        [JsonProperty("AnswerHeader")]
        public AnswerHeader AnswerHeader { get; set; }

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

        public void DecryptHeaderByAES()
        {
            AnswerHeader.DecryptByAES();
        }
        
    }

    /// <summary>
    /// Ответы на текстовую работу.
    /// </summary>
    public class TextAnswer
    {
        [JsonProperty("AnswerHeader")]
        public AnswerHeader AnswerHeader { get; set; }

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

        public void DecryptHeaderByAES()
        {
            AnswerHeader.DecryptByAES();
        }
    }

    /// <summary>
    /// Класс ответа ТЕСТОВОЙ работы для добавления.
    /// </summary>
    public class TestAnswerForAdd
    {
        [JsonProperty("AnswerHeader")]
        public AnswerHeader AnswerHeader { get; set; }

        [JsonProperty("AnswerBody")]
        public List<TestAnswerBody> AnswerBody { get; set; }

        public void EncryptByAES()
        {
            AnswerHeader.EncryptByAES();
            AnswerBody.ForEach(e => e.EncryptByAES());
        }
    }

    /// <summary>
    /// Класс ответа ПИСЬМЕНОЙ работы для добавления.
    /// </summary>
    public class TextAnswerForAdd
    {
        [JsonProperty("AnswerHeader")]
        public AnswerHeader AnswerHeader { get; set; }

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

        [JsonProperty("id_RecordInJ")]
        public string id_RecordInJ { get; set; }

        [JsonProperty("id_Student")]
        public string id_Student { get; set; }

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

        /// <summary>
        /// Расшифровка заголовка.
        /// </summary>
        public void DecryptByAES()
        {
            ID = Encryption.AESHelper.DecryptString(ID);
            id_RecordInJ = Encryption.AESHelper.DecryptString(id_RecordInJ);
            id_Student = Encryption.AESHelper.DecryptString(id_Student);
            Mark = Encryption.AESHelper.DecryptString(Mark);
            DateTimeS = Encryption.AESHelper.DecryptString(DateTimeS);
            DateTimeE = Encryption.AESHelper.DecryptString(DateTimeE);
            id_TypeWork = Encryption.AESHelper.DecryptString(id_TypeWork);
        }

        /// <summary>
        /// Шифрование заголовка.
        /// </summary>
        public void EncryptByAES()
        {
            ID = Encryption.AESHelper.EncryptString(ID);
            id_RecordInJ = Encryption.AESHelper.EncryptString(id_RecordInJ);
            id_Student = Encryption.AESHelper.EncryptString(id_Student);
            Mark = Encryption.AESHelper.EncryptString(Mark);
            DateTimeS = Encryption.AESHelper.EncryptString(DateTimeS);
            DateTimeE = Encryption.AESHelper.EncryptString(DateTimeE);
            id_TypeWork = Encryption.AESHelper.EncryptString(id_TypeWork);
        }
    }

}
