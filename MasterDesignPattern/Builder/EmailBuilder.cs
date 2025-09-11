namespace MasterDesignPattern.Builder
{
    internal class EmailSender
    {
        public void SendEmail()
        {
            Console.WriteLine("---------Email Builder------------");
            // Using the EmailBuilder to create an Email object
            Email email = EmailBuilder.Build(
               to: "dddd",
               subject: "Meeting Reminder",
                builder => builder
                    .SetBody("Don't forget about our meeting tomorrow at 10 AM.")
                    .SetCc("dd")
                    .SetBcc("adad")
                    .AddAttachment("agenda.pdf"));

            Email em1=new Email("","","",null,null,null);
            // Not allowed to immuatble em1.Subject = "dadad";

            //var e =new EmailBuilder();// Not allowed due to private constructor
           // var r = EmailBuilder.Build("", "", null);// we can't change the orders


        }

        public class Email
        {
            public string To { get; }
            public string Subject { get; }
            public string Body { get; }
            public string Cc { get; }
            public string Bcc { get; }
            public List<string> Attachments { get; }

            internal Email(string to, string subject, string body, string cc, string bcc, List<string> attachments)
            {
                To = to;
                Subject = subject;
                Body = body;
                Cc = cc;
                Bcc = bcc;
                Attachments = attachments ?? new List<string>();
            }

            public override string ToString()
            {
                return $"To: {To}, Subject: {Subject}, Cc: {Cc}, Bcc: {Bcc}, Attachments: {Attachments.Count}";
            }
        }

        public class EmailBuilder
        {
            private string _to;
            private string _subject;
            private string _body;
            private string _cc;
            private string _bcc;
            private readonly List<string> _attachments = new();

            private EmailBuilder(string to, string subject)
            {
                _to = to ?? throw new ArgumentNullException(nameof(to));
                _subject = subject ?? throw new ArgumentNullException(nameof(subject));
            }


            // Optional fluent methods
            public EmailBuilder SetBody(string body)
            {
                _body = body;
                return this;
            }

            public EmailBuilder SetCc(string cc)
            {
                _cc = cc;
                return this;
            }

            public EmailBuilder SetBcc(string bcc)
            {
                _bcc = bcc;
                return this;
            }

            public EmailBuilder AddAttachment(string filePath)
            {
                _attachments.Add(filePath);
                return this;
            }

            private Email Create()
            {
                return new Email(_to, _subject, _body, _cc, _bcc, _attachments);
            }

            // Entry point with mandatory fields
            public static Email Build(string to, string subject, Func<EmailBuilder, EmailBuilder> options = null)
            {
                var builder = new EmailBuilder(to, subject);

                if (options != null)
                {
                    builder = options(builder);
                }

                return builder.Create();
            }
        }
    }
}