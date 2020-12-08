namespace MailService.Model
{
    public class EmailModel
    {
        public string ToMail { get; set; }
        public string Param { get; set; }
        public int MailID { get; set; }
        public int ServiceID { get; set; }
        public int LangID { get; set; }
        public string Content { get; set; }
        public string Title { get; set; }
        public bool IsResend { get; set; }
        public string Sign { get; set; }
    }
}