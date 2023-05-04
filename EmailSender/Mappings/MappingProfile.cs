using AutoMapper;
using EmailSender.Entities.Models;
using EmailSender.Entities.Resources;

namespace RoommateFinderAPI.Mappings;
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Message, MessageResource>();



        CreateMap<MessageSaveResource, Message>()
            .ForMember(t => t.Id, opt => opt.Ignore());
    }
}