using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using MoldsApp.Data;
using MoldsApp.Infrastructure.Commands;
using MoldsApp.Models;
using MoldsApp.ViewModels.Base;

namespace MoldsApp.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {

        #region Методы

        #region UpdateCatalogueMethod

        private void UpdateCatalogue()
        {
            var currentMolds = ApplicationContext.GetContext().Molds.ToList();

            currentMolds = currentMolds.Where(p => p.Name.ToLower().Contains(TxtType.ToLower())).ToList();

            allMolds = new ObservableCollection<Molds>(currentMolds);
            AllMolds = new ObservableCollection<Molds>(currentMolds);

            OnPropertyChanged();

        }

        #endregion

        #endregion

        #region Переменные

        #region Текстблок поиска по типу

        private Molds newMolds { get; set; } = new Molds();
        public string TxtType
        {
            get => newMolds.Type;
            set => newMolds.Type = value;
        }

        //private string txtType { get; set; }
        //public string TxtType
        //{
        //    get { return this.txtType; }
        //    set
        //    {
        //        if (this.txtType != value)
        //        {
        //            this.txtType = value;

        //        }
        //    }
        //}

        #endregion

        #region Список всех прессформ

        private ObservableCollection<Molds> allMolds = new ObservableCollection<Molds>(ApplicationContext.GetContext().Molds.ToList());
        public ObservableCollection<Molds> AllMolds
        {
            get => allMolds; 
            set => allMolds = value; 
        }

        #endregion


        #endregion
       
        #region Команды

        #region CloseApplicationCommand
        public ICommand CloseApplicationCommand { get; }

        private bool CanCloseApplicationCommandExecute(object p) => true;

        private void OnCloseApplicationCommandExecuted(object p)
        {
            Application.Current.Shutdown();
        }
        #endregion

        #region SearchCommand

        public ICommand SearchCommand { get; }

        private bool CanSearchCommandExecute(object p) => true;

        private void OnSearchCommandExecuted(object p)
        {
            List <Molds> something = ApplicationContext.GetContext().Molds.ToList();
            something = something.Where(d => d.Name.ToLower().Contains(TxtType.ToLower())).ToList();            
            AllMolds = new ObservableCollection<Molds>(something);
            OnPropertyChanged();
        }

        #endregion

        #endregion

        public MainWindowViewModel()
        {
            #region Экземпляры команд

            CloseApplicationCommand = new LamdaCommand(OnCloseApplicationCommandExecuted, CanCloseApplicationCommandExecute);
            SearchCommand = new LamdaCommand(OnSearchCommandExecuted, CanSearchCommandExecute);

            #endregion
            
        }
    }
}