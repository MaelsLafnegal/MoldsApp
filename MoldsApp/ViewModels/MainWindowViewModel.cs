﻿using System;
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
using MoldsApp.Views.Windows;

namespace MoldsApp.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
      
        #region Переменные
       
        #region Текстблоки для поиска

        private Molds newMolds { get; set; } = new Molds();
        public string TxtType
        {
            get => newMolds.Type;
            set => newMolds.Type = value;
        }

        public string TxtName
        {
            get => newMolds.Name;
            set => newMolds.Name = value;
        }

        public string TxtKus
        {
            get => newMolds.Kus;
            set => newMolds.Kus = value;
        }

        #endregion

        #region Список всех прессформ

        private ObservableCollection<Molds> allMolds = new ObservableCollection<Molds>(ApplicationContext.GetContext().Molds.ToList());
        public ObservableCollection<Molds> AllMolds
        {
            get => allMolds; 
            set => allMolds = value;           
        }

        #endregion

        #region Отфильтрованный список прессформ

        private ObservableCollection<Molds> filteredMolds = new ObservableCollection<Molds>(ApplicationContext.GetContext().Molds.ToList());
        public ObservableCollection<Molds> FilteredMolds
        {
            get
            {               
                filteredMolds = AllMolds;              
                var someshit = new List<Molds>(filteredMolds);
                if (TxtName != null)
                    someshit = someshit.Where(p => p.Name.ToLower().Contains(TxtName.ToLower())).ToList();
                if (TxtType != null)
                    someshit = someshit.Where(p => p.Type.ToLower().Contains(TxtType.ToLower())).ToList();
                if (TxtKus != null)
                    someshit = someshit.Where(p => p.Kus.ToLower().Contains(TxtKus.ToLower())).ToList();
                return new ObservableCollection<Molds>(someshit);
            }
            
            set => filteredMolds = value;
        }

        #endregion

        #region Выбранный элемент DataGrid

        private List<Molds> _moldsforremoving;

        public List<Molds> MoldsForRemoving
        {
            get => _moldsforremoving;
            set => Set(ref _moldsforremoving, value);
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
            OnPropertyChanged("FilteredMolds");
        }

        #endregion

        #region Open AddEditWindowCommand
        public ICommand AddEditWindowCommand { get; }

        private bool CanAddEditWindowCommandExecute(object p) => true;

        private void OnAddEditWindowCommandExecuted(object p)
        {
            AddEditWindow window = new AddEditWindow();
            window.Show();
        }
        #endregion

        #region DeleteMoldCommand

        public ICommand DeleteMoldCommand { get; }

        private bool CanDeleteMoldCommandExecute(object p)
        {
            if (MoldsForRemoving != null) return true;
            else MessageBox.Show("Вы не выбрали элемент в таблице.", "Ошибка",
         MessageBoxButton.OK, MessageBoxImage.Error); return false;
        }

        private void OnDeleteMoldCommandExecuted(object p)
        {
            if (MessageBox.Show($"Вы точно хотите удалить следующие {MoldsForRemoving.Count()} элементов?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    ApplicationContext.GetContext().Molds.RemoveRange(MoldsForRemoving);
                    ApplicationContext.GetContext().SaveChanges();
                    MessageBox.Show("Данные успешно удалены.", "Удаление данных",
         MessageBoxButton.OK, MessageBoxImage.Information);
                    OnPropertyChanged("FilteredMolds");
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString(), "Ошибка",
         MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        #endregion

        #endregion

        public MainWindowViewModel()
        {
            #region Экземпляры команд

            CloseApplicationCommand = new LamdaCommand(OnCloseApplicationCommandExecuted, CanCloseApplicationCommandExecute);
            SearchCommand = new LamdaCommand(OnSearchCommandExecuted, CanSearchCommandExecute);
            AddEditWindowCommand = new LamdaCommand(OnAddEditWindowCommandExecuted, CanAddEditWindowCommandExecute);
            DeleteMoldCommand = new LamdaCommand(OnDeleteMoldCommandExecuted, CanDeleteMoldCommandExecute);


            #endregion

            #region Экземпляры переменных для поиска

            TxtName = null;
            TxtKus = null;
            TxtType = null;

            #endregion

        }
    }
}