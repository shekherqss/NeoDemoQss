using MvvmCross.IoC;
using MvvmCross.ViewModels;
using NeoDemoQss.Core.ViewModel;

namespace NeoDemoQss.Core
{
    public class CoreApp: MvxApplication
    {
        public override void Initialize()
        {
            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();

            //register dependencies for DI
            //AppContainer.RegisterDependencies();

            //initial viewmodel to be loaded
            RegisterAppStart<FillDetailsTypeOneViewModelSharp>();
            //RegisterCustomAppStart<CoreAppStart>();
        }
    }
}
