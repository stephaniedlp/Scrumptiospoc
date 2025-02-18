﻿using System.ComponentModel;

namespace Scrumptiospoc.Models
{
    public class BaseModel : INotifyPropertyChanged
    {
       

        public event PropertyChangedEventHandler PropertyChanged;

   

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
