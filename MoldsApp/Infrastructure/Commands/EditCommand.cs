using MoldsApp.Infrastructure.Commands.Base;
using MoldsApp.Models;
using MoldsApp.Views.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoldsApp.Infrastructure.Commands
{
    internal class EditCommand : Command
    {
        public override bool CanExecute(object SelectedToEditMold) => true;

        public override void Execute(object SelectedToEditMold)
        {
            AddEditWindow window = new AddEditWindow();
            window.Show();
        }
    }
}
