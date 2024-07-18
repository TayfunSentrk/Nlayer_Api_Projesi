using Autofac;
using Nlayer.Data;
using Nlayer.Service.Mapping;
using Module = Autofac.Module;//reflection ile autofact 'tan gelen iki değer olduğu çakışmayı önlemek amacıyla yaptım
using System.Reflection;
using Nlayer.Data.Repositories;
using Nlayer.Core.Repositories;
using Autofac.Core;
using Nlayer.Service.Services;
using Nlayer.Core.Services;
using Nlayer.Data.UnitOfWorks;
using Nlayer.Core.UnitOfWorks;

namespace Nlayer.API.Modules
{
    public class RepoServiceModule:Module //autofac'ten geliyor 
    {
        protected override void Load(ContainerBuilder builder)
        {

            builder.RegisterGeneric(typeof(GenericRepository<>)).As(typeof(IGenericRepository<>)).InstancePerLifetimeScope();//generic olan repository için dll yapısı
            builder.RegisterGeneric(typeof(Service<>)).As(typeof(IService<>)).InstancePerLifetimeScope();//generic olan servis için dll yapısı
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();    //unitof work tek oldığu için bu yapı kullanıldı
            var apiAssembly=Assembly.GetExecutingAssembly();//üzerinde çalışılan assembly

            var dataAssembly = Assembly.GetAssembly(typeof(AppDbContext));//burda data katmanında assembly ulaşmak için bu katmanda olan herhangi bir classı verdim

            var serviceAssembly = Assembly.GetAssembly(typeof(MapProfile));//burda service katmanında assembly ulaşmak için bu katmanda olan herhangi bir classı verdim
            builder.RegisterAssemblyTypes(apiAssembly, dataAssembly, serviceAssembly).Where(x=>x.Name.EndsWith("Repository")).AsImplementedInterfaces().InstancePerLifetimeScope();  //b scope her istekte bir kez nesne oluşsun yani o istek tamamlanana o kadar nesne kullanıcak
            builder.RegisterAssemblyTypes(apiAssembly, dataAssembly, serviceAssembly).Where(x=>x.Name.EndsWith("Service")).AsImplementedInterfaces().InstancePerLifetimeScope();  //b scope her istekte bir kez nesne oluşsun yani o istek tamamlanana o kadar nesne kullanıcak

        }
    }
}
