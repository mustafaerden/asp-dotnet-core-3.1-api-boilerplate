using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;

namespace Core.Aspects.Autofac.Performance
{
    // Manager da ki metotlarda kullanilir. Islemin ne kadar surdugu hesaplanir.
    public class PerformanceAspect : MethodInterception
    {
        private int _interval;
        private Stopwatch _stopwatch;

        public PerformanceAspect(int interval)
        {
            _interval = interval;
            _stopwatch = ServiceTool.ServiceProvider.GetService<Stopwatch>();
        }

        protected override void OnBefore(IInvocation invocation)
        {
            // method cagrildi ve isleme baslamadan once stopwatch i calistir;
            _stopwatch.Start();
        }

        protected override void OnAfter(IInvocation invocation)
        {
            // Cagrilan metot isini bitirdi. Gecen sureyi aliyoruz ve stopwatch i durduruyoruz;
            if(_stopwatch.Elapsed.TotalSeconds > _interval)
            {
                Debug.WriteLine($"Performance : {invocation.Method.DeclaringType.FullName}.{invocation.Method.Name}-->{_stopwatch.Elapsed.TotalSeconds}");
            }
            _stopwatch.Reset();
        }
    }
}
