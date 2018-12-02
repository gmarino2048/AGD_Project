namespace Dialog
{
    public class Prompt
    {
        public bool IsSaidByPlayer { get; set; }
        public bool IsInternalMonologue { get; set; }
        public DialogAnimState AnimState { get; set; }
        public string Body { get; set; }
        public string NextPromptID { get; set; }
        public string ResponseSetID { get; set; }
    }
}
