using AutoMapper;
using session_service.Entities;

namespace session_service.Mappings
{
    public class Maps : Profile
    {
        public Maps()
        {
            this.CreateMap<Session, SessionModeratorDto>().ReverseMap();
            this.CreateMap<Session, SessionParticipantDto>().ReverseMap();
            this.CreateMap<Session, SessionObserverDto>().ReverseMap();

        }
    }
}