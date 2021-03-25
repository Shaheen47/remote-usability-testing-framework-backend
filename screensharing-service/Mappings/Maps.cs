using AutoMapper;
using screensharing_service.Dtos;
using screensharing_service.Entities;

namespace screensharing_service.Mappings
{

        public class Maps : Profile
        {
            public Maps()
            {
                this.CreateMap<Session, SessionWithoutReplyDto>().ReverseMap();

            }
        }
}