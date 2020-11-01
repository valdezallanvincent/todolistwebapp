using System.Reflection;
using Autofac;
using Module = Autofac.Module;

namespace BackEnd.DataAccess
{
    /// <summary>
    /// Registers services for dependency injection.
    /// </summary>
    public class DataAccessModule : Module
    {
        /// <inheritdoc/>
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            RegisterServices(builder);
        }

        private static void RegisterServices(ContainerBuilder builder)
        {
            var assembly = Assembly.GetExecutingAssembly();
            //builder.RegisterAssemblyTypes(assembly).Where(t => t.IsClosedTypeOf(typeof(IDataQuery<>)))
            //    .AsImplementedInterfaces().InstancePerLifetimeScope();
            //builder.RegisterType<UnitOfWork>().AsImplementedInterfaces().InstancePerLifetimeScope();
        }
    }
}
