using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Mvc;
using Moq;
using Ninject;
using Cookery.WebUI.Infrastructure.Abstract;
using Cookery.WebUI.Infrastructure.Concrete;

namespace Cookery.WebUI.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            
            kernel.Bind<IRecipeRepository>().To<EFRecipeRepository>();
            kernel.Bind<IAuthProvider>().To<FormAuthProvider>();
        }
    }
}