﻿using AssetTracker.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AssetTracker.ViewModel
{
    public class MainViewModel : ViewModel
    {

        public User CurrentUser { get; set; }

        private static MainViewModel instance;
        public static MainViewModel Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MainViewModel();
                }

                return instance;
            }
        }

        public MainViewModel()
        {
            CurrentUser = context.Users.Find(2);
            NotifyPropertyChanged("CurrentUser");
        }
    }
}