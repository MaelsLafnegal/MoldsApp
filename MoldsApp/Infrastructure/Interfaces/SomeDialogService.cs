using MoldsApp.Views.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoldsApp.Infrastructure.Interfaces
{
    public sealed class SomeDialogService : IDialogService
    {
        public bool? ShowDialog()
        {
            var dialog = new AddEditWindow();

            return dialog.ShowDialog();
        }
    }
}
