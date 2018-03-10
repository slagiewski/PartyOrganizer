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

namespace PartyOrganizer.Core.Repository
{
    public interface IRepository<T> where T: class
    {
        IEnumerable<T> GetAll();

        T GetByID(int ID);

        void Add(T entity);

        void Remove(T entity);

        //only for unit tests purposes, it will be removed in the final ver
        void RemoveAll();

        void Populate();
    }
}