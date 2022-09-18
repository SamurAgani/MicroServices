using IdentityServer4.Validation;
using System.Linq;
using System.Threading.Tasks;

namespace FreeCourse.IdentityServer.Services
{
    public class TokenExchangeExtensionGrantValidator : IExtensionGrantValidator
    {
        public string GrantType => "urn:ietf:params:oauth:grant-type:token-exchange";

        private ITokenValidator tokenValidator;

        public TokenExchangeExtensionGrantValidator(ITokenValidator tokenValidator)
        {
            this.tokenValidator = tokenValidator;
        }

        public async Task ValidateAsync(ExtensionGrantValidationContext context)
        {
            var request = context.Request.Raw.ToString();
            var token = context.Request.Raw.Get("subject_token");
            if (string.IsNullOrEmpty(token))
            {
                context.Result = new GrantValidationResult(IdentityServer4.Models.TokenRequestErrors.InvalidRequest,"token missing");
                return;
            }
            var tokenValidateResult = await tokenValidator.ValidateAccessTokenAsync(token);

            if (tokenValidateResult.IsError)
            {
                context.Result = new GrantValidationResult(IdentityServer4.Models.TokenRequestErrors.InvalidGrant, "token missing");

                return;
            }

            var subject = tokenValidateResult.Claims.FirstOrDefault(x=>x.Type=="sub");

            if(subject == null)
            {
                context.Result = new GrantValidationResult(IdentityServer4.Models.TokenRequestErrors.InvalidGrant, "token must contain sub");
                return ;
            }

            context.Result = new GrantValidationResult(subject.Value,"access_token",tokenValidateResult.Claims);
            return ;


        }
    }
}
