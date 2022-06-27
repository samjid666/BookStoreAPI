using BookStoreAPI.Model;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace BookStoreAPI.Repository
{
    public interface IAccountRepository
    {
        Task<IdentityResult> SignUpAsync(SignUpModel signUpModel);
        Task<string> LoginAsync(SignInModel signInModel);


    }
}
