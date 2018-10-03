namespace Dialog
{
    public class Prompt
    {
        public bool IsSaidByPlayer { get; set; }
        public string Body { get; set; }
        public string NextPromptID { get; set; }
        public string ResponseSetID { get; set; }
    }
}
