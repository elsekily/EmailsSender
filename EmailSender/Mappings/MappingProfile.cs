using AutoMapper;
using EmailSender.Entities.Models;
using EmailSender.Entities.Resources;
using MimeKit;
using MimeKit.Text;

namespace RoommateFinderAPI.Mappings;
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Message, MessageResource>();

        CreateMap<MessageSaveResource, Message>()
            .ForMember(t => t.Id, opt => opt.Ignore());

        CreateMap<MessageRecipientsResource, MimeMessage>()
            .ForMember(mm => mm.To, opt => opt
                .MapFrom(mmr => mmr.RecipientEmailAddresses.Select(e => new MailboxAddress("", e))));

        CreateMap<Message, MimeMessage>()
            .ForMember(mm => mm.Subject, opt => opt.MapFrom(m => m.Subject))
            .ForMember(mm => mm.Body, opt => opt.MapFrom(m => new TextPart(TextFormat.Plain) { Text = m.Body }));
    }
}