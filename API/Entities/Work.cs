using Newtonsoft.Json;

namespace nsAPI.Entities
{
    /// <summary>
    /// Работа.
    /// </summary>
    class Work
    {
        [JsonProperty("WorkHeader")]
        public WorkHeader WorkHeader { get; set; }

        [JsonProperty("WorkBody")]
        public ParentWorkBody WorkBody { get; set; }
    }

    /// <summary>
    /// Родительский класс, для тела работы.
    /// </summary>
    public class ParentWorkBody
    {
        [JsonProperty("ID")]
        public string Id { get; set; }
    }

    /// <summary>
    /// Тело тестовой работы.
    /// </summary>
    public partial class WorkBodyTest: ParentWorkBody
    {
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
    }

    /// <summary>
    /// Тело текстовой работы.
    /// </summary>
    public partial class WorkBodyText : ParentWorkBody
    {
        [JsonProperty("id_Test")]
        public string IdTest { get; set; }

        [JsonProperty("TaskText")]
        public string TaskText { get; set; }

        [JsonProperty("TaskTitle")]
        public string TaskTitle { get; set; }
    }

    /// <summary>
    /// Заголовок работы.
    /// </summary>
    public partial class WorkHeader
    {
        [JsonProperty("ID")]
        public string Id { get; set; }

        [JsonProperty("id_Journal")]
        public string IdJournal { get; set; }

        [JsonProperty("DateTimeCreate")]
        public string DateTimeCreate { get; set; }

        [JsonProperty("id_TypeWork")]
        public string IdTypeWork { get; set; }

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
    }

}
