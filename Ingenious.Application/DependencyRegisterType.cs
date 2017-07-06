using Ingenious.Application.Implement;
using Ingenious.Application.Interface;
using Ingenious.Infrastructure.Cache;
using Ingenious.Repositories;
using Ingenious.Repositories.EntityFramework;
using Ingenious.Repositories.Implement;
using Ingenious.Repositories.Interface;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace Ingenious.Application
{
    public class DependencyRegisterType
    {
        public static void Register(ref UnityContainer container)
        {
            //注册 repository 和 service 

            container.AddNewExtension<Interception>();

            //缓存实现
            container.RegisterType<IAPICacheProvider, APIMemoryCacheProvider>(new Interceptor<InterfaceInterceptor>(),
                new InterceptionBehavior<Ingenious.Infrastructure.AOP.LoggingBehavior>());
            container.RegisterType<ICacheProvider, MemoryCacheProvider>(new Interceptor<InterfaceInterceptor>(),
                new InterceptionBehavior<Ingenious.Infrastructure.AOP.LoggingBehavior>());



            #region Api.Go

            container.RegisterType<IF_UserRepository, F_UserRepository>(new Interceptor<InterfaceInterceptor>(),
                new InterceptionBehavior<Ingenious.Infrastructure.AOP.LoggingBehavior>());
            container.RegisterType<IF_UserService, F_UserService>(new Interceptor<InterfaceInterceptor>(),
                new InterceptionBehavior<Ingenious.Infrastructure.AOP.LoggingBehavior>());

            container.RegisterType<IF_UserDetailRepository, F_UserDetailRepository>(new Interceptor<InterfaceInterceptor>(),
                new InterceptionBehavior<Ingenious.Infrastructure.AOP.LoggingBehavior>());
            container.RegisterType<IF_UserDetailService, F_UserDetailService>(new Interceptor<InterfaceInterceptor>(),
                new InterceptionBehavior<Ingenious.Infrastructure.AOP.LoggingBehavior>());

            container.RegisterType<IF_BrankRepository, F_BrankRepository>(new Interceptor<InterfaceInterceptor>(),
                new InterceptionBehavior<Ingenious.Infrastructure.AOP.LoggingBehavior>());

            #endregion

            container.RegisterType<IRepositoryContext, EntityFrameworkRepositoryContext>();

            container.RegisterType<IUserRepository, UserRepository>(new Interceptor<InterfaceInterceptor>(),
                new InterceptionBehavior<Ingenious.Infrastructure.AOP.LoggingBehavior>());
            container.RegisterType<IUserService, UserService>(new Interceptor<InterfaceInterceptor>(),
                new InterceptionBehavior<Ingenious.Infrastructure.AOP.LoggingBehavior>());

            container.RegisterType<IUserDetailRepository, UserDetailRepository>(new Interceptor<InterfaceInterceptor>(),
                new InterceptionBehavior<Ingenious.Infrastructure.AOP.LoggingBehavior>());
            container.RegisterType<IUserDetailService, UserDetailService>(new Interceptor<InterfaceInterceptor>(),
                new InterceptionBehavior<Ingenious.Infrastructure.AOP.LoggingBehavior>());

            container.RegisterType<IDepartmentRepository, DepartmentRepository>(new Interceptor<InterfaceInterceptor>(),
                new InterceptionBehavior<Ingenious.Infrastructure.AOP.LoggingBehavior>());
            container.RegisterType<IDepartmentService, DepartmentService>(new Interceptor<InterfaceInterceptor>(),
                new InterceptionBehavior<Ingenious.Infrastructure.AOP.LoggingBehavior>());

            container.RegisterType<IClientRepository, ClientRepository>(new Interceptor<InterfaceInterceptor>(),
                new InterceptionBehavior<Ingenious.Infrastructure.AOP.LoggingBehavior>());
            container.RegisterType<IClientService, ClientService>(new Interceptor<InterfaceInterceptor>(),
                new InterceptionBehavior<Ingenious.Infrastructure.AOP.LoggingBehavior>());

            container.RegisterType<IActivityRepository, ActivityRepository>(new Interceptor<InterfaceInterceptor>(),
                new InterceptionBehavior<Ingenious.Infrastructure.AOP.LoggingBehavior>());
            container.RegisterType<IActivityService, ActivityService>(new Interceptor<InterfaceInterceptor>(),
                new InterceptionBehavior<Ingenious.Infrastructure.AOP.LoggingBehavior>());

            container.RegisterType<IProductRepository, ProductRepository>(new Interceptor<InterfaceInterceptor>(),
                new InterceptionBehavior<Ingenious.Infrastructure.AOP.LoggingBehavior>());
            container.RegisterType<IProductService, ProductService>(new Interceptor<InterfaceInterceptor>(),
                new InterceptionBehavior<Ingenious.Infrastructure.AOP.LoggingBehavior>());

            container.RegisterType<IPriceStrategyRepository, PriceStrategyRepository>(new Interceptor<InterfaceInterceptor>(),
                new InterceptionBehavior<Ingenious.Infrastructure.AOP.LoggingBehavior>());
            container.RegisterType<IPriceStrategyService, PriceStrategyService>(new Interceptor<InterfaceInterceptor>(),
                new InterceptionBehavior<Ingenious.Infrastructure.AOP.LoggingBehavior>());

            container.RegisterType<IPriceStrategyDetailRepository, PriceStrategyDetailRepository>(new Interceptor<InterfaceInterceptor>(),
                new InterceptionBehavior<Ingenious.Infrastructure.AOP.LoggingBehavior>());
            container.RegisterType<IPriceStrategyDetailService, PriceStrategyDetailService>(new Interceptor<InterfaceInterceptor>(),
                new InterceptionBehavior<Ingenious.Infrastructure.AOP.LoggingBehavior>());

            container.RegisterType<IContractRepository, ContractRepository>(new Interceptor<InterfaceInterceptor>(),
                new InterceptionBehavior<Ingenious.Infrastructure.AOP.LoggingBehavior>());
            container.RegisterType<IContractService, ContractService>(new Interceptor<InterfaceInterceptor>(),
                new InterceptionBehavior<Ingenious.Infrastructure.AOP.LoggingBehavior>());

            container.RegisterType<IAccountRepository, AccountRepository>(new Interceptor<InterfaceInterceptor>(),
                new InterceptionBehavior<Ingenious.Infrastructure.AOP.LoggingBehavior>());
            container.RegisterType<IAccountService, AccountService>(new Interceptor<InterfaceInterceptor>(),
                new InterceptionBehavior<Ingenious.Infrastructure.AOP.LoggingBehavior>());

            container.RegisterType<IBillRepository, BillRepository>(new Interceptor<InterfaceInterceptor>(),
                new InterceptionBehavior<Ingenious.Infrastructure.AOP.LoggingBehavior>());
            container.RegisterType<IBillService, BillService>(new Interceptor<InterfaceInterceptor>(),
                new InterceptionBehavior<Ingenious.Infrastructure.AOP.LoggingBehavior>());

            container.RegisterType<IRechargeRepository, RechargeRepository>(new Interceptor<InterfaceInterceptor>(),
                new InterceptionBehavior<Ingenious.Infrastructure.AOP.LoggingBehavior>());
            container.RegisterType<IRechargeService, RechargeService>(new Interceptor<InterfaceInterceptor>(),
                new InterceptionBehavior<Ingenious.Infrastructure.AOP.LoggingBehavior>());

            container.RegisterType<IDictionaryRepository, DictionaryRepository>(new Interceptor<InterfaceInterceptor>(),
                new InterceptionBehavior<Ingenious.Infrastructure.AOP.LoggingBehavior>());
            container.RegisterType<IDictionaryService, DictionaryService>(new Interceptor<InterfaceInterceptor>(),
                new InterceptionBehavior<Ingenious.Infrastructure.AOP.LoggingBehavior>());

            container.RegisterType<IClientContactRepository, ClientContactRepository>(new Interceptor<InterfaceInterceptor>(),
                new InterceptionBehavior<Ingenious.Infrastructure.AOP.LoggingBehavior>());
            container.RegisterType<IClientContactService, ClientContactService>(new Interceptor<InterfaceInterceptor>(),
                new InterceptionBehavior<Ingenious.Infrastructure.AOP.LoggingBehavior>());

            
            //container.RegisterType<IProductRepository, ProductRepository>();
            //container.RegisterType<IProductService, ProductService>(new Interceptor<InterfaceInterceptor>(),
            //    new InterceptionBehavior<Ingenious.Infrastucture.AOP.LoggingBehavior>());

        }
    }
}
