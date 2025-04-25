namespace SportAgencyDApplication.Services
{
    public interface IApplicationService
    {
        Task<bool> ApplyAsync(string adId, string athleteId);
    }

}
