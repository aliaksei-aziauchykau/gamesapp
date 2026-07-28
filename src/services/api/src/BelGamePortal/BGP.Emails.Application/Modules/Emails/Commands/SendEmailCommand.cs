

using BGP.Shared.Modules.Email;
using MediatR;

namespace BGP.Emails.Application.Modules.Emails.Commands;

public class SendEmailCommand : IRequest
{
    // public SendEmailCommand(string to, string subject, string body)
    // {
    //     To = to;
    //     Subject = subject;
    //     Body = body;
    // }

    // public string To { get; set; }
    // public string Subject { get; set; }
    // public string Body { get; set; }
}

public class SendEmailCommandHandler : IRequestHandler<SendEmailCommand>
{
    private readonly IEmailService emailService;

    public SendEmailCommandHandler(IEmailService emailService)
    {
        this.emailService = emailService;
    }

    public async Task Handle(SendEmailCommand request, CancellationToken cancellationToken)
    {
        await emailService.SendAsync();
    }
}

