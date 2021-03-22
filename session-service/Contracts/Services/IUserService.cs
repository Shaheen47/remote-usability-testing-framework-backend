namespace session_service.Contracts.Services
{
    public interface IUserService
    {
        public void createModerator(ModeratorDto moderatorDto);

        public bool loginModerator(ModeratorLoginDto moderatorLoginDto);


    }
}