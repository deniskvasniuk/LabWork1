namespace LabWork1.Models
{
    public class MailFormModel
    {
        public string Name{ get; set; }
        public string Email { get; set; }
        public string Msg { get; set; }

        public MailFormModel()
        {
            Name = string.Empty;
            Email = string.Empty;
            Msg = string.Empty;
        }
    }
}
