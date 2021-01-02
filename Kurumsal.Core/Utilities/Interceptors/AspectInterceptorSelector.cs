using Castle.DynamicProxy;
using Kurumsal.Core.Aspects.Autofac.Exception;
using Kurumsal.Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using System;
using System.Linq;
using System.Reflection;

namespace Core.Utilities.Interceptors
{
    public class AspectInterceptorSelector : IInterceptorSelector
    {
        public IInterceptor[] SelectInterceptors(Type type, MethodInfo method, IInterceptor[] interceptors)
        {
            var classAttributes = type.GetCustomAttributes<MethodInterceptionBaseAttribute>(true).ToList();
            var methodAttributes = type.GetMethod(method.Name).GetCustomAttributes<MethodInterceptionBaseAttribute>(true);
            classAttributes.AddRange(methodAttributes);

            // manager larda metotlarda try catch kullanmamak veya her metodun basinda ExceptionLogAspect i cagirmamak icin burada otomatik olarak tum metotlarda calismasi icin ayarladik; Ancak database e baglanamama gibi durumlardaki exceptionlari database deki logs tablosuna yazamayacagimizda burada FileLogger i kullanmamiz daha mantikli.
            classAttributes.Add(new ExceptionLogAspect(typeof(FileLogger)));

            return classAttributes.OrderBy(x => x.Priority).ToArray();
        }
    }
}
