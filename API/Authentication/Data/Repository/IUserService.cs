using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;

namespace Authentication.Data.Repository
{
    public interface IUserService<T>
    {
        Task RegisterUser(T user, string password);
        Task<bool> ValidateCredentials(T user, string password);

        Task<T> FindByEmail(string email);
        Task<T> FindById(Guid id);

        Task SignIn(T user);
        Task SignOut(T user);
        Task<string> GetToken(T user, string url);

        bool VerifyIdentificationNumber(string identificationNumber);

    }
}