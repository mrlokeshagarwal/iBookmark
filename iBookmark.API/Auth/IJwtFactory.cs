namespace iBookmark.API.Auth
{
    using iBookmark.Model.AggregatesModel.UserAggregate;
    using System.Security.Claims;
    using System.Threading.Tasks;

    public interface IJwtFactory
    {
        Task<string> GenerateEncodedToken(string userName, ClaimsIdentity identity);
        ClaimsIdentity GenerateClaimsIdentity(UserModel user);
    }
}
