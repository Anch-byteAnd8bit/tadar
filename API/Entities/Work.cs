using Newtonsoft.Json;
using System.Collections.Generic;

namespace nsAPI.Entities
{
    public class Works
    {
        public Works()
        {
            TestWorks = new List<TestWork>();
            TextWorks = new List<TextWork>();
        }

        public List<TestWork> TestWorks { get; set; }
        public List<TextWork> TextWorks { get; set; }
    }

    /// <summary>
    /// Тестовая работа.
    /// </summary>
    public class TestWork
    {
        public TestWork()
        {
            WorkHeader = new WorkHeader();
            WorkBody = new List<TestTask>();
        }

        [JsonProperty("WorkHeader")]
        public WorkHeader WorkHeader { get; set; }

        [JsonProperty("WorkBody")]
        public List<TestTask> WorkBody { get; set; }

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
    /// Текстовая работа.
    /// </summary>
    public class TextWork
    {
        [JsonProperty("WorkHeader")]
        public WorkHeader WorkHeader { get; set; }

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
