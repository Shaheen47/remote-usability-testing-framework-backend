namespace screensharing_service.Dtos
{
    public class DomEventCreationDto
    {
        public DomEventCreationDto(string content)
        {
            this.content = content;
        }

        public string content { get; set; }
    }
}