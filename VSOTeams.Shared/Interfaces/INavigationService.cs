using System;

namespace VSOTeams.Interfaces
{
    public interface INavigationService
    {
        void Navigate(Type sourcePageType);
        void Navigate(Type sourcePageType, object parameter);
        void GoBack();
    }
}
