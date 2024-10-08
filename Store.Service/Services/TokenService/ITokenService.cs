using Store.Data.Entities.IdenetityEntities;


namespace Store.Service.Services.TokenService
{
    public interface ITokenService
    {
        string GenerateToken(AppUser appUser);
    }
}
