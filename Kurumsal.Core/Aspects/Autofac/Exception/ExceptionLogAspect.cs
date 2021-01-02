using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Logging;
using Core.CrossCuttingConcerns.Logging.Log4Net;
using Core.Utilities.Interceptors;
using Core.Utilities.Messages;
using Kurumsal.Core.CrossCuttingConcerns.Logging;
using System;
using System.Collections.Generic;

namespace Kurumsal.Core.Aspects.Autofac.Exception
{
    // Business ta manager larda tum metotlar icinde tek tek try catch kullanmak yerine bu ExceptionLogAspect imizi Core/Utilities/Interceptors/AspectInterceptorSelector da tum metotlarda calisacak sekilde tanimladik ki, managerlardaki metotlarin uzerinde [ExceptionLogAspect()] yazmaktanda kurtulduk!
    public class ExceptionLogAspect : MethodInterception
    {
        // Loglama yapilacagi zaman bu sekilde;
        private LoggerServiceBase _loggerServiceBase;

        public ExceptionLogAspect(Type loggerService)
        {
            // Bizim FileLogger ve DatabaseLogger olarak 2 tip logger imiz vardi. Programci bunlarin haricinde Business manager bir tip gondermeye calisirsa ona yonelik hata firlatma;
            if(loggerService.BaseType != typeof(LoggerServiceBase))
            {
                throw new System.Exception(AspectMessages.WrongLoggerTypeMessage);
            }

            _loggerServiceBase = (LoggerServiceBase)Activator.CreateInstance(loggerService);
        }

        protected override void OnException(IInvocation invocation, System.Exception e)
        {
            LogDetailWithException logDetailWithException = GetLogDetail(invocation);
            logDetailWithException.ExceptionMessage = e.Message;
            // Error olarak loglama yapicaz;
            _loggerServiceBase.Error(logDetailWithException);
        }

        private LogDetailWithException GetLogDetail(IInvocation invocation)
        {
            var logParameters = new List<LogParameter>();
            for (int i = 0; i < invocation.Arguments.Length; i++)
            {
                logParameters.Add(new LogParameter
                {
                    Name = invocation.GetConcreteMethod().GetParameters()[i].Name,
                    Value = invocation.Arguments[i],
                    Type = invocation.Arguments[i].GetType().Name
                });
            }

            var logDetailWithException = new LogDetailWithException
            {
                MethodName = invocation.Method.Name,
                LogParameters = logParameters
            };

            return logDetailWithException;
        }
    }
}
