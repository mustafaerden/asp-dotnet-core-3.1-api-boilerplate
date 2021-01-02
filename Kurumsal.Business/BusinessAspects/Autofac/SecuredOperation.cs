using Business.Constants;
using Castle.DynamicProxy;
using Core.Extensions;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Business.BusinessAspects.Autofac
{
    public class SecuredOperation : MethodInterception
    {
        private string[] _roles;
        private IHttpContextAccessor _httpContextAccessor;

        public SecuredOperation(string roles)
        {
            _roles = roles.Split(',');
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
        }

        protected override void OnBefore(IInvocation invocation)
        {
            // User in rolleri;
            var roleClaims = _httpContextAccessor.HttpContext.User.ClaimRoles();

            // Manager da istenen rollerden bitanesi kullanicinin rollerinde varsa islemi devam ettirecek;
            foreach (var role in _roles)
            {
                if (roleClaims.Contains(role))
                {
                    return;
                }
            }

            // Kullanicinin rolu uymuyorsa;
            throw new Exception(Messages.AutorizationDeniedMessage);
        }
    }
}
