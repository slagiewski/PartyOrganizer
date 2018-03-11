using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace PartyOrganizer.Core.Model.Interfaces
{
    public interface IEntity<T> : IEquatable<T> where T : class
    {
        int ID { get; set; }

        string ImagePath { get; set; }

    }
}